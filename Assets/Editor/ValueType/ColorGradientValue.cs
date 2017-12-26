using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json.Linq;
using UnityEngine;

public class ColorGradientValue : IColorValue
{
    private List<uint> colors = new List<uint>();
    private List<float> colorRatios = new List<float>();


    public List<float> ColorRatios
    {
        get
        {
            return colorRatios;
        }

        set
        {
            colorRatios = value;
        }
    }

    public List<uint> Colors
    {
        get
        {
            return colors;
        }

        set
        {
            colors = value;
        }
    }

    public void deserialize(JObject data)
    {
        var tmpColors = ((string)data["colors"]).Split(',');
        foreach(var color in tmpColors)
        {
            colors.Add(uint.Parse(color));
        }
        var tmpRatios = ((string)data["colorRatios"]).Split(',');
        foreach (var ratio in tmpRatios)
        {
            colorRatios.Add(float.Parse(ratio));
        }
    }

    public uint getValue(float ratio)
    {
        throw new NotImplementedException();
    }

    public UnityEngine.ParticleSystem.MinMaxGradient getGradient()
    {
        return new UnityEngine.ParticleSystem.MinMaxGradient(ValueTypeUtil.GenerateConstGradient(colors.ToArray(), colorRatios.ToArray()));
    }
}
