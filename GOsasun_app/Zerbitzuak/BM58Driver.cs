using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading;
using System.Xml.Linq;
using HidSharp;
using GOsasun_app.Modeloak;
using System.Diagnostics;

namespace GOsasun_app.Zerbitzuak
{
    /// <summary>
    /// Beurer BM58 tentsiometroarekin USB bidez komunikatzeko zerbitzua.
    /// curzon01/bm58 Python inplementazioan oinarritua, Serie eta HID euskarriarekin.
    /// </summary>
    public class BM58Driver
    {
        private const int BaudRate = 4800;
        private const int Beurer_VID = 0x0C45;
        private const int Beurer_PID = 0x7406;

        // TentsioNeurria kenduta OBP logika jarraitzeko (Neurketa modeloa erabiliko da)

        #region Komunikazio Abstrakzioa
        private interface IChannel : IDisposable
        {
            void Write(byte[] data);
            byte ReadByte();
            byte[] ReadPayload(); // Berria: 8 byteko karga-erabilgarria irakurtzeko
            void DiscardInBuffer();
            bool IsOpen { get; }
        }

        private class SerialChannel : IChannel
        {
            private readonly SerialPort _port;
            public SerialChannel(string portName)
            {
                _port = new SerialPort(portName, BaudRate, Parity.None, 8, StopBits.One);
                _port.ReadTimeout = 2000;
                _port.WriteTimeout = 2000;
                _port.DtrEnable = true;
                _port.RtsEnable = true;
                _port.Open();
                Thread.Sleep(200);
            }
            public void Write(byte[] data) => _port.Write(data, 0, data.Length);
            public byte ReadByte() => (byte)_port.ReadByte();
            public byte[] ReadPayload() 
            {
                byte[] buffer = new byte[8];
                int read = 0;
                while (read < 8) {
                    int n = _port.Read(buffer, read, 8 - read);
                    if (n <= 0) break;
                    read += n;
                }
                return buffer;
            }
            public void DiscardInBuffer() => _port.DiscardInBuffer();
            public bool IsOpen => _port.IsOpen;
            public void Dispose() => _port.Dispose();
        }

        private class HidChannel : IChannel
        {
            public enum ProtocolMode { Raw, LengthPrefixed, MicrodiaTunnel, ReportId8Raw, ReportId0 }
            private readonly HidDevice _device;
            private readonly HidStream _stream;
            private readonly int _maxInput;
            private readonly int _maxOutput;
            private readonly int _maxFeature;
            public ProtocolMode Mode { get; set; } = ProtocolMode.Raw;

            public HidChannel(HidDevice device)
            {
                _device = device;
                if (!device.TryOpen(out _stream)) throw new Exception("Ezin izan da HID gailua ireki.");
                _stream.ReadTimeout = 2000;
                _stream.WriteTimeout = 2000;
                
                // Gailuaren report luzera natiboak lortu (Metodo berriak erabiliz obsolete ekiditeko)
                _maxInput = device.GetMaxInputReportLength();
                _maxOutput = device.GetMaxOutputReportLength();
                _maxFeature = device.GetMaxFeatureReportLength();
            }

            public void ConfigureBaudRate()
            {
                Debug.WriteLine($"[HID] Microdia baud rate 4800 (0x12C0) konfiguratzen (MaxFeature={_maxFeature})...");
                try {
                    // Microdia/Sonix-ek Feature Report-a behar du abiadura ezartzeko (ID=2, SetFlags=1, BaudLow, BaudHigh)
                    // Padded to the device's native feature report length.
                    byte[] baud = new byte[_maxFeature];
                    if (_maxFeature >= 4) {
                        baud[0] = 0x02; // Report ID
                        baud[1] = 0x01; // Flags (1 = set baud)
                        baud[2] = 0xC0; // 0x12C0 = 4800
                        baud[3] = 0x12;
                    }
                    _stream.SetFeature(baud);
                    Thread.Sleep(800); // Zenbait gailuk denbora behar dute UART sinkronizatzeko
                    Debug.WriteLine("[HID] Baud rate konfigurazioa bidalita.");
                } catch (Exception ex) {
                    Debug.WriteLine($"[HID] Baud rate konfigurazioak huts egin du: {ex.Message}");
                }
            }

