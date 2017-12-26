using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json.Linq;
using UnityEngine;

public class OneDRandom : IOneDValue
{
    private float min = 0.0f;
    private float max = 0.0f;

    public void deserialize(JObject data)
    {
        min = (float)data["min"];
        max = (float)data["max"];
    }

    public float getMaxValue()
    {
        throw new NotImplementedException();
    }

    public float getValue(float ratio)
    {
        throw new NotImplementedException();
    }

    float IOneDValue.getValue(float ratio)
    {
        throw new NotImplementedException();
    }

    float IOneDValue.getMaxValue()
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
