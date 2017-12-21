using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SimpleJson;
using UnityEngine;

public class ParticleSystem
{
    private string type = string.Empty;
    private string name = string.Empty;
    private int layer = 0;
    private int parent = -1;
    private float frameSpeed = 0;
    private int texId = -1;
    private uint surfId = 0;
    private SimpleJson.JsonObject jsonObj = null;
    private Matrix4x4 matrix;

    //粒子发射器
    private Emitter emitter = null;

    public string Type
    {
        get
        {
            return type;
        }

        set
        {
            type = value;
        }
    }

    public string Name
    {
        get
        {
            return name;
        }

        set
        {
            name = value;
        }
    }

    public int Layer
    {
        get
        {
            return layer;
        }

        set
        {
            layer = value;
        }
    }

    public int Parent
    {
        get
        {
            return parent;
        }

        set
        {
            parent = value;
        }
    }

    public float FrameSpeed
    {
        get
        {
            return frameSpeed;
        }

        set
        {
            frameSpeed = value;
        }
    }

    public int TexId
    {
        get
        {
            return texId;
        }

        set
        {
            texId = value;
        }
    }

    public uint SurfId
    {
        get
        {
            return surfId;
        }

        set
        {
            surfId = value;
        }
    }



    public Matrix4x4 Matrix
    {
        get
        {
            return matrix;
        }

        set
        {
            matrix = value;
        }
    }

    public JsonObject JsonObj
    {
        get
        {
            return jsonObj;
        }

        set
        {
            jsonObj = value;
        }
    }

    public ParticleSystem()
    {
        emitter = new Emitter();
    }

    /// <summary>
    /// 反序列化
    /// </summary>
    /// <param name="data">json数据</param>
    public void deserialize(JsonObject data)
    {
        emitter.deserialize(data["emitter"] as JsonObject);
    }
}
