using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class Emitter : IDeserialize
{
    public void deserialize(Newtonsoft.Json.Linq.JObject data)
    {
        deserializeAttribute(data, "shape");
    }

    private void deserializeAttribute(Newtonsoft.Json.Linq.JObject data, string v)
    {
        Newtonsoft.Json.Linq.JObject dict = data[v] as Newtonsoft.Json.Linq.JObject;
        string classType = (string)dict["type"];
        classType = classType.Substring(classType.IndexOf("::") + 2);
        System.Type ct = System.Type.GetType(classType, true);
        IDeserialize o = (IDeserialize)Activator.CreateInstance(ct);
        o.deserialize(dict);
    }
}
