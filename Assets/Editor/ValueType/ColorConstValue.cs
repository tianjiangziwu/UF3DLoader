using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json.Linq;
using UnityEngine;

public class ColorConstValue : IColorValue
{
    private uint value = 0;

    public uint Value
    {
        get
        {
            return value;
        }

        set
        {
            this.value = value;
        }
    }

    public void deserialize(JObject data)
    {
        value = (uint)data["value"];
    }

    public UnityEngine.ParticleSystem.MinMaxGradient getGradient()
    {
        return new UnityEngine.ParticleSystem.MinMaxGradient(ValueTypeUtil.GetColor(value));
    }

    public UnityEngine.Color getValue(float ratio)
    {
        return getGradient().Evaluate(ratio);
    }

}
