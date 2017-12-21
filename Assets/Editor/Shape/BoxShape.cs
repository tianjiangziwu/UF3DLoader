using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SimpleJson;
using UnityEngine;

public class BoxShape : IDeserialize
{
    private UnityEngine.Vector3 rectFrom;
    private UnityEngine.Vector3 rectTo;

    public void deserialize(JsonObject data)
    {
        var value = (data["rectFrom"] as string).Split((",").ToCharArray());
        rectFrom = new Vector3(Convert.ToSingle(value[0]), Convert.ToSingle(value[1]), Convert.ToSingle(value[2]));
        value = (data["rectTo"] as string).Split((",").ToCharArray());
        rectTo = new Vector3(Convert.ToSingle(value[0]), Convert.ToSingle(value[1]), Convert.ToSingle(value[2]));
    }
}