            public void Write(byte[] data) 
            { 
                byte[] report = new byte[9]; // Beti 9 byte (MaxOutput)
                
                if (Mode == ProtocolMode.MicrodiaTunnel)
                {
                    // Report ID 0x08 + Length + Data
                    report[0] = 0x08;
                    report[1] = (byte)data.Length;
                    Array.Copy(data, 0, report, 2, Math.Min(data.Length, 7));
                    Debug.WriteLine($"[HID WRITE 0x08] Len={report[1]} Data={BitConverter.ToString(data)}");
                    _stream.Write(report);
                }
                else if (Mode == ProtocolMode.ReportId8Raw)
                {
                    // Report ID 0x08 + Data (Directly)
                    report[0] = 0x08;
                    Array.Copy(data, 0, report, 1, Math.Min(data.Length, 8));
                    _stream.Write(report);
                }
                else if (Mode == ProtocolMode.ReportId0)
                {
                    // Report ID 0x00 + Data
                    // Windows-en, ID 0 badugu, HidSharp-ek 8 byte datu bakarrik onar ditzake 9 byteko paketean
                    report[0] = 0x00;
                    Array.Copy(data, 0, report, 1, Math.Min(data.Length, 8));
                    _stream.Write(report);
                }
                else if (Mode == ProtocolMode.LengthPrefixed)
                {
                    report[0] = 0x01;
                    Array.Copy(data, 0, report, 1, Math.Min(data.Length, 8));
                    _stream.Write(report);
                }
                else
                {
                    // Raw mode (ID gabe asmoa, baina bufferrak 9 izan behar du)
                    Array.Copy(data, 0, report, 0, Math.Min(data.Length, 9));
                    _stream.Write(report);
                }
            }

            public byte ReadByte() 
            { 
                byte[] payload = ReadPayload();
                // Bilatu lehenengo byte esanguratsua (ez 0, ez 0xF4 filler-a)
                foreach (byte b in payload) if (b != 0 && b != 0xF4) return b;
                return payload[0];
            }

            public byte[] ReadPayload()
            {
                byte[] buffer = new byte[Math.Max(_maxInput, 9)];
                int n = _stream.Read(buffer);
                if (n <= 0) throw new TimeoutException();

                Debug.WriteLine($"[HID READ] {BitConverter.ToString(buffer, 0, n)}");

                byte[] payload = new byte[8];
                if (Mode == ProtocolMode.MicrodiaTunnel)
                {
                    // Tunnel Packets (Sonix): [ID=0x07, Length, Data0, Data1, ...]
                    if (buffer[0] == 0x07 && n >= 3) {
                        Array.Copy(buffer, 2, payload, 0, Math.Min(n - 2, 8));
                    } else {
                        Array.Copy(buffer, 1, payload, 0, Math.Min(n - 1, 8));
                    }
                }
                else if (Mode != ProtocolMode.Raw)
                {
                    // Skip Report ID at index 0
                    Array.Copy(buffer, 1, payload, 0, Math.Min(n - 1, 8));
                }
                else
                {
                    Array.Copy(buffer, 0, payload, 0, Math.Min(n, 8));
                }
                return payload;
            }
            public void DiscardInBuffer() 
            { 
                // Saiatu bufferretan dagoena irakurtzen 'garbitzeko' (itxaron gabe)
                int originalTimeout = _stream.ReadTimeout;
                try {
                    _stream.ReadTimeout = 50;
                    byte[] garbage = new byte[_maxInput];
                    _stream.Read(garbage);
                } catch { }
                finally {
                    _stream.ReadTimeout = originalTimeout;
                }
            }
            public bool IsOpen => _stream != null;
            public void Dispose() => _stream?.Dispose();
        }
        #endregion

