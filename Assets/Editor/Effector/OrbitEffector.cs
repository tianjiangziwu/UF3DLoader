using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json.Linq;

public class OrbitEffector : IEffector
{
    private float degree = 0.0f;
    private float radius = 0.0f;

    public void deserialize(JObject data)
    {
        //todo -
        degree = (float)data["degree"];
        radius = (float)data["radius"];
    }
}