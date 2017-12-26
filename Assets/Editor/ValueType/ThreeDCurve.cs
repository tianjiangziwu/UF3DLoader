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

    public void deserialize(JObject data)
    {
        int count = (int)data["anchorXCount"];
        curveTypeX = (string)data["anchorXType"];

        for (int i = 0; i < count; i++)
	    {
            var value = ((string)data["anchorX" + i]).Split(',');
            anchorsX.Add(new CurveAnchor(float.Parse(value[0]), float.Parse(value[1])));
        }

        count = (int)data["anchorYCount"];
        curveTypeY = (string)data["anchorYType"];

        for (int i = 0; i < count; i++)
        {
            var value = ((string)data["anchorY" + i]).Split(',');
            anchorsY.Add(new CurveAnchor(float.Parse(value[0]), float.Parse(value[1])));
        }

        count = (int)data["anchorZCount"];
        curveTypeZ = (string)data["anchorZType"];

        for (int i = 0; i < count; i++)
        {
            var value = ((string)data["anchorZ" + i]).Split(',');
            anchorsZ.Add(new CurveAnchor(float.Parse(value[0]), float.Parse(value[1])));
        }
    }

    public Vector3 getValue(float ratio)
    {
        throw new NotImplementedException();
    }

    void IDeserialize.deserialize(JObject data)
    {
        throw new NotImplementedException();
    }

    UnityEngine.ParticleSystem.MinMaxCurve IValue.getCurve()
    {
        throw new NotImplementedException();
    }

    Vector3 IThreeDValue.getValue(float ratio)
    {
        throw new NotImplementedException();
    }
}