        /// <summary>
        /// Gailua konektatuta dagoen egiaztatzen du (Hardware ID bidez).
        /// </summary>
        public bool EgiaztatuHardwareKonexioa()
        {
            return DeviceList.Local.GetHidDevices(Beurer_VID, Beurer_PID).Any();
        }

        /// <summary>
        /// Gailua bilatzen du (Serie portuan lehenengo, gero HID bidez).
        /// </summary>
        public string? BilatuGailua(out bool isHid)
        {
            isHid = false;

            // 1. HID bidez saiatu (Gomendatua, driverrik behar ez duelako)
            if (EgiaztatuHardwareKonexioa())
            {
                isHid = true;
                return "USB-HID: Beurer BM58";
            }

            // 2. Serie portuen bidez saiatu (Modelo zaharragoak edo driverra badute)
            string[] portuak = SerialPort.GetPortNames();
            foreach (string portuIzena in portuak)
            {
                try
                {
                    using (var channel = new SerialChannel(portuIzena))
                    {
                        for (int i = 0; i < 2; i++)
                        {
                            channel.DiscardInBuffer();
                            channel.Write(new byte[] { 0xAA });
                            if (channel.ReadByte() == 0x55) return portuIzena;
                            Thread.Sleep(100);
                        }
                    }
                }
                catch { }
            }

            return null;
        }

        /// <summary>
        /// Neurriak irakurtzen ditu, aukeratutako kanalaren arabera.
        /// </summary>
        public Neurketa IrakurriAzkenNeurria(string identifier, bool isHid, int pazienteId)
        {
            IChannel? channel = null;
            try
            {
                if (isHid)
                {
                    var device = DeviceList.Local.GetHidDevices(Beurer_VID, Beurer_PID).FirstOrDefault();
                    if (device == null) throw new Exception("HID gailua deskonektatu da.");
                    channel = new HidChannel(device);
                }
                else
                {
                    channel = new SerialChannel(identifier);
                }

                HidChannel? hid = channel as HidChannel;
                if (hid == null) throw new Exception("HID ez dago erabilgarri.");

                hid.ConfigureBaudRate();
                Thread.Sleep(1000); 

                // 2. Handshake Nagusia - BRUTE FORCE AURKIKUNTZA
                bool ok = false;
                var modesToTry = new[] { 
                    HidChannel.ProtocolMode.MicrodiaTunnel, 
                    HidChannel.ProtocolMode.ReportId8Raw,
                    HidChannel.ProtocolMode.ReportId0,
                    HidChannel.ProtocolMode.Raw 
                };

                Debug.WriteLine("[BM58] Brute force handshake hasaten...");

                foreach (var mode in modesToTry)
                {
                    hid.Mode = mode;
                    Debug.WriteLine($"[BM58] Saiatzen Mode: {mode} (Padded Handshake)...");

                    for (int i = 0; i < 6; i++) 
                    {
                        channel.DiscardInBuffer();
                        channel.Write(new byte[] { 0xAA }); // Shake Signal
                        Thread.Sleep(200); 
                        
                        try {
                            byte resp = channel.ReadByte();
                            if (resp == 0x55) { 
                                Debug.WriteLine($"[BM58] SHAKE SUCCESS! Mode: {mode}");
                                
                                // ID Confirmations (0xA4) are critical to clear the PC Busy state
                                // Logs show 4 identification parts are requested
                                Debug.WriteLine("[BM58] ID Confirmations (4 attempts) bidaltzen...");
                                for (int j = 0; j < 4; j++) {
                                    channel.Write(new byte[] { 0xA4 });
                                    Thread.Sleep(150);
                                    try { channel.ReadPayload(); } catch { } // Kontsumitu ID zati osoa
                                }

                                ok = true; 
                                break; 
                            }
                        } catch { }
                        Thread.Sleep(300);
                    }
                    if (ok) break;
                }
                if (!ok) throw new Exception("Gailua ez dago prest (Handshake errorea: PC Er ekiditeko ziurtatu pantailan 'PC' agertzen dela).");

                // 2. Errekor kopurua (A2)
                Debug.WriteLine("[BM58] Errekor kopurua eskatzen (0xA2)...");
                channel.Write(new byte[] { 0xA2 });
                Thread.Sleep(100);
                int count = channel.ReadByte();
                Debug.WriteLine($"[BM58] Errekor kopurua: {count}");
                if (count <= 0) throw new Exception("Ez dago neurketarik gailuan (Record count <= 0).");

                // 3. Irakurri azken errekorra (A3 + index)
                Debug.WriteLine($"[BM58] Azken errekorra irakurtzen (0xA3 {count:X2})...");
                channel.Write(new byte[] { 0xA3, (byte)count });
                Thread.Sleep(200);
                
                // GARRANTZITSUA: Irakurri byte guztiak REPORT BAKARREAN (HID atomic read)
                byte[] data = channel.ReadPayload();
                Debug.WriteLine($"[BM58] Errecords-eko datu esanguratsuak: {BitConverter.ToString(data)}");

                // Check finalization
                try {
                    channel.Write(new byte[] { 0xA5 }); // End Communication sig
                    Thread.Sleep(50);
                    channel.ReadPayload();
                } catch { }

                // Beurer data starts with 0xAC or similar in some cases, but official logs show 
                // formatted records. We'll use the mapping from the logger.

                // Itzuli Neurketa objektu berria (OBP logika)
                // Irakurritako datuen mapping-a (Logs-etan oinarritua):
                // 56 2b 41 01 02 0b 34 08 -> 1. errekorra
                // Byte-en esanahia (0-indizea):
                // 0: Sistole-25 (e.g., 0x56 = 86 + 25 = 111)
                // 1: Diastole-25 (e.g., 0x2b = 43 + 25 = 68)
                // 2: Pultsua (e.g., 0x41 = 65)
                // 3: Hilabetea (0x01 = Jan)
                // 4: Eguna (0x02 = 2nd)
                // 5: Ordua (0x0b = 11)
                // 6: Minutua (0x34 = 52)
                // 7: Urtea (0x08 = 2008?)
                
                return new Neurketa
                {
                    PazienteId = pazienteId,
                    TentsioSistolikoa = data[0] + 25,
                    TentsioDiastolikoa = data[1] + 25,
                    Pultsua = data[2],
                    ErregistroData = new DateTime(2000 + data[7], data[3], data[4], data[5], data[6], 0),
                    Sintomak = "" 
                };
            }
            finally
            {
                channel?.Dispose();
            }
        }

