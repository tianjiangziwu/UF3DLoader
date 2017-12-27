using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class CurveAnchor
{
    private float _time = 0.0f;
    private float _value = 0.0f;

    private delegate float interpolateType(CurveAnchor lhs, CurveAnchor rhs, float process);

    private static Dictionary<string, interpolateType> dict = new Dictionary<string, interpolateType>();

    public CurveAnchor(float time, float value)
    {
        this._time = time;
        this._value = value;
    }

    public static string LINEAR = "0";
	public static string CONST = "1";
    public static string BEZIER = "2";

    static CurveAnchor()
    {
        dict.Add(LINEAR, new interpolateType(interpolateLinear));
        dict.Add(BEZIER, new interpolateType(interpolateBezier));
    }

    private static float interpolateLinear(CurveAnchor lhs, CurveAnchor rhs, float process)
    {
        return lhs.Value + (process - lhs.Time) / (rhs.Time - lhs.Time) * (rhs.Value - lhs.Time);
    }

    private static float interpolateBezier(CurveAnchor lhs, CurveAnchor rhs, float process)
    {
        throw new NotImplementedException();
    }

    public float interpolate(CurveAnchor rhs, float process, string type)
    {
        return dict[type](this, rhs, process);
    }

    public float Time
    {
        get
        {
            return _time;
        }

        set
        {
            _time = value;
        }
    }

    public float Value
    {
        get
        {
            return _value;
        }

        set
        {
            _value = value;
        }
    }
}
