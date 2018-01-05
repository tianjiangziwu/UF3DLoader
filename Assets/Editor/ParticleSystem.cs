using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using System.Runtime;
using Newtonsoft;

public class ParticleSystem
{
    private int chunkId = -1;
    private string type = string.Empty;
    private string name = string.Empty;
    private int layer = 0;
    private int parent = -1;
    private float frameSpeed = 0;
    private int texId = -1;
    private uint surfId = 0;
    private Matrix4x4 matrix;

    //粒子发射器
    private Emitter emitter = null;

    /// <summary>
    /// 所有的影响器
    /// </summary>
    private List<IEffector> effectors = null;

    private RenderParam renderParam = new RenderParam();

    private UnityResource unityResourceParam = new UnityResource(); 

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

    public RenderParam RenderParam
    {
        get
        {
            return renderParam;
        }

        set
        {
            renderParam = value;
        }
    }

    public UnityResource UnityResourceParam
    {
        get
        {
            return unityResourceParam;
        }

        set
        {
            unityResourceParam = value;
        }
    }

    public int ChunkId
    {
        get
        {
            return chunkId;
        }

        set
        {
            chunkId = value;
        }
    }

    public Emitter Emitter
    {
        get
        {
            return emitter;
        }

        set
        {
            emitter = value;
        }
    }

    public List<IEffector> Effectors
    {
        get
        {
            return effectors;
        }

        set
        {
            effectors = value;
        }
    }

    public ParticleSystem()
    {
        emitter = new Emitter();
        emitter.Ps = this;
    }

    /// <summary>
    /// 反序列化
    /// </summary>
    /// <param name="data">json数据</param>
    public void deserialize(Newtonsoft.Json.Linq.JObject data)
    {
        emitter.deserialize(data["emitter"] as Newtonsoft.Json.Linq.JObject);

        Effectors = new List<IEffector>();

        foreach(Newtonsoft.Json.Linq.JObject effect in data["effectors"])
        {
            string classType = (string)effect["type"];
            classType = classType.Substring(classType.IndexOf("::") + 2);
            //UnityEngine.Debug.LogFormat("classType{0}", classType);
            System.Type ct = System.Type.GetType(classType, true);
            IEffector o = (IEffector)Activator.CreateInstance(ct);
            o.deserialize(effect);
            Effectors.Add(o);
        }

        renderParam.ParticleNumber = (int)data["number"];
        emitter.UniformScale = (bool)data["uniformScale"];
        renderParam.BlendMode = (int)data["blendMode"];
        renderParam.DepthWrite = (bool)data["depthWrite"];
        renderParam.Loop = (bool)data["loop"];
        renderParam.RotateAxis = (int)data["rotateAxis"];
        renderParam.BillboardType = (int)data["orient"];
        renderParam.IsWorldParticle = (bool)data["isWorldParticle"];
        renderParam.RotateSurface = data["rotateSurface"] == null ? false : (bool)data["rotateSurface"];
        renderParam.RandomOnBorn = (bool)data["randomOnBorn"];
        renderParam.Decal = (bool)data["decal"];
        renderParam.Prewarm = data["prewarm"] == null ? 0 : (float)data["prewarm"];
        renderParam.InfiniteBounds = (bool)data["infiniteBounds"];
        if (!renderParam.InfiniteBounds)
        {
            var bound = StringUtil.SplitString<float>((string)data["boundMin"], new char[] { ',' });
            renderParam.BoundMin = new Vector3(bound[0] * Uf3dLoader.vertexScale, bound[1] * Uf3dLoader.vertexScale, bound[2] * Uf3dLoader.vertexScale);
            bound = StringUtil.SplitString<float>((string)data["boundMax"], new char[] { ',' });
            renderParam.BoundMax = new Vector3(bound[0] * Uf3dLoader.vertexScale, bound[1] * Uf3dLoader.vertexScale, bound[2] * Uf3dLoader.vertexScale);
        }
    }
}
