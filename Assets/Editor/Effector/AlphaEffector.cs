using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json.Linq;

public class AlphaEffector : IEffector
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
}
