using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json.Linq;
using UnityEngine;

public class OneDRandom : IOneDValue
{
    private float min = 0.0f;
    private float max = 0.0f;

    public void deserialize(JObject data)
    {
        min = (float)data["min"];
        max = (float)data["max"];
    }

    public float getMaxValue()
    {
        throw new NotImplementedException();
    }

    public float getValue(float ratio)
    {
        return min + (max - min) * UnityEngine.Random.Range(0.0f, 1.0f);
    }

    public UnityEngine.ParticleSystem.MinMaxCurve getCurve()
    {
        var ret = new UnityEngine.ParticleSystem.MinMaxCurve(min, max);
        return ret;
    }

    public UnityEngine.ParticleSystem.MinMaxCurve getNegativeCurve()
    {
        var ret = new UnityEngine.ParticleSystem.MinMaxCurve(-max, -min);
        return ret;
    }
}
