using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public interface IOneDValue:IValue
{
    void scaleValue(float ratio = 1.0f);
    float getValue(float ratio);
    float getMaxValue();
    UnityEngine.ParticleSystem.MinMaxCurve getCurve(ValueTypeUtil.CurveType flag = ValueTypeUtil.CurveType.Normal, float scale = 1.0f);
    UnityEngine.ParticleSystem.MinMaxCurve getNegativeCurve(ValueTypeUtil.CurveType flag = ValueTypeUtil.CurveType.Normal, float scale = 1.0f);
}
