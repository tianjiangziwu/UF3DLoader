using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json.Linq;

public class ForceEffector : IEffector
{

    public UnityEngine.Vector3 forceDir;
    public float force = 0.0f;

    public void deserialize(JObject data)
    {
        var vec = ((string)data["forceDir"]).Split(',');
        forceDir = new UnityEngine.Vector3(float.Parse(vec[0]), float.Parse(vec[1]), float.Parse(vec[2]));
        force = (float)data["force"];
    }
}
