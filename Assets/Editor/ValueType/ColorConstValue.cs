﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json.Linq;
using UnityEngine;

public class ColorConstValue : IColorValue
{
    private uint value = 0;

    public uint Value
    {
        get
        {
            return value;
        }

        set
        {
            this.value = value;
        }
    }

    public void deserialize(JObject data)
    {
        value = (uint)data["value"];
    }

    public uint getValue(float ratio)
    {
        throw new NotImplementedException();
    }

}