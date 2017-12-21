using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class Emitter : IDeserialize
{
    public void deserialize(SimpleJson.JsonObject data)
    {
        deserializeAttribute(data, "shape");
    }

    private void deserializeAttribute(SimpleJson.JsonObject data, string v)
    {
        SimpleJson.JsonObject dict = data[v] as SimpleJson.JsonObject;
        string classType = dict["type"] as String;
        classType = classType.Substring(classType.IndexOf("::") + 2);
        System.Type ct = System.Type.GetType(classType, true);
        IDeserialize o = (IDeserialize)Activator.CreateInstance(ct);
        o.deserialize(dict);
    }
}
