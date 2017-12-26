using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json.Linq;
using UnityEngine;

public class ThreeDConst : IThreeDValue
{
    private Vector3 value;

    public void deserialize(JObject data)
    {
        var array = ((string)data["value"]).Split(',');
        value = new Vector3(float.Parse(array[0]), float.Parse(array[1]), float.Parse(array[2]));
    }

    public Vector3 getValue(float ratio)
    {
        throw new NotImplementedException();
    }
}
