using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class Texture3D
{
    public const int FORMAT_RGBA = 0;
    public const int FORMAT_COMPRESSED = 1;
    public const int FORMAT_COMPRESSED_ALPHA = 2;
    public const int FORMAT_BGR_PACKED = 3;
    public const int FORMAT_BGRA_PACKED = 4;

    public const int FILTER_NEAREST = 0;
    public const int FILTER_LINEAR = 1;
    public const int FILTER_ANISOTROPIC2X = 2;    //Introduced by Flash 14
    public const int FILTER_ANISOTROPIC4X = 3;
    public const int FILTER_ANISOTROPIC8X = 4;
    public const int FILTER_ANISOTROPIC16X = 5;

    public const int WRAP_CLAMP = 0;
    public const int WRAP_REPEAT = 1;
    public const int WRAP_CLAMP_U_REPEAT_V = 2;   //Introduced by Flash 13
    public const int WRAP_REPEAT_U_CLAMP_V = 3;

    public const int TYPE_2D = 0;
    public const int TYPE_CUBE = 1;

    public const int MIP_NONE = 0;
    public const int MIP_NEAREST = 1;
    public const int MIP_LINEAR = 2;

    public const string PNG_LOADED = "PngLoaded";

    private bool _optimizeForRenderToTexture = false;
    private byte[] data = null;
    private int filterMode = FILTER_LINEAR;
    private int wrapMode = WRAP_REPEAT;
    private int mipMode = MIP_LINEAR;
    public int typeMode = TYPE_2D;
    private string format = string.Empty;
    private string name = string.Empty;
    private bool isATF = false;

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

    public int FilterMode
    {
        get
        {
            return filterMode;
        }

        set
        {
            filterMode = value;
        }
    }

    public int WrapMode
    {
        get
        {
            return wrapMode;
        }

        set
        {
            wrapMode = value;
        }
    }

    public int MipMode
    {
        get
        {
            return mipMode;
        }

        set
        {
            mipMode = value;
        }
    }

    public Texture3D(byte[] data, bool optimizeForRenderToTexture, int fmt = FORMAT_RGBA, int type = TYPE_2D)
    {
        this._optimizeForRenderToTexture = optimizeForRenderToTexture;
        typeMode = type;

        if ((fmt == FORMAT_COMPRESSED) || (fmt == FORMAT_COMPRESSED_ALPHA))
        {
            this.isATF = true;
        }

        this.data = data;
    }
}
