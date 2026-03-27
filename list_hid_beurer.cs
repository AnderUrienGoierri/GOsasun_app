using System;
using System.Linq;
using HidSharp;

class Program {
    static void Main() {
        var devices = DeviceList.Local.GetHidDevices(0x0C45).ToList();
        Console.WriteLine($"Found {devices.Count} Microdia/Beurer HID devices.");
        foreach (var dev in devices) {
            Console.WriteLine($"VID: 0x{dev.VendorID:X4} PID: 0x{dev.ProductID:X4} Name: {dev.GetProductName()} Path: {dev.DevicePath}");
            Console.WriteLine($"  MaxI: {dev.MaxInputReportLength} MaxO: {dev.MaxOutputReportLength} MaxF: {dev.MaxFeatureReportLength}");
        }
    }
}
