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
using GOsasun_app.Modeloa;
using System.Diagnostics;

namespace GOsasun_app.Kontrola.Zerbitzuak
{
    /// <summary>
    /// Beurer BM58 tentsiometroarekin USB bidez komunikatzeko zerbitzua.
    /// </summary>
    public class BM58RawRecord
    {
        public byte[] Data { get; set; }
        public int Index { get; set; }
        public int UserId { get; set; } // 1 edo 2

        public bool IsU1 => UserId == 1;
        public bool IsU2 => UserId == 2;
    }

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
                    // Tunnel Packets (Sonix/Microdia): [ID=0x07/0x08, Length/ID, Data0, Data1, ...]
                    // In some Beurer versions, it's [08, 08, Sys, Dia...]
                    if ((buffer[0] == 0x07 || buffer[0] == 0x08) && n >= 3) {
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

        public List<BM58RawRecord> IrakurriErrekordGuztiak(string identifier, bool isHid)
        {
            var records = new List<BM58RawRecord>();
            IChannel? channel = null;
            try
            {
                channel = KonektatuGailura(identifier, isHid);
                channel.Write(new byte[] { 0xA4 }); // Init
                Thread.Sleep(200);

                // FASE 1: USER 1 (0xA6 + 0-59 indizeak)
                IrakurriBankua(channel, records, 1, 0xA6);

                // FASE 2: USER 2 (0xA7 + 0-59 indizeak)
                IrakurriBankua(channel, records, 2, 0xA7);

                channel.Write(new byte[] { 0xA5 }); // End
                return records;
            }
            finally { channel?.Dispose(); }
        }

        private void IrakurriBankua(IChannel channel, List<BM58RawRecord> records, int userId, byte cmd)
        {
            // FASE 2 bada (U2), seguruagoa da lehenik Ending eta Re-Init egitea bankua aldatzeko
            if (userId == 2) {
                try { 
                    channel.Write(new byte[] { 0xA5 }); Thread.Sleep(300);
                    channel.Write(new byte[] { 0xA4 }); Thread.Sleep(300);
                } catch { }
            }

            try { 
                channel.Write(new byte[] { cmd }); 
                Thread.Sleep(800); // Itxaronaldi luzeagoa banku aldaketarentzat
                channel.DiscardInBuffer();
            } catch { }

            byte[] lastData = null;
            int hutsikJarraian = 0;

            for (int idx = 0; idx < 60; idx++)
            {
                if (hutsikJarraian >= 8) break; 

                bool aurkitua = false;
                for (int saialdia = 0; saialdia < 3; saialdia++) 
                {
                    try {
                        channel.Write(new byte[] { 0xA3, (byte)idx });
                        Thread.Sleep(70); 
                        byte[] data = channel.ReadPayload();

                        if (data != null && data.Length >= 8) {
                            // U2 bita (0x80) data batean etortzen da bankua bereizteko.
                            // BM58 "Andon" barne-modeloak data[4] (hilabetea) erabiltzen du. Batzuek data[3] (urtea).
                            bool isU2Month = (data[4] & 0x80) != 0;
                            bool isU2Year = (data[3] & 0x80) != 0;
                            bool trueU2Bit = isU2Month || isU2Year;

                            int benetakoHila = data[4] & 0x7F; // 0x80 bita ezabatu hilabete erreala jakiteko
                            
                            bool datuaOn = (data[0] > 10 && data[0] < 250); 
                            bool hilaOn = (benetakoHila >= 1 && benetakoHila <= 12);
                            bool egunaOn = (data[5] >= 1 && data[5] <= 31);

                            if (datuaOn && hilaOn && egunaOn) {
                                bool isConsecutiveDuplicate = lastData != null && data.SequenceEqual(lastData);
                                bool alreadyExists = records.Any(r => r.Data.SequenceEqual(data)); // Bankua aldatzean huts egin badu U1 berriro ez irakurtzeko
                                
                                if (!isConsecutiveDuplicate && !alreadyExists) {
                                    // Gailuak tentsio guztiak irakurtzen ditu segidan banku bakoitzetik, errealitatea isU2Bit da.
                                    int benetakoUserId = trueU2Bit ? 2 : 1; 
                                    
                                    Debug.WriteLine($"[BM58] Baliozkoa - Jatorrizko U{userId} -> Benetako U{benetakoUserId} Index: {idx} Data: {BitConverter.ToString(data)}");
                                    records.Add(new BM58RawRecord { Data = data, Index = idx, UserId = benetakoUserId });
                                    lastData = data;
                                    aurkitua = true;
                                    hutsikJarraian = 0;
                                }
                            }
                            break; // Adaptive Retry
                        }
                    } catch { }
                }
                if (!aurkitua) {
                    hutsikJarraian++;
                }
            }
        }

        public Neurketa? KalkulatuBatezbestekoa(List<BM58RawRecord> records, int pazienteId, int memoria)
        {
            if (records == null || records.Count == 0) return null;
            long sSisi = 0, sDia = 0, sPul = 0;
            int count = 0;
            bool filterU2 = (memoria == 2);

            foreach (var r in records)
            {
                if ((filterU2 ? r.IsU2 : r.IsU1))
                {
                    int si = r.Data[0] + 25, di = r.Data[1] + 25, pu = r.Data[2];
                    if (si > 0 && si < 400 && di > 0 && di < 400) {
                        sSisi += si; sDia += di; sPul += pu; count++;
                    }
                }
            }

            if (count == 0) throw new Exception($"Ez da U{memoria} memoriako neurketarik aurkitu (indizeetan oinarrituta).");

            return new Neurketa {
                PazienteId = pazienteId,
                TentsioSistolikoa = (int)Math.Round((double)sSisi / count),
                TentsioDiastolikoa = (int)Math.Round((double)sDia / count),
                PultsuaPpm = (int)Math.Round((double)sPul / count),
                ErregistroData = DateTime.Now,
                Sintomak = $"U{memoria} Batezbestekoa (A) - {count} neurketa (Indize blokea)."
            };
        }

        public class MemoriaInformazioa { public int U1Kopurua, U2Kopurua, Denetara; }
        public MemoriaInformazioa AnalizatuErrekordak(List<BM58RawRecord> records)
        {
            var info = new MemoriaInformazioa { Denetara = records.Count };
            foreach (var r in records) { if (r.UserId == 2) info.U2Kopurua++; else info.U1Kopurua++; }
            return info;
        }

        private IChannel KonektatuGailura(string identifier, bool isHid)
        {
            if (isHid)
            {
                var device = DeviceList.Local.GetHidDevices(Beurer_VID, Beurer_PID).FirstOrDefault();
                if (device == null) throw new Exception("HID gailua ez da aurkitu.");
                var hid = new HidChannel(device);
                hid.ConfigureBaudRate();
                Thread.Sleep(500);

                // Handshake
                bool ok = false;
                foreach (var mode in new[] { HidChannel.ProtocolMode.MicrodiaTunnel, HidChannel.ProtocolMode.ReportId8Raw, HidChannel.ProtocolMode.ReportId0, HidChannel.ProtocolMode.Raw })
                {
                    hid.Mode = mode;
                    for (int i = 0; i < 3; i++) {
                        hid.Write(new byte[] { 0xAA });
                        Thread.Sleep(200);
                        try { if (hid.ReadByte() == 0x55) { ok = true; break; } } catch { }
                    }
                    if (ok) break;
                }
                if (!ok) throw new Exception("Handshake-ak huts egin du.");
                return hid;
            }
            return new SerialChannel(identifier);
        }


    }
}
