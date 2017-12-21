using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SimpleJson;

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

    public void deserialize(JsonObject data)
    {
        string str = data["radiusBig"] as string;
        radiusBig = float.Parse(str);
        radiusSmall = float.Parse(data["radiusSmall"] as string);
    }
}

