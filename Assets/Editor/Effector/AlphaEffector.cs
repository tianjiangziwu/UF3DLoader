﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json.Linq;
using UnityEngine;

public class AlphaEffector : IEffector, IFrame
{
    private List<float> keyFrameLifeTime = new List<float>();
    private List<uint> a = new List<uint>();

    public void deserialize(JObject data)
    {
        var lifeTimeVec = ((string)data["keyFrameLifeTime"]).Split(',');
        var aVec = ((string)data["a"]).Split(',');

        for (int i = 0; i < lifeTimeVec.Length; i++){
            addKeyFrame(float.Parse(lifeTimeVec[i]), uint.Parse(aVec[i]));
        }
        ModifyFrame();
    }

    private void addKeyFrame(float ratio, uint alpha)
    {
        ratio = Math.Max(0.0f, Math.Min(1.0f, ratio));
        int i = 0;
        while (i < keyFrameLifeTime.Count && ratio >= keyFrameLifeTime[i])
        {
            i++;
        }
        keyFrameLifeTime.Insert(i, ratio);
        a.Insert(i, alpha);
    }

    public void ApplyToUnityParticleSystem(UnityEngine.ParticleSystem ups, ParticleSystem ps)
    {
        var colorModule = ups.colorOverLifetime;

        var gradient = new UnityEngine.Gradient();
        gradient.mode = UnityEngine.GradientMode.Blend;
        UnityEngine.GradientAlphaKey[] gak = new UnityEngine.GradientAlphaKey[keyFrameLifeTime.Count];

        for (int i = 0; i < keyFrameLifeTime.Count; ++i)
        {
            gak[i].time = keyFrameLifeTime[i];
            gak[i].alpha = a[i] / 255.0f;
        }
        UnityEngine.GradientColorKey[] gck;
        if (colorModule.enabled)
        {
            gck = colorModule.color.gradient.colorKeys;
        }
        else
        {
            gck = new UnityEngine.GradientColorKey[2];
            gck[0].time = 0.0f;
            gck[0].color = ps.Emitter.color.getValue(gck[0].time);
            gck[1].time = 1.0f;
            gck[1].color = ps.Emitter.color.getValue(gck[1].time);
        }
        gradient.SetKeys(gck, gak);
        colorModule.enabled = true;
        colorModule.color = new UnityEngine.ParticleSystem.MinMaxGradient(gradient);
    }

    public const int gpuEffectorRgbaKeyFrameMax = 5;

    public void ModifyFrame()
    {
        int i = 0;
        int ki = 0;
        float[] tmpTime = new float[gpuEffectorRgbaKeyFrameMax];
        uint[] tmpa = new uint[gpuEffectorRgbaKeyFrameMax];
        while (i < gpuEffectorRgbaKeyFrameMax)
        {
            if (ki < keyFrameLifeTime.Count)
            {
                tmpTime[i] = keyFrameLifeTime[ki];
                tmpa[i] = a[ki];
            }
            else
            {
                tmpTime[i] = keyFrameLifeTime[keyFrameLifeTime.Count - 1];
                tmpa[i] = a[keyFrameLifeTime.Count - 1];
            }
            //前后帧时间夹逼到0-1
            if (i == 0)
            {
                tmpTime[i] = 0;
            }
            else if (i == gpuEffectorRgbaKeyFrameMax - 1)
            {
                tmpTime[i] = 1;
            }
            ++ki;
            ++i;
        }
        keyFrameLifeTime = tmpTime.ToList();
        a = tmpa.ToList();
    }
}
