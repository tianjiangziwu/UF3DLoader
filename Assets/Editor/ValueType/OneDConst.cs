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

    public float getMaxValue()
    {
        throw new NotImplementedException();
    }

    public float getValue(float ratio)
    {
        throw new NotImplementedException();
    }

    void IDeserialize.deserialize(JObject data)
    {
        throw new NotImplementedException();
    }

    UnityEngine.ParticleSystem.MinMaxCurve IValue.getCurve()
    {
        return new UnityEngine.ParticleSystem.MinMaxCurve(value);
    }

    float IOneDValue.getMaxValue()
    {
        throw new NotImplementedException();
    }

    float IOneDValue.getValue(float ratio)
    {
        throw new NotImplementedException();
    }
}
