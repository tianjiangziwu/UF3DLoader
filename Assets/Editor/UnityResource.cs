using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class UnityResource
{
    private string texture2DPath = string.Empty;

    private string materialPath = string.Empty;

    private string meshPath = string.Empty;

    public string Texture2DPath
    {
        get
        {
            return texture2DPath;
        }

        set
        {
            texture2DPath = value;
        }
    }

    public string MaterialPath
    {
        get
        {
            return materialPath;
        }

        set
        {
            materialPath = value;
        }
    }

    public string MeshPath
    {
        get
        {
            return meshPath;
        }

        set
        {
            meshPath = value;
        }
    }
}
