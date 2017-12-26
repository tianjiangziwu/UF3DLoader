﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json.Linq;
using UnityEngine;

public class ThreeDRandom : IThreeDValue
{
    private Vector3 min;
    private Vector3 max;

    public void deserialize(JObject data)
    {
        var vec = ((string)data["minValue"]).Split(',');
        min = new Vector3(float.Parse(vec[0]), float.Parse(vec[1]), float.Parse(vec[2]));

        vec = ((string)data["maxValue"]).Split(',');
        max = new Vector3(float.Parse(vec[0]), float.Parse(vec[1]), float.Parse(vec[2]));
    }

    public Vector3 getValue(float ratio)
    {
        throw new NotImplementedException();
    }

    public List<UnityEngine.ParticleSystem.MinMaxCurve> getThreeDCurve()
    {
        var ret = new List<UnityEngine.ParticleSystem.MinMaxCurve>();
        ret.Add(new UnityEngine.ParticleSystem.MinMaxCurve(min.x, max.x));
        ret.Add(new UnityEngine.ParticleSystem.MinMaxCurve(min.y, max.y));
        ret.Add(new UnityEngine.ParticleSystem.MinMaxCurve(min.z, max.z));
        return ret;
    }
}
