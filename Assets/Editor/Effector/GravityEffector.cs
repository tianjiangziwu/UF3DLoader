using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json.Linq;

public class GravityEffector : IEffector
{

    private UnityEngine.Vector3 gravityDir;
    private float gravity = 0.0f;

    public void deserialize(JObject data)
    {
        var dir = StringUtil.SplitString<float>((string)data["gravityDir"], new char[] { ',' });
        gravityDir = new UnityEngine.Vector3(dir[0], dir[1], dir[2]);
        gravity = (float)data["gravity"];
    }
}
