using System;
using System.Linq;
using HidSharp;
using System.Threading;

class Program {
    static void Main() {
        var loader = new DeviceList();
        var dev = DeviceList.Local.GetHidDevices(0x0C45, 0x7406).FirstOrDefault();
        if (dev == null) { Console.WriteLine("Device not found."); return; }

        using (var stream = dev.Open()) {
            stream.ReadTimeout = 1000;
            
            // 1. Try SetFeature (Baud rate)
            Console.WriteLine("Configuring Baud Rate...");
            byte[] baud = new byte[6] { 0x02, 0x01, 0xC0, 0x12, 0x00, 0x00 };
            try { stream.SetFeature(baud); } catch(Exception ex) { Console.WriteLine("SetFeature Error: " + ex.Message); }
            Thread.Sleep(500);

            // 2. Try Handshake via Tunnel (ID 8)
            byte[] tunnel = new byte[9] { 0x08, 0x01, 0xAA, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 };
            Console.WriteLine("Sending Handshake (0xAA) via ID 8...");
            stream.Write(tunnel);

            // 3. Listen for response
            Console.WriteLine("Waiting for response (any ID)...");
            byte[] resp = new byte[9];
            try {
                int n = stream.Read(resp);
                Console.WriteLine("Received " + n + " bytes: " + BitConverter.ToString(resp, 0, n));
            } catch (Exception ex) {
                Console.WriteLine("Read Error: " + ex.Message);
            }
        }
    }
}
