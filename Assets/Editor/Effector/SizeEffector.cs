using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json.Linq;
using UnityEngine;

public class SizeEffector : IEffector
{
    private List<float> keyFrameLifeTime = new List<float>();
    private List<float> sizeX = new List<float>();
    private List<float> sizeY = new List<float>();
    private List<float> sizeZ = new List<float>();

    public void ApplyToUnityParticleSystem(UnityEngine.ParticleSystem ups, ParticleSystem ps)
    {
        var sizeModule = ups.sizeOverLifetime;
        sizeModule.enabled = true;
        sizeModule.separateAxes = true;
        var curvX = new UnityEngine.AnimationCurve();
        var curvY = new UnityEngine.AnimationCurve();
        var curvZ = new UnityEngine.AnimationCurve();
        //重复的key第二次不会添加
        for (int i = keyFrameLifeTime.Count - 1; i >= 0; --i)
        {
            curvX.AddKey(keyFrameLifeTime[i], sizeX[i]);
            curvY.AddKey(keyFrameLifeTime[i], sizeY[i]);
            curvZ.AddKey(keyFrameLifeTime[i], sizeZ[i]);
        }
        sizeModule.x = new UnityEngine.ParticleSystem.MinMaxCurve(1.0f, curvX);
        sizeModule.y = new UnityEngine.ParticleSystem.MinMaxCurve(1.0f, curvY);
        sizeModule.z = new UnityEngine.ParticleSystem.MinMaxCurve(1.0f, curvZ);
    }

    public void deserialize(JObject data)
    {
        var lifeTimeVec = StringUtil.SplitString<float>((string)data["keyFrameLifeTime"], new char[] { ',' });

        var sizeXArray = StringUtil.SplitString<float>((string)data["sx"], new char[] { ',' });

        var sizeZArray = StringUtil.SplitString<float>((string)data["sy"], new char[] { ',' });

        var sizeYArray = StringUtil.SplitString<float>((string)data["sz"], new char[] { ',' });

        for (int i = 0; i < lifeTimeVec.Count; ++i)
        {
            addKeyFrame(lifeTimeVec[i], sizeXArray[i], sizeYArray[i], sizeZArray[i]);
        }
    }

    private void addKeyFrame(float ratio, float valueX, float valueY, float valueZ)
    {
        ratio = Math.Max(0.0f, Math.Min(1.0f, ratio));
        int i = 0;
        while (i < keyFrameLifeTime.Count && ratio >= keyFrameLifeTime[i])
        {
            i++;
        }
        keyFrameLifeTime.Insert(i, ratio);
        sizeX.Insert(i, valueX);
        sizeY.Insert(i, valueY);
        sizeZ.Insert(i, valueZ);
    }
}
