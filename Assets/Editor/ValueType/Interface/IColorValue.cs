using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public interface IColorValue : IValue
{
    uint getValue(float ratio);
    UnityEngine.ParticleSystem.MinMaxGradient getGradient();
}
