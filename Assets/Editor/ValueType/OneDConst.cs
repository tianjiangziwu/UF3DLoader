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

    public UnityEngine.ParticleSystem.MinMaxCurve getCurve(ValueTypeUtil.CurveType flag = ValueTypeUtil.CurveType.Normal)
    {
        UnityEngine.ParticleSystem.MinMaxCurve ret;
        if (flag == ValueTypeUtil.CurveType.Rotation)
        {
            ret = new UnityEngine.ParticleSystem.MinMaxCurve(value * UnityEngine.Mathf.PI / 180.0f);
        }
        else
        {
            ret = new UnityEngine.ParticleSystem.MinMaxCurve(value);
        }
        return ret;
    }

    public float getMaxValue()
    {
        return value;
    }

    public UnityEngine.ParticleSystem.MinMaxCurve getNegativeCurve(ValueTypeUtil.CurveType flag = ValueTypeUtil.CurveType.Normal)
    {
        UnityEngine.ParticleSystem.MinMaxCurve ret;
        if (flag == ValueTypeUtil.CurveType.Rotation)
        {
            ret = new UnityEngine.ParticleSystem.MinMaxCurve(-value * UnityEngine.Mathf.PI / 180.0f);
        }
        else
        {
            ret = new UnityEngine.ParticleSystem.MinMaxCurve(-value);
        }
        return ret;
    }

    public float getValue(float ratio)
    {
        return value;
    }
}
