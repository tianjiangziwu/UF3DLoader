using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json.Linq;

public class AttractEffector : IEffector
{
    private UnityEngine.Vector3 attractPoint;

    /// <summary>
    /// 每秒影响的速度值
    /// </summary>
    private float vel = 0.0f;

    public void deserialize(JObject data)
    {
        var vec = ((string)data["attractPoint"]).Split(',');
        attractPoint = new UnityEngine.Vector3(float.Parse(vec[0]), float.Parse(vec[1]), float.Parse(vec[2]));
        vel = (float)data["vel"];
    }
}
