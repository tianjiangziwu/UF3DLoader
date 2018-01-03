using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json.Linq;
using UnityEngine;

public class OrbitEffector : IEffector
{
    private float degree = 0.0f;
    private float radius = 0.0f;

    public void ApplyToUnityParticleSystem(UnityEngine.ParticleSystem ups, ParticleSystem ps)
    {
        throw new NotImplementedException();
    }

    public void deserialize(JObject data)
    {
        //todo -
        degree = (float)data["degree"];
        radius = (float)data["radius"];
    }
}