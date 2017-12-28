using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json.Linq;
using UnityEngine;

public class OneDConst : IOneDValue
{
    /// <summary>
    /// 一维固定值
    /// </summary>
    private float value = 0.0f;

    public void deserialize(JObject data)
    {
        value = (float)data["value"];
    }

    public UnityEngine.ParticleSystem.MinMaxCurve getCurve()
    {
        var ret = new UnityEngine.ParticleSystem.MinMaxCurve(value);
        return ret;
    }

    public float getMaxValue()
    {
        throw new NotImplementedException();
    }

    public UnityEngine.ParticleSystem.MinMaxCurve getNegativeCurve()
    {
        var ret = new UnityEngine.ParticleSystem.MinMaxCurve(-value);
        return ret;
    }

    public float getValue(float ratio)
    {
        return value;
    }
}
