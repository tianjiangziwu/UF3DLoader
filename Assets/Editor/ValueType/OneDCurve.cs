﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json.Linq;
using UnityEngine;

public class OneDCurve : IOneDValue
{
    /// <summary>
    /// 曲线上的锚点
    /// </summary>
    private List<CurveAnchor> anchors = new List<CurveAnchor>();

    /// <summary>
    /// 曲线的类型
    /// </summary>
    private string curveType = string.Empty;

    public void deserialize(JObject data)
    {
        int count = (int)data["anchorCount"];
        curveType = (string)data["anchorType"];

        for (int i = 0; i < count; i++)
		{
            var value = ((string)data["anchor_" + i]).Split(',');
            anchors.Add(new CurveAnchor(float.Parse(value[0]), float.Parse(value[1])));
        }
        
    }

    public float getMaxValue()
    {
        throw new NotImplementedException();
    }

    public float getValue(float percent)
    {
        float ret = 0;
        int i = 0;
        for (i = 0; i < anchors.Count - 1; i++)
			{
            if (anchors[i + 1].Time > percent)
            {
                ret = anchors[i].interpolate(anchors[i + 1], percent, curveType);
                break;
            }
        }
        return ret;
    }

    public UnityEngine.ParticleSystem.MinMaxCurve getCurve()
    {
        var ret = new UnityEngine.ParticleSystem.MinMaxCurve(1.0f, ValueTypeUtil.GenerateAnimationCurve(anchors));  
        return ret;
    }

    public UnityEngine.ParticleSystem.MinMaxCurve getNegativeCurve()
    {
        var ret = new UnityEngine.ParticleSystem.MinMaxCurve(1.0f, ValueTypeUtil.GenerateAnimationCurve(anchors, true));
        return ret;
    }
}
