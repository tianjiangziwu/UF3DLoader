using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json.Linq;
using UnityEngine;

public class ColorEffector : IEffector
{
    private List<float> keyFrameLifeTime = new List<float>();
    private List<uint> r = new List<uint>();
    private List<uint> g = new List<uint>();
    private List<uint> b = new List<uint>();

    public void deserialize(JObject data)
    {
        var lifeTimeVec = ((string)data["keyFrameLifeTime"]).Split(',');
        var rVec = ((string)data["r"]).Split(',');
        var gVec = ((string)data["g"]).Split(',');
        var bVec = ((string)data["b"]).Split(',');

        for (int i = 0; i < lifeTimeVec.Length; i++){
            addKeyFrame(float.Parse(lifeTimeVec[i]), uint.Parse(rVec[i]), uint.Parse(gVec[i]), uint.Parse(bVec[i]));
        }
    }

    private void addKeyFrame(float ratio, uint rValue, uint gValue, uint bValue)
    {
        ratio = Math.Max(0.0f, Math.Min(1.0f, ratio));
        int i = 0;
        while (i < keyFrameLifeTime.Count && ratio >= keyFrameLifeTime[i])
        {
            i++;
        }
        keyFrameLifeTime.Insert(i, ratio);
        r.Insert(i, rValue);
        g.Insert(i, gValue);
        b.Insert(i, bValue);

    }

    public void ApplyToUnityParticleSystem(UnityEngine.ParticleSystem ups, ParticleSystem ps)
    {
        var colorModule = ups.colorOverLifetime;

        var gradient = new UnityEngine.Gradient();
        gradient.mode = UnityEngine.GradientMode.Blend;
        UnityEngine.GradientColorKey[] gck = new UnityEngine.GradientColorKey[keyFrameLifeTime.Count];

        for (int i = 0; i < keyFrameLifeTime.Count; ++i)
        {
            gck[i].time = keyFrameLifeTime[i];
            gck[i].color = new Color(r[i] / 255.0f, g[i] / 255.0f, b[i] / 255.0f);
        }
        UnityEngine.GradientAlphaKey[] gak;
        if (colorModule.enabled)
        {
            gak = colorModule.color.gradient.alphaKeys;
        }
        else
        {
            gak = new UnityEngine.GradientAlphaKey[2];
            gak[0].time = 0.0f;
            gak[0].alpha = 1.0f;
            gak[1].time = 1.0f;
            gak[1].alpha = 1.0f;
        }
        gradient.SetKeys(gck, gak);
        colorModule.enabled = true;
        colorModule.color = new UnityEngine.ParticleSystem.MinMaxGradient(gradient);
    }
}
