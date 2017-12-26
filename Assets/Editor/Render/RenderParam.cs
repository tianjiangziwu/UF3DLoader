using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class RenderParam
{
    public const int BLEND_NONE = 0;
    public const int BLEND_ADDITIVE = 1;
    public const int BLEND_ALPHA_BLENDED = 2;
    public const int BLEND_MULTIPLY = 3;
    public const int BLEND_SCREEN = 4;
    public const int BLEND_ALPHA_BLENDED2 = 5;
    public const int BLEND_PARTICLE_ADDITIVE = 6;
    public const int BLEND_PARTICLE_ALPHA_BLEND = 7;

    public const int BillboardType_billboard = 0;
    public const int BillboardType_X = 1;
    //todo
    // left-handed to right-handed, Y&Z exchanged
    public const int BillboardType_Y = 2;
    public const int BillboardType_Z = 3;
    public const int BillboardType_Normal = 4;
    public const int BillboardType_Camera = 5;
    public const int BillboardType_Stretch = 6;

    // todo
    // Y&Z exchanged
    public const int RotateAxis_X = 0;
    public const int RotateAxis_Y = 1;
    public const int RotateAxis_Z = 2;

    // Mip Enum
    public const int MIP_NONE = 0;
    public const int MIP_NEAREST = 1;
    public const int MIP_LINEAR = 2;
    // Filter Enum
    public const int FILTER_NEAREST = 0;
    public const int FILTER_LINEAR = 1;
    // Wrap Enum
    public const int WRAP_CLAMP = 0;
    public const int WRAP_REPEAT = 1;
    //////////////////////////////////////////////////////////////////////////

    private int particleNumber = 0;
    private int blendMode = 0;
    private bool depthWrite = false;
    private bool loop = false;
    /// <summary>
    /// 粒子旋转所依赖的坐标轴
    /// </summary>
    private int rotateAxis = 0;
    private int billboardType = 0;
    private bool isWorldParticle = false;
    private bool rotateSurface = false;
    private bool randomOnBorn = false;
    private bool decal = false;
    private float prewarm = 0.0f;
    private bool infiniteBounds = false;
    private UnityEngine.Vector3 boundMin;
    private Vector3 boundMax;

    public int BlendMode
    {
        get
        {
            return blendMode;
        }

        set
        {
            blendMode = value;
        }
    }

    public bool DepthWrite
    {
        get
        {
            return depthWrite;
        }

        set
        {
            depthWrite = value;
        }
    }

    public bool Loop
    {
        get
        {
            return loop;
        }

        set
        {
            loop = value;
        }
    }

    public int RotateAxis
    {
        get
        {
            return rotateAxis;
        }

        set
        {
            rotateAxis = value;
        }
    }

    public int BillboardType
    {
        get
        {
            int ret = 0;
            if (billboardType == BillboardType_billboard)
            {
                ret = (int)ParticleSystemRenderMode.Billboard;
            }
            else if (billboardType == BillboardType_X)
            {
                ret = (int)ParticleSystemRenderMode.VerticalBillboard;
            }
            else if (billboardType == BillboardType_Y)
            {
                ret = (int)ParticleSystemRenderMode.HorizontalBillboard;
            }
            else if (billboardType == BillboardType_Z)
            {

            }
            else if (billboardType == BillboardType_Normal)
            {
                ret = (int)ParticleSystemRenderMode.Mesh;
            }
            else if (billboardType == BillboardType_Stretch)
            {
                ret = (int)ParticleSystemRenderMode.Stretch;
            }
            return ret;
        }

        set
        {
            billboardType = value;
        }
    }

    public bool IsWorldParticle
    {
        get
        {
            return isWorldParticle;
        }

        set
        {
            isWorldParticle = value;
        }
    }

    public bool RotateSurface
    {
        get
        {
            return rotateSurface;
        }

        set
        {
            rotateSurface = value;
        }
    }

    public bool RandomOnBorn
    {
        get
        {
            return randomOnBorn;
        }

        set
        {
            randomOnBorn = value;
        }
    }

    public bool Decal
    {
        get
        {
            return decal;
        }

        set
        {
            decal = value;
        }
    }

    public float Prewarm
    {
        get
        {
            return prewarm;
        }

        set
        {
            prewarm = value;
        }
    }

    public bool InfiniteBounds
    {
        get
        {
            return infiniteBounds;
        }

        set
        {
            infiniteBounds = value;
        }
    }

    public Vector3 BoundMin
    {
        get
        {
            return boundMin;
        }

        set
        {
            boundMin = value;
        }
    }

    public Vector3 BoundMax
    {
        get
        {
            return boundMax;
        }

        set
        {
            boundMax = value;
        }
    }

    public int ParticleNumber
    {
        get
        {
            return particleNumber;
        }

        set
        {
            particleNumber = value;
        }
    }
}
