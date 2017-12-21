using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class Surface3D
{
    /**
     * Vertex position attribute (float3). 
     */
    public const int POSITION = 0;
    /**
     * Vertex primary uv channel attribute (flaot2). 
     */
    public const int UV0 = 1;
    /**
     * Vertex secondary uv channel attribute (float2). 
     */
    public const int UV1 = 2;
    /**
     * Vertex normals attribute (float3). 
     */
    public const int NORMAL = 3;
    /**
     * Vertex tangents attribute (float3). 
     */
    public const int TANGENT = 4;
    /**
     * Vertex bi-tangents attribute (float3). 
     */
    public const int BITANGENT = 5;
    /**
     * Vertex particles attribute (float4). Life (0-1), rotation in radians and size x and y (-1 to 1). 
     */
    public const int PARTICLE = 6;
    /**
     * Vertex skin weights attribute (could be float1 to float4 depending of the Device3D.maxBonesPerVertex property). 
     */
    public const int SKIN_WEIGHTS = 7;
    /**
     * Vertex skin indices attribute (could be float1 to float4 depending of the Device3D.maxBonesPerVertex property). 
     */
    public const int SKIN_INDICES = 8;
    /**
     * Vertex color attribute (float3). 
     */
    public const int COLOR0 = 9;
    /**
     * Vertex color attribute (float3). 
     */
    public const int COLOR1 = 10;
    public const int COLOR2 = 11;
    /**
     * Vertex target position attribute (float3). 
     */
    public const int TARGET_POSITION = 12;
    /**
     * Vertex target normal attribute (float3). 
     */
    public const int TARGET_NORMAL = 13;
    /**
     * Vertex third uv channel attribute (float2). 
     */
    public const int UV2 = 14;
    /**
     * Vertex four uv channel attribute (float2). 
     */
    public const int UV3 = 15;

    private List<float> vertexVector;
    private List<uint> indexVector;
    /**
     * Number of triangles that will be drawn. 
     */
    private int numTriangles = -1;
    /**
     * The index of the first vertex index selected to render. 
     */
    private int firstIndex = 0;
    /**
     * The number of data values associated with each vertex 
     */
    private int sizePerVertex = 0;
    /**
     * The offset of data index in the vertexVector. 
     */
    private List<int> offset = Enumerable.Repeat<int>(0, 16).ToList();
    /**
     * The buffer format specified for each input. 
     */
    private List<string> format = new List<string>();

    public List<float> VertexVector
    {
        get
        {
            return vertexVector;
        }

        set
        {
            vertexVector = value;
        }
    }

    public List<uint> IndexVector
    {
        get
        {
            return indexVector;
        }

        set
        {
            indexVector = value;
        }
    }

    public int NumTriangles
    {
        get
        {
            return numTriangles;
        }

        set
        {
            numTriangles = value;
        }
    }

    public int FirstIndex
    {
        get
        {
            return firstIndex;
        }

        set
        {
            firstIndex = value;
        }
    }

    public int SizePerVertex
    {
        get
        {
            return sizePerVertex;
        }

        set
        {
            sizePerVertex = value;
        }
    }

    public List<int> Offset
    {
        get
        {
            return offset;
        }

        set
        {
            offset = value;
        }
    }

    public List<string> Format
    {
        get
        {
            return format;
        }

        set
        {
            format = value;
        }
    }

    public Surface3D()
    {
        for (int i = 0; i < Offset.Count; i++)
        {
            Offset[i] = -1;
            Format.Add(string.Empty);
        }
    }
}
