using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json.Linq;
using UnityEngine;

public class ThreeDCylinder : IThreeDValue
{
    private float innerRadius;
    private float outerRadius;
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

    Vector3 IThreeDValue.getValue(float ratio)
    {
        throw new NotImplementedException();
    }

    UnityEngine.ParticleSystem.MinMaxCurve IValue.getCurve()
    {
        throw new NotImplementedException();
    }

    void IDeserialize.deserialize(JObject data)
    {
        throw new NotImplementedException();
    }
}
