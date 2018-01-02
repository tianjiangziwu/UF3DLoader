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
        if (max < min)
        {
            var tmp = max;
            max = min;
            min = tmp;
        }
    }

    public float getMaxValue()
    {
        return max;
    }

    public float getValue(float ratio)
    {
        return min + (max - min) * UnityEngine.Random.Range(0.0f, 1.0f);
    }

    public UnityEngine.ParticleSystem.MinMaxCurve getCurve(ValueTypeUtil.CurveType flag = ValueTypeUtil.CurveType.Normal, float scale = 1.0f)
    {
        UnityEngine.ParticleSystem.MinMaxCurve ret;
        if (flag == ValueTypeUtil.CurveType.Rotation)
        {
            ret = new UnityEngine.ParticleSystem.MinMaxCurve(min * UnityEngine.Mathf.PI / 180.0f * scale, max * UnityEngine.Mathf.PI / 180.0f * scale);
        }
        else
        {
            ret = new UnityEngine.ParticleSystem.MinMaxCurve(min * scale, max * scale);
        }
        return ret;
    }

    public UnityEngine.ParticleSystem.MinMaxCurve getNegativeCurve(ValueTypeUtil.CurveType flag = ValueTypeUtil.CurveType.Normal, float scale = 1.0f)
    {
        UnityEngine.ParticleSystem.MinMaxCurve ret;
        if (flag == ValueTypeUtil.CurveType.Rotation)
        {
            ret = new UnityEngine.ParticleSystem.MinMaxCurve(-max * UnityEngine.Mathf.PI / 180.0f * scale, -min * UnityEngine.Mathf.PI / 180.0f * scale);
        }
        else
        {
            ret = new UnityEngine.ParticleSystem.MinMaxCurve(-max * scale, -min * scale);
        }
        return ret;
    }
}
