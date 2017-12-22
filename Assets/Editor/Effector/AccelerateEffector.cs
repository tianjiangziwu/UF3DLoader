using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json.Linq;

public class AccelerateEffector : IEffector
{
    private float accelerate = 0.0f;

    public void deserialize(JObject data)
    {
        accelerate = (float)data["accelerate"];
    }
}
