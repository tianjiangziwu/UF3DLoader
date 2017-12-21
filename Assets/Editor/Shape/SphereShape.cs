using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class SphereShape : IDeserialize
{
    private float radiusBig = 0;
    private float radiusSmall = 0;

    public float RadiusBig
    {
        get
        {
            return radiusBig;
        }

        set
        {
            radiusBig = value;
        }
    }

    public float RadiusSmall
    {
        get
        {
            return radiusSmall;
        }

        set
        {
            radiusSmall = value;
        }
    }

    public void deserialize(Newtonsoft.Json.Linq.JObject data)
    {
        radiusBig = (float)data["radiusBig"];
        radiusSmall = (float)data["radiusSmall"];
    }
}

