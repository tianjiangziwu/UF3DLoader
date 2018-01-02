using System;
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

    private float maxCurveAnchorValue = 0.001f;

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
            maxCurveAnchorValue = UnityEngine.Mathf.Max(maxCurveAnchorValue, float.Parse(value[1]));
            anchors.Add(new CurveAnchor(float.Parse(value[0]), float.Parse(value[1])));
        }

        ValueTypeUtil.NormalizeCurveAnchor(anchors, maxCurveAnchorValue);
    }

    public float getMaxValue()
    {
        return maxCurveAnchorValue;
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

    public UnityEngine.ParticleSystem.MinMaxCurve getCurve(ValueTypeUtil.CurveType flag = ValueTypeUtil.CurveType.Normal)
    {
        UnityEngine.ParticleSystem.MinMaxCurve ret;
        if (flag == ValueTypeUtil.CurveType.Rotation)
        {
            ret = new UnityEngine.ParticleSystem.MinMaxCurve(maxCurveAnchorValue, ValueTypeUtil.GenerateAnimationCurve(anchors, false, UnityEngine.Mathf.PI / 180.0f));
        }
        else
        {
            ret = new UnityEngine.ParticleSystem.MinMaxCurve(maxCurveAnchorValue, ValueTypeUtil.GenerateAnimationCurve(anchors));
        }
        return ret;
    }

    public UnityEngine.ParticleSystem.MinMaxCurve getNegativeCurve(ValueTypeUtil.CurveType flag = ValueTypeUtil.CurveType.Normal)
    {
        UnityEngine.ParticleSystem.MinMaxCurve ret;
        if (flag == ValueTypeUtil.CurveType.Rotation)
        {
            ret = new UnityEngine.ParticleSystem.MinMaxCurve(maxCurveAnchorValue, ValueTypeUtil.GenerateAnimationCurve(anchors, true, UnityEngine.Mathf.PI / 180.0f));
        }
        else
        {
            ret = new UnityEngine.ParticleSystem.MinMaxCurve(maxCurveAnchorValue, ValueTypeUtil.GenerateAnimationCurve(anchors, true));
        }
        return ret;
    }
}
