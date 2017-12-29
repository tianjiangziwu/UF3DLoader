using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json.Linq;
using UnityEngine;

public class UVEffector : IEffector, IFrame
{
    // useless
    private float scaleU = 1.0f;
    // useless
    private float scaleV = 1.0f;
    private List<float> keyFrameLifeTime = new List<float>();
    private List<float> u = new List<float>();
    private List<float> v = new List<float>();

    public void deserialize(JObject data)
    {
        
        var lifeTimeVec = StringUtil.SplitString<float>((string)data["keyFrameLifeTime"], new char[] { ',' });
        var uVec = StringUtil.SplitString<float>((string)data["u"], new char[] { ',' });
        var vVec = StringUtil.SplitString<float>((string)data["v"], new char[] { ',' });

        for (int i = 0; i < lifeTimeVec.Count; ++i)
        {
            addKeyFrame(lifeTimeVec[i], uVec[i], vVec[i]);
        }

        scaleU = (float)data["scaleU"];
        scaleV = (float)data["scaleV"];
		ModifyFrame();
    }

    private void addKeyFrame(float ratio, float uVal, float vVal)
    {
        ratio = Math.Max(0.0f, Math.Min(1.0f, ratio));
        int i = 0;
        while (i < keyFrameLifeTime.Count && ratio >= keyFrameLifeTime[i])
        {
            i++;
        }
        keyFrameLifeTime.Insert(i, ratio);
        u.Insert(i, uVal);
        v.Insert(i, vVal);
    }

    public void ApplyToUnityParticleSystem(UnityEngine.ParticleSystem ups, ParticleSystem ps)
    {
        throw new NotImplementedException();
    }

    public const int gpuEffectorKeyFrameMax = 3;

    public void ModifyFrame()
    {
        int i = 0;
        int ki = 0;
        float[] tmpTime = new float[gpuEffectorKeyFrameMax];
        float[] tmpu = new float[gpuEffectorKeyFrameMax];
        float[] tmpv = new float[gpuEffectorKeyFrameMax];
        while (i < gpuEffectorKeyFrameMax)
        {
            if (ki < keyFrameLifeTime.Count)
            {
                tmpu[i] = u[ki];
                tmpv[i] = v[ki];
                tmpTime[i] = keyFrameLifeTime[ki];
            }
            else
            {
                tmpu[i] = u[keyFrameLifeTime.Count - 1];
                tmpv[i] = v[keyFrameLifeTime.Count - 1];
                tmpTime[i] = keyFrameLifeTime[keyFrameLifeTime.Count - 1];
            }
            //前后帧时间夹逼到0-1
            if (i == 0)
                tmpTime[i] = 0;
            else if (i == gpuEffectorKeyFrameMax - 1)
                tmpTime[i] = 1;

            if (i == 0 && keyFrameLifeTime[ki] > 0)
            {

            }
            else
            {
                ++ki;
            }
            ++i;
        }

        keyFrameLifeTime = tmpTime.ToList();
        u = tmpu.ToList();
        v = tmpv.ToList();
    }
}
