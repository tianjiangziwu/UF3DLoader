using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json.Linq;
using UnityEngine;

public class ForceEffector : IEffector
{

    public UnityEngine.Vector3 forceDir;
    public float force = 0.0f;

    public void deserialize(JObject data)
    {
        var vec = ((string)data["forceDir"]).Split(',');
        forceDir = new UnityEngine.Vector3(float.Parse(vec[0]), float.Parse(vec[1]), float.Parse(vec[2]));
        force = (float)data["force"];
    }

    public void ApplyToUnityParticleSystem(UnityEngine.ParticleSystem ups, ParticleSystem ps)
    {
        var forceModule = ups.forceOverLifetime;
        
        forceModule.space = ParticleSystemSimulationSpace.Local;
        if (forceModule.enabled)
        {
            forceModule.x = new UnityEngine.ParticleSystem.MinMaxCurve(force * forceDir[0] + forceModule.x.Evaluate(0.0f));
            forceModule.y = new UnityEngine.ParticleSystem.MinMaxCurve(-force * forceDir[2] + forceModule.z.Evaluate(0.0f));
            forceModule.z = new UnityEngine.ParticleSystem.MinMaxCurve(force * forceDir[1] + forceModule.y.Evaluate(0.0f));
        }
        else
        {
            forceModule.x = new UnityEngine.ParticleSystem.MinMaxCurve(force * forceDir[0]);
            forceModule.y = new UnityEngine.ParticleSystem.MinMaxCurve(-force * forceDir[2]);
            forceModule.z = new UnityEngine.ParticleSystem.MinMaxCurve(force * forceDir[1]);
        }
        forceModule.enabled = true;
    }
}
