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
    private List<float> maxXYZ = new List<float>(3) { 0.001f, 0.001f, 0.001f };

    private const int gpuEffectorSizeKeyFrameMax = 4;

    public void ApplyToUnityParticleSystem(UnityEngine.ParticleSystem ups, ParticleSystem ps)
    {
        var sizeModule = ups.sizeOverLifetime;
        sizeModule.enabled = true;
        sizeModule.separateAxes = true;
        int i = 0;
        int ki = 0;
        float[] tmpX = new float[gpuEffectorSizeKeyFrameMax];
        float[] tmpY = new float[gpuEffectorSizeKeyFrameMax];
        float[] tmpZ = new float[gpuEffectorSizeKeyFrameMax];
        float[] tmpTime = new float[gpuEffectorSizeKeyFrameMax];


        while (i < gpuEffectorSizeKeyFrameMax)
        {
            if (ki < keyFrameLifeTime.Count)
            {
                tmpX[i] = sizeX[ki];
                tmpY[i] = sizeY[ki];
                tmpZ[i] = sizeZ[ki];
                tmpTime[i] = keyFrameLifeTime[ki];
            }
            else
            {
                tmpX[i] = sizeX[keyFrameLifeTime.Count - 1];
                tmpY[i] = sizeY[keyFrameLifeTime.Count - 1];
                tmpZ[i] = sizeZ[keyFrameLifeTime.Count - 1];
                tmpTime[i] = keyFrameLifeTime[keyFrameLifeTime.Count - 1];
            }

            if (i == 0)
            {
                tmpTime[i] = 0;
            }
            else if (i == gpuEffectorSizeKeyFrameMax - 1)
            {
                tmpTime[i] = 1;
            }

            // 如果第一帧的时间不为0，就将第一帧重复一次，第一次的时间为0，第二次的时间为原有时间，这样会丢弃最后一帧
            if (i == 0 && keyFrameLifeTime[ki] > 0)
            {

            }
            else
            {
                ++ki;
            }
            ++i;
        }
        var curvX = new UnityEngine.AnimationCurve();
        var curvY = new UnityEngine.AnimationCurve();
        var curvZ = new UnityEngine.AnimationCurve();
        //重复的key第二次不会添加
        for (i = tmpTime.Length - 1; i >= 0; --i)
        {
            curvX.AddKey(tmpTime[i], tmpX[i]);
            curvY.AddKey(tmpTime[i], tmpY[i]);
            curvZ.AddKey(tmpTime[i], tmpZ[i]);
        }
        sizeModule.x = new UnityEngine.ParticleSystem.MinMaxCurve(maxXYZ[0], curvX);
        sizeModule.y = new UnityEngine.ParticleSystem.MinMaxCurve(maxXYZ[1], curvY);
        sizeModule.z = new UnityEngine.ParticleSystem.MinMaxCurve(maxXYZ[2], curvZ);
    }

    public void deserialize(JObject data)
    {
        var lifeTimeVec = StringUtil.SplitString<float>((string)data["keyFrameLifeTime"], new char[] { ',' });

        var sizeXArray = StringUtil.SplitString<float>((string)data["sx"], new char[] { ',' });

        var sizeZArray = StringUtil.SplitString<float>((string)data["sy"], new char[] { ',' });

        var sizeYArray = StringUtil.SplitString<float>((string)data["sz"], new char[] { ',' });

        for (int i = 0; i < lifeTimeVec.Count; ++i)
        {
            maxXYZ[0] = UnityEngine.Mathf.Max(maxXYZ[0], UnityEngine.Mathf.Abs(sizeXArray[i]));
            maxXYZ[1] = UnityEngine.Mathf.Max(maxXYZ[1], UnityEngine.Mathf.Abs(sizeYArray[i]));
            maxXYZ[2] = UnityEngine.Mathf.Max(maxXYZ[2], UnityEngine.Mathf.Abs(sizeZArray[i]));

        }

        for (int i = 0; i < lifeTimeVec.Count; ++i)
        {
            addKeyFrame(lifeTimeVec[i], sizeXArray[i] / maxXYZ[0], sizeYArray[i] / maxXYZ[1], sizeZArray[i] / maxXYZ[2]);
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
