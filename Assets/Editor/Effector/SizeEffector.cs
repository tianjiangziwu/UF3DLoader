using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json.Linq;

public class SizeEffector : IEffector
{
    private List<float> keyFrameLifeTime = new List<float>();
    private List<float> sizeX = new List<float>();
    private List<float> sizeY = new List<float>();
    private List<float> sizeZ = new List<float>();

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
