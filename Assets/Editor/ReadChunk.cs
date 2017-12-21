using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

public class ReadChunk
{
    private string name = string.Empty;
    private int id = 0;
    private uint length = 0;
    private System.IO.BinaryReader bytes = null;
    private long pos = 0;

    public string Name
    {
        get
        {
            return name;
        }

        set
        {
            name = value;
        }
    }

    public int Id
    {
        get
        {
            return id;
        }

        set
        {
            id = value;
        }
    }

    public uint Length
    {
        get
        {
            return length;
        }

        set
        {
            length = value;
        }
    }

    public long Pos
    {
        get
        {
            return pos;
        }

        set
        {
            pos = value;
        }
    }

    public BinaryReader Bytes
    {
        get
        {
            return bytes;
        }

        set
        {
            bytes = value;
        }
    }

    public ReadChunk(System.IO.BinaryReader input)
    {

        this.name = ReadUtil.ReadUTF(input);
        this.id = input.ReadInt16();
        this.length = input.ReadUInt32();
        this.bytes = input;
        this.pos = input.BaseStream.Position;
    }

    public int BytesAvailable()
    {
        return (int)(this.pos + this.length - this.bytes.BaseStream.Position);
    }

    public void Next()
    {
        this.bytes.BaseStream.Seek(this.pos + this.length, SeekOrigin.Begin);
    }
}
