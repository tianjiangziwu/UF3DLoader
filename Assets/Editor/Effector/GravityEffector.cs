using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json.Linq;
using UnityEngine;

public class GravityEffector : IEffector
{

    private UnityEngine.Vector3 gravityDir;
    private float gravity = 0.0f;

    public void deserialize(JObject data)
    {
        var dir = StringUtil.SplitString<float>((string)data["gravityDir"], new char[] { ',' });
        gravityDir = new UnityEngine.Vector3(dir[0], dir[1], dir[2]);
        gravity = (float)data["gravity"] * Uf3dLoader.vertexScale;
    }

    public void ApplyToUnityParticleSystem(UnityEngine.ParticleSystem ups, ParticleSystem ps)
    {
        var forceModule = ups.forceOverLifetime;

        forceModule.space = ParticleSystemSimulationSpace.Local;
        if (forceModule.enabled)
        {
            forceModule.x = new UnityEngine.ParticleSystem.MinMaxCurve(gravity * gravityDir[0] + forceModule.x.Evaluate(0.0f));
            forceModule.y = new UnityEngine.ParticleSystem.MinMaxCurve(-gravity * gravityDir[2] + forceModule.y.Evaluate(0.0f));
            forceModule.z = new UnityEngine.ParticleSystem.MinMaxCurve(gravity * gravityDir[1] + forceModule.z.Evaluate(0.0f));
        }
        else
        {
            forceModule.x = new UnityEngine.ParticleSystem.MinMaxCurve(gravity * gravityDir[0]);
            forceModule.y = new UnityEngine.ParticleSystem.MinMaxCurve(-gravity * gravityDir[2]);
            forceModule.z = new UnityEngine.ParticleSystem.MinMaxCurve(gravity * gravityDir[1]);
        }
        forceModule.enabled = true;
    }
}
