using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public interface IOneDValue:IValue
{
    float getValue(float ratio);
    float getMaxValue();
    UnityEngine.ParticleSystem.MinMaxCurve getCurve(ValueTypeUtil.CurveType flag = ValueTypeUtil.CurveType.Normal);
    UnityEngine.ParticleSystem.MinMaxCurve getNegativeCurve(ValueTypeUtil.CurveType flag = ValueTypeUtil.CurveType.Normal);
}
