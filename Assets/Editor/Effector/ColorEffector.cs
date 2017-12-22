using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json.Linq;

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
}
