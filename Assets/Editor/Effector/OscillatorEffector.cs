using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json.Linq;

public class OscillatorEffector : IEffector
{
    private float degree = 0.0f;
    private float distance = 0.0f;

    public void deserialize(JObject data)
    {
        
        degree = (float)data["degree"];
        //todo -
        distance = (float)data["distance"];
    }
}
