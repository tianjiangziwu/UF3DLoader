using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json.Linq;
using UnityEngine;

public class ColorRandomBetweenValue : IColorValue
{
    private uint color0 = 0;
    private uint color1 = 0;

    public uint Color0
    {
        get
        {
            return color0;
        }

        set
        {
            color0 = value;
        }
    }

    public uint Color1
    {
        get
        {
            return color1;
        }

        set
        {
            color1 = value;
        }
    }

    public void deserialize(JObject data)
    {
        color0 = (uint)data["color0"];
        color1 = (uint)data["color1"];
    }

    public UnityEngine.ParticleSystem.MinMaxGradient getGradient()
    {
        
        return new UnityEngine.ParticleSystem.MinMaxGradient(ValueTypeUtil.GenerateConstGradient(new uint[] {color0, color0}, new float[] { 0.0f, 1.0f}), ValueTypeUtil.GenerateConstGradient(new uint[] { color1, color1 }, new float[] { 0.0f, 1.0f }));
    }

    

    public uint getValue(float ratio)
    {
        throw new NotImplementedException();
    }
}
