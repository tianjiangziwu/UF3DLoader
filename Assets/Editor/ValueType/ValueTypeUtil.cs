﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class ValueTypeUtil
{
    public enum CurveType
    {
        Normal = 0,
        Rotation = 1
    }

    public static UnityEngine.Color GetColor(uint initialColor)
    {
        
        float r = 1.0f * ((initialColor & 0xff0000) >> 16) / 0xff;
        float g = 1.0f * ((initialColor & 0x00ff00) >> 8) / 0xff;
        float b = 1.0f * (initialColor & 0x0000ff) / 0xff;
        return new UnityEngine.Color(r, g, b);
    }

    public static UnityEngine.Gradient GenerateConstGradient(uint[] colors, float[] colorRatios)
    {
        var gradient = new UnityEngine.Gradient();
        gradient.mode = UnityEngine.GradientMode.Blend;
        UnityEngine.GradientColorKey[] gck = new UnityEngine.GradientColorKey[colors.Length];
        UnityEngine.GradientAlphaKey[] gak = new UnityEngine.GradientAlphaKey[colors.Length];
        for (int i = 0; i < colors.Length; ++i)
        {
            gck[i].time = colorRatios[i];
            gck[i].color = ValueTypeUtil.GetColor(colors[i]);
            gak[i].time = colorRatios[i];
            gak[i].alpha = 1.0f;
        }
        gradient.SetKeys(gck, gak);
        return gradient;
    }

    public static UnityEngine.AnimationCurve GenerateAnimationCurve(List<CurveAnchor> anchors, bool negative = false, float valueScale = 1.0f)
    {
        var curve = new UnityEngine.AnimationCurve();
        foreach (CurveAnchor point in anchors)
        {
            curve.AddKey(point.Time, valueScale * point.Value * (negative ? -1.0f : 1.0f));
        }
        CurveExtended.CurveExtension.ForceUpdateAllLinearTangents(curve);
        return curve;
    }

    public static void NormalizeCurveAnchor(List<CurveAnchor> anchors, float scale)
    {
        for (int i = 0; i < anchors.Count; ++i)
        {
            anchors[i].Value = anchors[i].Value / scale;
        }
    }
}
