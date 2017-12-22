using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json.Linq;

public class UVEffector : IEffector
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
}
