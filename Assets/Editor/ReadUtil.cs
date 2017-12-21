using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class ReadUtil
{
    public static string ReadUTF(System.IO.BinaryReader input)
    {
        ushort len = input.ReadUInt16();
        byte[] data = input.ReadBytes(len);
        return System.Text.Encoding.UTF8.GetString(data);
    }
}
