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

    public void deserialize(Newtonsoft.Json.Linq.JObject data)
    {
        shell = (bool)data["shell"];
        volum = (bool)data["volum"];
        angle = (float)data["angle"];
        length = (float)data["length"];
        radius = (float)data["radius"];
    }
}
