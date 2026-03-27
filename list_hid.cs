using System;
using System.Linq;
using HidSharp;

class Program {
    static void Main() {
        var devices = DeviceList.Local.GetHidDevices();
        Console.WriteLine($"Found {devices.Count()} HID devices.");
        foreach (var dev in devices) {
            try {
                Console.WriteLine($"VID: 0x{dev.VendorID:X4} PID: 0x{dev.ProductID:X4} Name: {dev.GetProductName()} OriginalName: {dev.DevicePath}");
                Console.WriteLine($"  MaxInput: {dev.MaxInputReportLength} MaxOutput: {dev.MaxOutputReportLength} MaxFeature: {dev.MaxFeatureReportLength}");
            } catch {
                Console.WriteLine($"VID: 0x{dev.VendorID:X4} PID: 0x{dev.ProductID:X4} (Could not read details)");
            }
        }
    }
}
