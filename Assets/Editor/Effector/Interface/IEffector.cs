using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public interface IEffector:IDeserialize
{
    void ApplyToUnityParticleSystem(UnityEngine.ParticleSystem ups, ParticleSystem ps);
}
