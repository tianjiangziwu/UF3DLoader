using UnityEngine;
using System.Collections;
using System.IO;
using System.IO.Compression;
using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

public class Uf3dLoader
{

    private string _strFileName = string.Empty;
    private int _compressionLevel = 0;
    private Dictionary<int, System.Object> resource = new Dictionary<int, System.Object>();

    public string StrFileName
    {
        get
        {
            return _strFileName;
        }

        set
        {
            _strFileName = value;
        }
    }

    public List<ParticleSystem> ParticleSystemList
    {
        get
        {
            return particleSystemList;
        }

        set
        {
            particleSystemList = value;
        }
    }

    public Dictionary<int, Matrix4x4> CascadeTransform
    {
        get
        {
            return cascadeTransform;
        }

        set
        {
            cascadeTransform = value;
        }
    }

    public Dictionary<int, object> Resource
    {
        get
        {
            return resource;
        }

        set
        {
            resource = value;
        }
    }

    public bool parse(string path)
    {
        if (System.IO.Path.GetExtension(path).ToLower() == ".uf3d")
        {
            if (System.IO.File.Exists(path) == true)
            {
                this._strFileName = path;
                MemoryStream filestream = null;
                BinaryReader binReader = null;
                byte[] filebytes = null;

                // load the file as an array of bytes
                filebytes = System.IO.File.ReadAllBytes(this._strFileName);
                if (filebytes != null && filebytes.Length > 0)
                {
                    // create a seekable memory stream of the file bytes
                    using (filestream = new MemoryStream(filebytes))
                    {
                        if (filestream != null && filestream.Length > 0 && filestream.CanSeek == true)
                        {
                            // create a BinaryReader used to read the uf3d file
                            using (binReader = new BinaryReader(filestream))
                            {
                                this.load(binReader);
                            }
                        }
                        else
                            throw new Exception(@"Error loading file, could not read file from disk.");

                    }
                }
                else
                    throw new Exception(@"Error loading file, could not read file from disk.");
            }
            else
                throw new Exception(@"Error loading file, could not find file '" + _strFileName + "' on disk.");
        }
        else
            throw new Exception(@"Error loading file, file '" + _strFileName + "' must have an extension of '.uf3d'.");

        return true;
    }