        public void GordeXML(Neurketa neurria)
        {
            // Erabiltzaileak eskatutako formatu zehatza txantiloiaren arabera
            XDocument doc = new XDocument(
                new XDeclaration("1.0", "utf-8", "yes"),
                new XElement("Neurketak",
                    new XElement("Neurketa",
                        new XElement("erregistro_data", neurria.ErregistroData.ToString("yyyy-MM-dd HH:mm:ss")),
                        new XElement("paziente_id", neurria.PazienteId),
                        new XElement("altuera", ""),
                        new XElement("pisua", ""),
                        new XElement("glukosa_mg_dl"), 
                        new XElement("tentsio_sistolikoa", neurria.TentsioSistolikoa),
                        new XElement("tentsio_diastolikoa", neurria.TentsioDiastolikoa),
                        new XElement("pultsua_ppm", neurria.Pultsua)
                    )
                )
            );

            // Fitxategi izen dinamikoa
            string fitxategiIzena = $"TENS_{neurria.ErregistroData:yyyy-MM-dd_HH-mm-ss}.xml";

            // 1. Proiektuko backup karpeta
            string proiektuBidea = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "xml");
            if (!Directory.Exists(proiektuBidea)) Directory.CreateDirectory(proiektuBidea);
            doc.Save(Path.Combine(proiektuBidea, fitxategiIzena));

            // 2. Apache karpeta (Aplication Interface)
            string apacheBidea = @"C:\Apache24-64\htdocs\neurketak";
            try
            {
                if (!Directory.Exists(apacheBidea)) Directory.CreateDirectory(apacheBidea);
                doc.Save(Path.Combine(apacheBidea, fitxategiIzena));
            }
            catch { }
        }
    }
}
