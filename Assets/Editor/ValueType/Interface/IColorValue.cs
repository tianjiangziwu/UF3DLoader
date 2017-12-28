using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public interface IColorValue : IValue
{
    UnityEngine.Color getValue(float ratio);
    UnityEngine.ParticleSystem.MinMaxGradient getGradient();
}
