using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


public interface IThreeDValue : IValue
{
    UnityEngine.Vector3 getValue(float ratio);

    List<UnityEngine.ParticleSystem.MinMaxCurve> getThreeDCurve(bool changYZ = true);
}
