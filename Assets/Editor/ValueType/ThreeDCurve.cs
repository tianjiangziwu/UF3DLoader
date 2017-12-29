using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json.Linq;
using UnityEngine;

public class ThreeDCurve : IThreeDValue
{
    private List<CurveAnchor> anchorsX = new List<CurveAnchor>();
    private List<CurveAnchor> anchorsY = new List<CurveAnchor>();
    private List<CurveAnchor> anchorsZ = new List<CurveAnchor>();

    private string curveTypeX = string.Empty;
    private string curveTypeY = string.Empty;
    private string curveTypeZ = string.Empty;

    private List<float> maxXYZ = new List<float> { 0.001f, 0.001f, 0.001f };

    public void deserialize(JObject data)
    {
        int count = (int)data["anchorXCount"];
        curveTypeX = (string)data["anchorXType"];

        for (int i = 0; i < count; i++)
	    {
            var value = ((string)data["anchorX" + i]).Split(',');
            anchorsX.Add(new CurveAnchor(float.Parse(value[0]), float.Parse(value[1])));
            maxXYZ[0] = UnityEngine.Mathf.Max(maxXYZ[0], float.Parse(value[1]));
        }

        ValueTypeUtil.NormalizeCurveAnchor(anchorsX, maxXYZ[0]);

        count = (int)data["anchorYCount"];
        curveTypeY = (string)data["anchorYType"];

        for (int i = 0; i < count; i++)
        {
            var value = ((string)data["anchorY" + i]).Split(',');
            anchorsY.Add(new CurveAnchor(float.Parse(value[0]), float.Parse(value[1])));
            maxXYZ[1] = UnityEngine.Mathf.Max(maxXYZ[1], float.Parse(value[1]));
        }

        ValueTypeUtil.NormalizeCurveAnchor(anchorsY, maxXYZ[1]);

        count = (int)data["anchorZCount"];
        curveTypeZ = (string)data["anchorZType"];

        for (int i = 0; i < count; i++)
        {
            var value = ((string)data["anchorZ" + i]).Split(',');
            anchorsZ.Add(new CurveAnchor(float.Parse(value[0]), float.Parse(value[1])));
            maxXYZ[2] = UnityEngine.Mathf.Max(maxXYZ[2], float.Parse(value[1]));
        }

        ValueTypeUtil.NormalizeCurveAnchor(anchorsZ, maxXYZ[2]);
    }

    public List<UnityEngine.ParticleSystem.MinMaxCurve> getThreeDCurve()
    {
        var ret = new List<UnityEngine.ParticleSystem.MinMaxCurve>();
        List<List<CurveAnchor>> tmp = new List<List<CurveAnchor>> { anchorsX, anchorsY, anchorsZ };
        for (int i = 0; i < tmp.Count; ++i)
        {
            ret.Add(new UnityEngine.ParticleSystem.MinMaxCurve(maxXYZ[i], ValueTypeUtil.GenerateAnimationCurve(tmp[i])));
        }
        return ret;
    }

    public Vector3 getValue(float ratio)
    {
        var tdc = getThreeDCurve();
        return new Vector3(tdc[0].Evaluate(ratio), tdc[1].Evaluate(ratio), tdc[2].Evaluate(ratio));
    }
}
