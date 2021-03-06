﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class BoxShape : IShape
{
    private UnityEngine.Vector3 rectFrom;
    private UnityEngine.Vector3 rectTo;

    public Vector3 getScale()
    {
        Vector3 scale = new Vector3();
        scale = rectTo - rectFrom;
        return scale;
    }

    public Vector3 getPosition()
    {
        return getScale() / 2;
    }

    /// <summary>
    /// do not exchange y,z
    /// </summary>
    /// <param name="data"></param>
    public void deserialize(Newtonsoft.Json.Linq.JObject data)
    {
        //todo
        var value = ((string)data["rectFrom"]).Split((",").ToCharArray());
        rectFrom = new Vector3(Convert.ToSingle(value[0]) * Uf3dLoader.vertexScale, Convert.ToSingle(value[1]) * Uf3dLoader.vertexScale, Convert.ToSingle(value[2]) * Uf3dLoader.vertexScale);
        value = ((string)data["rectTo"]).Split((",").ToCharArray());
        rectTo = new Vector3(Convert.ToSingle(value[0]) * Uf3dLoader.vertexScale, Convert.ToSingle(value[1]) * Uf3dLoader.vertexScale, Convert.ToSingle(value[2]) * Uf3dLoader.vertexScale);
    }
}
