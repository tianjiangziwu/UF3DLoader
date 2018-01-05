using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json.Linq;
using UnityEngine;

public class ThreeDCylinder : IThreeDValue
{
    private float innerRadius = 0.0f;
    private float outerRadius = 1.0f;
    private Vector3 center = new Vector3();

    private Matrix4x4 matrix;

    private float height = 1.0f;

    private Vector3 direction = new Vector3();

    public Vector3 Direction
    {
        get
        {
            return direction;
        }

        set
        {
            direction = value;
            if (direction.magnitude > 0)
            {
                direction.Normalize();
                var flag = Vector3.Dot(direction, new Vector3(0.0f, 1.0f, 0.0f)) > 0 ? 1 : -1;
                var degree = flag * Vector3.Angle(new Vector3(0.0f, 1.0f, 0.0f), direction);
                if (degree != 0)
                {
                    
                    var rotationAxis = Vector3.Cross(new Vector3(0.0f, 1.0f, 0.0f), direction);
                    matrix = new Matrix4x4();
                    //todo 等待实现
                    //matrix.appendRotation(degree, rotationAxis);
                }
            }
        }
    }

    public void deserialize(JObject data)
    {
        innerRadius = (float)data["innerRadius"];
        outerRadius = (float)data["outerRadius"];

        var vec = ((string)data["center"]).Split(',');
        center = new Vector3(float.Parse(vec[0]), float.Parse(vec[1]), float.Parse(vec[2]));
    }

    public Vector3 getValue(float ratio)
    {
        var tdc = getThreeDCurve();
        return new Vector3(tdc[0].Evaluate(ratio), tdc[1].Evaluate(ratio), tdc[2].Evaluate(ratio));
    }

    /// <summary>
    /// 未实现
    /// </summary>
    /// <returns></returns>
    public List<UnityEngine.ParticleSystem.MinMaxCurve> getThreeDCurve(bool changYZ = true)
    {
        var result = new Vector3();

        var h = UnityEngine.Random.Range(0.0f, 1.0f) * height; // - _height / 2;
        var r = outerRadius * UnityEngine.Mathf.Pow(UnityEngine.Random.Range(0.0f, 1.0f) * (1 - innerRadius / outerRadius) + innerRadius / outerRadius, 1 / 2);
        var degree1 = UnityEngine.Random.Range(0.0f, 1.0f) * UnityEngine.Mathf.PI * 2;
        result.Set(r * UnityEngine.Mathf.Cos(degree1), h, r * UnityEngine.Mathf.Sin(degree1));
        if (matrix != null)
        {
            result = TransformUtil.DeltaTransformVector(matrix, result);
        }
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
