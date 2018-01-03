using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json.Linq;
using UnityEngine;

public class AccelerateEffector : IEffector
{
    private float accelerate = 0.0f;

    public void ApplyToUnityParticleSystem(UnityEngine.ParticleSystem ups, ParticleSystem ps)
    {
        throw new NotImplementedException();
    }

    public void deserialize(JObject data)
    {
        accelerate = (float)data["accelerate"];
    }
}