    private void load(BinaryReader binReader)
    {
        if (binReader != null && binReader.BaseStream != null && binReader.BaseStream.Length > 0 && binReader.BaseStream.CanSeek == true)
        {
            try
            {
                binReader.BaseStream.Seek(0, SeekOrigin.Begin);
                if (binReader.BaseStream.Length - binReader.BaseStream.Position >= 13)
                {
                    string magic = System.Text.Encoding.UTF8.GetString(binReader.ReadBytes(3));
                    int version = (int)binReader.ReadInt16();

                    // usually version == 40
                    if (magic == "C3D" && version >= 40 && version < 50)
                    {
                        int totalLength = (int)binReader.ReadUInt32();
                        // useless
                        int totalResLength = (int)binReader.ReadUInt32();

                        int len = (int)(binReader.BaseStream.Length - binReader.BaseStream.Position);
                        var rawData = binReader.ReadBytes(len);
                        // create a seekable memory stream of the file bytes
                        using (MemoryStream filestream = new MemoryStream())
                        {
                            // create a BinaryReader used to read the uf3d file
                            using (Ionic.Zlib.ZlibStream gzipStream = new Ionic.Zlib.ZlibStream(filestream, Ionic.Zlib.CompressionMode.Decompress, Ionic.Zlib.CompressionLevel.Default, true))
                            {
                                gzipStream.Write(rawData, 0, rawData.Length);
                                gzipStream.Flush();
                                if (filestream != null && filestream.Length > 0 && filestream.CanSeek == true)
                                {
                                    // create a BinaryReader used to read the uf3d file
                                    using (binReader = new BinaryReader(filestream))
                                    {
                                        this.loadDataChunk(binReader);
                                    }
                                    ParticleSystemAssembler.Assemble(this);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // clear all 
                this.ClearAll();
                throw ex;
            }
        }
        else
        {
            this.ClearAll();
            throw new Exception(@"Error loading file, could not read file from disk.");
        }
    }

    

    

    private void loadDataChunk(BinaryReader binReader)
    {
        binReader.BaseStream.Seek(0, SeekOrigin.Begin);
        while (binReader.BaseStream.Position < binReader.BaseStream.Length)
        {
            ReadChunk chunk = new ReadChunk(binReader);
            switch (chunk.Name)
            {
                case "data":
                    ReadData(chunk);
                    break;
                case "tex":
                    ReadTexture(chunk);
                    break;
                case "buffer":
                    ReadBuffer(chunk);
                    break;
                case "idx":
                    ReadIndices(chunk);
                    break;
                case "surf_ps":
                    ReadPSSurface(chunk);
                    break;
                case "obj":
                    ReadObject(chunk);
                    break;
                default:
                    System.Diagnostics.Debug.Print("未识别的类型 {0}", chunk.Name);
                    break;
            }
            chunk.Next();
        }
    }


    private void ReadExtends(ReadChunk data)
    {
        //string jsonObject = ReadUtil.ReadUTF(data.Bytes);
    }

    private void ReadParticleSystem(string name, int layer, Matrix4x4 matrix, ReadChunk data, int parent, int chunkId)
    {
        int texId = data.Bytes.ReadUInt16();
        uint surfId = data.Bytes.ReadUInt16();
        string jsonStr = ReadUtil.ReadUTF(data.Bytes);
        JObject jsonObject = JObject.Parse(jsonStr);
        ParticleSystem ps = new ParticleSystem();
        ps.ChunkId = chunkId;
        ps.Name = name;
        ps.Layer = layer;
        ps.Matrix = matrix;
        ps.TexId = texId;
        ps.SurfId = surfId;
        ps.Parent = parent;
        ps.deserialize(jsonObject);
        particleSystemList.Add(ps);
        //System.Diagnostics.Debug.WriteLine("粒子名字:{0}", name);
    }

    private UnityEngine.Matrix4x4 ReadMatrix3D(BinaryReader input, int compression = 0)
    {
        UnityEngine.Matrix4x4 ret = new Matrix4x4();
        
        int i = 0;
        List<float> vector = new List<float>(){1, 0, 0, 0,
                0, 1, 0, 0,
                0, 0, 1, 0,
                0, 0, 0, 1};
        //文件中的实际的数据是
        //m00, m01, m02
        //m10, m11, m12
        //m20, m21, m22
        //m03, m13, m23
        //这边vector按列排列
        if (compression == 0)
        {
            i = 0;
            while (i < 16)
            {
                vector[i] = input.ReadSingle();
                if ((i % 4) == 2)
                {
                    i = i + 2;
                }
                else
                {
                    i++;
                }
            }
        }
        else if (compression == 1)
        {
            i = 0;
            while (i < 12)
            {
                vector[i] = input.ReadInt16() / 500;
                if ((i % 4) == 2)
                {
                    i = (i + 2);
                }
                else
                {
                    i++;
                }
            }
            vector[12] = input.ReadSingle();
            vector[13] = input.ReadSingle();
            vector[14] = input.ReadSingle();
        }
        FillMatrix3D(ref ret, vector);
        return ret;
    }

    /// <summary>
    /// 列存储的vecor复制到Matrix4x4
    /// </summary>
    /// <param name="matrix"></param>
    /// <param name="vector"></param>
    private void FillMatrix3D(ref UnityEngine.Matrix4x4 matrix, List<float> vector)
    {
        matrix.m00 = vector[0];
        matrix.m10 = vector[1];
        matrix.m20 = vector[2];
        matrix.m30 = vector[3];

        matrix.m01 = vector[4];
        matrix.m11 = vector[5];
        matrix.m21 = vector[6];
        matrix.m31 = vector[7];

        matrix.m02 = vector[8];
        matrix.m12 = vector[9];
        matrix.m22 = vector[10];
        matrix.m32 = vector[11];

        matrix.m03 = vector[12];
        matrix.m13 = vector[13];
        matrix.m23 = vector[14];
        matrix.m33 = vector[15];
    }

    private Dictionary<int, Matrix4x4> cascadeTransform = new Dictionary<int, Matrix4x4>();
    private List<ParticleSystem> particleSystemList = new List<ParticleSystem>();

    private void ReadObject(ReadChunk chunk)
    {
        ReadChunk data;
        BinaryReader input = chunk.Bytes;
        string type = ReadUtil.ReadUTF(chunk.Bytes);
        string name = ReadUtil.ReadUTF(chunk.Bytes);
        //名字不能包含\
        name = System.Text.RegularExpressions.Regex.Replace(name, @"/+|\*+|\\+", string.Empty);
        UnityEngine.Matrix4x4 matrix = ReadMatrix3D(chunk.Bytes, _compressionLevel);
        int layer = chunk.Bytes.ReadInt16();
        int parent = chunk.Bytes.ReadInt16();
        float frameSpeed = chunk.Bytes.ReadSingle();

        if (parent != -1)
        {
            matrix = cascadeTransform[parent] * matrix;
        }
        cascadeTransform[chunk.Id] = matrix;

        while (chunk.BytesAvailable() > 0)
        {
            data = new ReadChunk(input);
            switch (data.Name)
            {
                case "particleSysterm":
                    ReadParticleSystem(name, layer, matrix, data, parent, chunk.Id);
                    break;
                case "extends":
                    ReadExtends(data);
                    break;
                default:
                    System.Diagnostics.Debug.WriteLine("未识别属性{0}", data.Name);
                    break;
            }
            data.Next();
        }
    }

    private void ReadPSSurface(ReadChunk chunk)
    {
        Surface3D surf = new Surface3D();
        int bufId = chunk.Bytes.ReadInt16();
        int idx = chunk.Bytes.ReadInt16();

        surf.VertexVector = resource[bufId] as List<float>;
        surf.IndexVector = resource[idx] as List<uint>;

        surf.NumTriangles = chunk.Bytes.ReadInt32();
        surf.FirstIndex = chunk.Bytes.ReadInt32();

        surf.SizePerVertex = chunk.Bytes.ReadSByte();

        for (int i = 0; i < surf.Offset.Count; i++)
        {
            surf.Offset[i] = chunk.Bytes.ReadSByte();
            if (surf.Offset[i] != -1)
            {
                switch (surf.Offset[i])
                {
                    case Surface3D.POSITION:
                    case Surface3D.NORMAL:
                    case Surface3D.TANGENT:
                    case Surface3D.BITANGENT:
                    case Surface3D.COLOR0:
                    case Surface3D.COLOR1:
                    case Surface3D.COLOR2:
                        surf.Format[i] = "float3";
                        break;
                    case Surface3D.UV0:
                    case Surface3D.UV1:
                    case Surface3D.SKIN_WEIGHTS:
                    case Surface3D.SKIN_INDICES:
                        surf.Format[i] = "float2";
                        break;
                    default:
                        break;
                }
            }
        }

        resource[chunk.Id] = surf;
    }

    private void ReadIndices(ReadChunk chunk)
    {
        List<uint> indicesVector = new List<uint>();
        while (chunk.BytesAvailable() > 0)
        {
            indicesVector.Add(chunk.Bytes.ReadUInt16());
        }
        resource[chunk.Id] = indicesVector;
    }

    private void ReadBuffer(ReadChunk chunk)
    {
        List<float> verticesVector = new List<float>();
        while (chunk.BytesAvailable() > 0)
        {
            verticesVector.Add(chunk.Bytes.ReadSingle());
        }
        resource[chunk.Id] = verticesVector;
    }

    /// <summary>
    /// 编译时常量
    /// </summary>
    private const int TEXTURE_NULL = 0;
    private const int Texture_EMBED = 1;

    private void ReadTexture(ReadChunk chunk)
    {
        Texture3D texture = null;
        bool optimized = chunk.Bytes.ReadBoolean();
        int typeMode = chunk.Bytes.ReadSByte();
        int formatMode = chunk.Bytes.ReadSByte();
        int texture_type = chunk.Bytes.ReadSByte();
        switch (texture_type)
        {
            case Texture_EMBED:
                int length = (int)chunk.Bytes.ReadUInt32();
                byte[] data = chunk.Bytes.ReadBytes(length);
                texture = new Texture3D(data, optimized, formatMode, typeMode);
                //chunk.Bytes.BaseStream.Seek(chunk.Bytes.BaseStream.Position + length, SeekOrigin.Begin);
                break;
            default:
                return;
        }
        texture.Name = ReadUtil.ReadUTF(chunk.Bytes);
        texture.FilterMode = chunk.Bytes.ReadSByte();
        texture.WrapMode = chunk.Bytes.ReadSByte();
        texture.MipMode = chunk.Bytes.ReadSByte();

        resource[chunk.Id] = texture;

    }

    private void ReadData(ReadChunk chunk)
    {
        _compressionLevel = chunk.Bytes.ReadByte();
    }

    /// <summary>
    /// Clears out all objects and resources.
    /// </summary>
    private void ClearAll()
    {
        this._strFileName = string.Empty;
    }

}
