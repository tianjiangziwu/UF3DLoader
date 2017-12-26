using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json.Linq;
using UnityEngine;

public class ThreeDSphere : IThreeDValue
{
    private float innerRadius = 0.0f;
    private float outerRadius = 0.0f;
    private Vector3 center;

    public void deserialize(JObject data)
    {
        innerRadius = (float)data["innerRadius"];
        outerRadius = (float)data["outerRadius"];

        var vec = ((string)data["center"]).Split(',');
        center = new Vector3(float.Parse(vec[0]), float.Parse(vec[1]), float.Parse(vec[2]));
    }

    public Vector3 getValue(float ratio)
    {
        throw new NotImplementedException();
    }

    public UnityEngine.ParticleSystem.MinMaxCurve getCurve()
    {
        throw new NotImplementedException();
    }

}