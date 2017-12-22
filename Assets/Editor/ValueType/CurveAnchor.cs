using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class CurveAnchor
{
    private float _time = 0.0f;
    private float _value = 0.0f;

    public CurveAnchor(float time, float value)
    {
        this._time = time;
        this._value = value;
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
