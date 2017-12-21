using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public interface IDeserialize
{
    void deserialize(SimpleJson.JsonObject data);
}
