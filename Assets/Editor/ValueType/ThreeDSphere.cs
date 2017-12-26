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

    public void deserialize(JObject data)
    {
        innerRadius = (float)data["innerRadius"];
        outerRadius = (float)data["outerRadius"];

        var vec = ((string)data["center"]).Split(',');
        center = new Vector3(float.Parse(vec[0]), float.Parse(vec[1]), float.Parse(vec[2]));
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
        var radius  = (outerRadius - innerRadius) * UnityEngine.Random.Range(0.0f, 1.0f) + innerRadius;
        var angle1  = UnityEngine.Mathf.PI * 2 * UnityEngine.Random.Range(0.0f, 1.0f);
        var angle2  = UnityEngine.Mathf.PI * UnityEngine.Random.Range(0.0f, 1.0f) + UnityEngine.Mathf.PI / 2;
        result.x = radius * UnityEngine.Mathf.Cos(angle2) * UnityEngine.Mathf.Cos(angle1);
        result.z = radius * UnityEngine.Mathf.Cos(angle2) * UnityEngine.Mathf.Sin(angle1);
        result.y = radius * UnityEngine.Mathf.Sin(angle2);

        result.x += center.x;
        result.y += center.y;
        result.z += center.z;
        List<UnityEngine.ParticleSystem.MinMaxCurve> ret = new List<UnityEngine.ParticleSystem.MinMaxCurve>();
        ret.Add(new UnityEngine.ParticleSystem.MinMaxCurve(result.x));
        ret.Add(new UnityEngine.ParticleSystem.MinMaxCurve(result.x));
        ret.Add(new UnityEngine.ParticleSystem.MinMaxCurve(result.x));
        return ret;
    }
}