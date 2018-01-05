using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class CylinderShape : IShape
{
    // 内径
    private float radiusSmall = 0.0f;
    // 外径
    private float radiusBig = 1.0f;
    // 高
    private float height;

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

    public float Height
    {
        get
        {
            return height;
        }

        set
        {
            height = value;
        }
    }

    public void deserialize(Newtonsoft.Json.Linq.JObject data)
    {
        radiusBig = (float)data["radiusBig"] * Uf3dLoader.vertexScale;
        radiusSmall = (float)data["radiusSmall"] * Uf3dLoader.vertexScale;
        height = (float)data["height"] * Uf3dLoader.vertexScale;
    }
}
