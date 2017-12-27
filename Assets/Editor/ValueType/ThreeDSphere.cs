using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json.Linq;
using UnityEngine;

public class ThreeDSphere : IThreeDValue
{
    private float innerRadius = 0.0f;
    private float outerRadius = 0.0f;
    private Vector3 center;

    public float InnerRadius
    {
        get
        {
            return innerRadius;
        }

        set
        {
            innerRadius = value;
        }
    }

    public float OuterRadius
    {
        get
        {
            return outerRadius;
        }

        set
        {
            outerRadius = value;
        }
    }

    public Vector3 Center
    {
        get
        {
            return center;
        }

        set
        {
            center = value;
        }
    }

    public void deserialize(JObject data)
    {
        InnerRadius = (float)data["innerRadius"];
        OuterRadius = (float)data["outerRadius"];

        var vec = ((string)data["center"]).Split(',');
        Center = new Vector3(float.Parse(vec[0]), float.Parse(vec[1]), float.Parse(vec[2]));
    }

    public Vector3 getValue(float ratio)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// 实现由问题，不是随机
    /// </summary>
    /// <returns></returns>
    public List<UnityEngine.ParticleSystem.MinMaxCurve> getThreeDCurve()
    {
        Vector3 result = new Vector3();
        var radius  = (OuterRadius - InnerRadius) * UnityEngine.Random.Range(0.0f, 1.0f) + InnerRadius;
        var angle1  = UnityEngine.Mathf.PI * 2 * UnityEngine.Random.Range(0.0f, 1.0f);
        var angle2  = UnityEngine.Mathf.PI * UnityEngine.Random.Range(0.0f, 1.0f) + UnityEngine.Mathf.PI / 2;
        result.x = radius * UnityEngine.Mathf.Cos(angle2) * UnityEngine.Mathf.Cos(angle1);
        result.z = radius * UnityEngine.Mathf.Cos(angle2) * UnityEngine.Mathf.Sin(angle1);
        result.y = radius * UnityEngine.Mathf.Sin(angle2);

        result.x += Center.x;
        result.y += Center.y;
        result.z += Center.z;
        List<UnityEngine.ParticleSystem.MinMaxCurve> ret = new List<UnityEngine.ParticleSystem.MinMaxCurve>();
        ret.Add(new UnityEngine.ParticleSystem.MinMaxCurve(result.x));
        ret.Add(new UnityEngine.ParticleSystem.MinMaxCurve(result.x));
        ret.Add(new UnityEngine.ParticleSystem.MinMaxCurve(result.x));
        return ret;
    }
}