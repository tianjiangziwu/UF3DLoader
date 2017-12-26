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

    public float getValue(float ratio)
    {
        throw new NotImplementedException();
    }

    public UnityEngine.ParticleSystem.MinMaxCurve getCurve()
    {
        var curve = new AnimationCurve();
        foreach (CurveAnchor point in anchors)
        {
            curve.AddKey(point.Time, point.Value);
        }
        var ret = new UnityEngine.ParticleSystem.MinMaxCurve(1.0f, curve);  
        return ret;
    }
}