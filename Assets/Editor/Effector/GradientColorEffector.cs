using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json.Linq;
using UnityEngine;

public class GradientColorEffector : IEffector
{
    private List<uint> colors = new List<uint>();
    private List<float> alphas = new List<float>();
    private List<float> colorRatios = new List<float>();
    private List<float> alphaRatios = new List<float>();

    private bool justAlpha = false;

    public void deserialize(JObject data)
    {
        colors = StringUtil.SplitString<uint>((string)data["colors"], new char[]{','});
        alphas = StringUtil.SplitString<float>((string)data["alphas"], new char[] { ',' });

        colorRatios = StringUtil.SplitString<float>((string)data["colorRatios"], new char[] { ',' });
        alphaRatios = StringUtil.SplitString<float>((string)data["alphaRatios"], new char[] { ',' });

        justAlpha = (bool)data["justAlpha"];
    }

    public void ApplyToUnityParticleSystem(UnityEngine.ParticleSystem ups, ParticleSystem ps)
    {
        var colorModule = ups.colorOverLifetime;

        var gradient = new UnityEngine.Gradient();
        gradient.mode = UnityEngine.GradientMode.Blend;
        UnityEngine.GradientColorKey[] gck;
       
        if (justAlpha)
        {
            gck = new UnityEngine.GradientColorKey[2];
            gck[0].time = 0.0f;
            gck[0].color = ps.Emitter.color.getValue(gck[0].time);
            gck[1].time = 1.0f;
            gck[1].color = ps.Emitter.color.getValue(gck[1].time);
        }
        else
        {
            gck = new UnityEngine.GradientColorKey[colors.Count];

            for (int i = 0; i < colors.Count; ++i)
            {
                gck[i].time = colorRatios[i] / 255.0f;
                gck[i].color = ValueTypeUtil.GetColor(colors[i]);
            }
        }

        UnityEngine.GradientAlphaKey[] gak = new UnityEngine.GradientAlphaKey[alphaRatios.Count];

        for (int i = 0; i < alphaRatios.Count; ++i)
        {
            gak[i].time = alphaRatios[i] / 255.0f;
            gak[i].alpha = alphas[i];
        }

        gradient.SetKeys(gck, gak);
        colorModule.enabled = true;
        colorModule.color = new UnityEngine.ParticleSystem.MinMaxGradient(gradient);
    }
}
