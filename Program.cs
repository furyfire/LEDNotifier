using HidSharp;
using System;

byte color_code = 0x0;

if (args.Length != 1)
{
    Console.WriteLine("LEDNotifier off | blue | red | green | aqua | purple | yellow | white | random");
    Console.WriteLine("No or too many arguments");
    return 1;
}

Random rnd = new Random();
switch (args[0])
{
    case "off":
        color_code = 0;
        break;
    case "blue":
        color_code = 1;
        break;
    case "red":
        color_code = 2;
        break;
    case "green":
        color_code = 3;
        break;
    case "aqua":
        color_code = 4;
        break;
    case "purple":
        color_code = 5;
        break;
    case "yellow":
        color_code = 6;
        break;
    case "white":
        color_code = 7;
        break;
    case "random":
        color_code = (byte)rnd.Next(1, 7);
        break;
    default:
        Console.WriteLine("Invalid argument");
        return 1;
}


HidDevice hidDevice;
var ret = DeviceList.Local.TryGetHidDevice(out hidDevice, vendorID: 0x1294, productID: 0x1320);

if (ret == false)
{
    Console.WriteLine("Device not found");
    return 1;
}

DeviceStream deviceStream;
ret = hidDevice.TryOpen(out deviceStream);

if (ret == false)
{
    Console.WriteLine("Failed to open stream");
    return 1;
}


byte[] data = { 0xff, color_code };
deviceStream.Write(data, 0, data.Length);

return 0;