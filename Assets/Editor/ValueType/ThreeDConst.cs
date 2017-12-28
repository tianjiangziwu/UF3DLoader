using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json.Linq;
using UnityEngine;

public class ThreeDConst : IThreeDValue
{
    private Vector3 value;

    public void deserialize(JObject data)
    {
        var array = ((string)data["value"]).Split(',');
        value = new Vector3(float.Parse(array[0]), float.Parse(array[1]), float.Parse(array[2]));
    }

    public List<UnityEngine.ParticleSystem.MinMaxCurve> getThreeDCurve()
    {
        var ret = new List<UnityEngine.ParticleSystem.MinMaxCurve>();
        ret.Add(new UnityEngine.ParticleSystem.MinMaxCurve(value[0]));
        ret.Add(new UnityEngine.ParticleSystem.MinMaxCurve(value[1]));
        ret.Add(new UnityEngine.ParticleSystem.MinMaxCurve(value[2]));
        return ret;
    }

    public Vector3 getValue(float ratio)
    {
        var tdc = getThreeDCurve();
        return new Vector3(tdc[0].Evaluate(ratio), tdc[1].Evaluate(ratio), tdc[2].Evaluate(ratio));
    }
}
