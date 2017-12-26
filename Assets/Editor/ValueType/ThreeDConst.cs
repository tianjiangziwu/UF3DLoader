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
