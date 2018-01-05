using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

public class ConeShape : IShape
{
    // 表面
    private bool shell;
    // 椎体
    private bool volum;
    // 倾角
    private float angle;
    // 高度
    private float length;
    // 半径
    private float radius;

    public bool Shell
    {
        get
        {
            return shell;
        }

        set
        {
            shell = value;
        }
    }

    public bool Volum
    {
        get
        {
            return volum;
        }

        set
        {
            volum = value;
        }
    }

    public float Angle
    {
        get
        {
            return angle;
        }

        set
        {
            angle = value;
        }
    }

    public float Length
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

    public float Radius
    {
        get
        {
            return radius;
        }

        set
        {
            radius = value;
        }
    }

    public void deserialize(Newtonsoft.Json.Linq.JObject data)
    {
        Shell = (bool)data["shell"];
        Volum = (bool)data["volum"];
        Angle = (float)data["angle"];
        Length = (float)data["length"] * Uf3dLoader.vertexScale;
        Radius = (float)data["radius"] * Uf3dLoader.vertexScale;
    }
}
