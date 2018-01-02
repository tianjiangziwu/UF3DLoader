using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json.Linq;
using UnityEngine;

public class SpriteSheetEffector : IEffector
{
    private uint rows;
    private uint columns;
    /// <summary>
    /// 单位：s
    /// </summary>
    private float duration;
    private uint count;
    /// <summary>
    /// duration随生命期
    /// </summary>
    private bool lifeDuration;
    private bool randomFrame;

    public void deserialize(JObject data)
    {
        rows = (uint)data["rows"];
        columns = (uint)data["columns"];
        duration = (float)data["duration"];
        count = data["count"] == null ? rows * columns : (uint)data["count"];
        randomFrame = (bool)data["randomFrame"];
        lifeDuration = (bool)data["lifeDuration"];
    }

    public void ApplyToUnityParticleSystem(UnityEngine.ParticleSystem ups, ParticleSystem ps)
    {
        var textureSheetAnimationModule = ups.textureSheetAnimation;
        textureSheetAnimationModule.enabled = true;
        textureSheetAnimationModule.numTilesX = (int)columns;
        textureSheetAnimationModule.numTilesY = (int)rows; 
        textureSheetAnimationModule.animation = ParticleSystemAnimationType.WholeSheet;
        if (randomFrame)
        {
            textureSheetAnimationModule.frameOverTime = new UnityEngine.ParticleSystem.MinMaxCurve(0.0f, 1.0f);
        }
        else if(lifeDuration)
        {
            var curve = new UnityEngine.AnimationCurve();
            curve.AddKey(new Keyframe(0.0f, 0.0f));
            curve.AddKey(new Keyframe(1.0f, 1.0f));
            CurveExtended.CurveExtension.ForceUpdateAllLinearTangents(curve);
            textureSheetAnimationModule.frameOverTime = new UnityEngine.ParticleSystem.MinMaxCurve(1.0f, curve);
            textureSheetAnimationModule.cycleCount = 1;
        }
        else
        {
            var curve = new UnityEngine.AnimationCurve();
            curve.AddKey(new Keyframe(0.0f, 0.0f));
            curve.AddKey(new Keyframe(1.0f, 1.0f));
            CurveExtended.CurveExtension.ForceUpdateAllLinearTangents(curve);
            textureSheetAnimationModule.frameOverTime = new UnityEngine.ParticleSystem.MinMaxCurve(1.0f, curve);
            textureSheetAnimationModule.cycleCount = 1;
        }
    }
}
