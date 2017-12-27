using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using System.IO;

public class ParticleSystemAssembler
{
    public static int Assemble(Uf3dLoader loader)
    {
        AssembleParticleSystem(loader);
        return 0;
    }

    

    private static void AssembleParticleSystem(Uf3dLoader loader)
    {
        Dictionary<int, GameObject> particles = new Dictionary<int, GameObject>();
        for (int i = 0; i < loader.ParticleSystemList.Count; i++)
        {
            ParticleSystem ps = loader.ParticleSystemList[i];
            var tex2D = AssembleTexture(ps, loader.Resource);
            var mtl = AssembleMaterial(ps, loader.Resource);
            var mesh = AssembleMesh(ps, loader.Resource);
            var unityParticleSystem = new GameObject(ps.Name);
            unityParticleSystem.AddComponent<UnityEngine.ParticleSystem>();
            if (ps.Parent != -1)
            {
                var father = particles[ps.Parent];
                unityParticleSystem.transform.parent = father.transform;
            }
            var matrix = ps.Matrix;
            
            TransformUtil.SetTransformFromMatrix(unityParticleSystem.transform, ref matrix);

            UnityEngine.ParticleSystem ups = unityParticleSystem.GetComponent<UnityEngine.ParticleSystem>();

            //StreamWriter sw = new StreamWriter("D:\\ps.txt", false, Encoding.UTF8);
            //sw.WriteLine(string.Format("displayName \t propertyPath \t type"));
            //UnityEditor.SerializedObject so = new UnityEditor.SerializedObject(ups);
            //UnityEditor.SerializedProperty it = so.GetIterator();
            //while (it.Next(true))
            //{
            //    UnityEngine.Debug.Log(it.propertyPath);
            //    int count = it.propertyPath.Count(f => f == '.');
            //    sw.WriteLine(string.Format("{0}{1},{2},{3},{4}", (count == 0 ? "" : String.Empty.PadLeft(count, '-')), it.displayName, it.propertyPath, it.type, it.tooltip));
            //}
            //sw.Close();
            FillMainModule(ups, ps);
            FillEmissionModule(ups, ps);
            FillShapeModule(ups, ps);
            FillRenderModule(ups, ps);
            
            particles.Add(ps.ChunkId, unityParticleSystem);
        }
    }

    /// <summary>
    /// 粒子发色器的形状
    /// </summary>
    /// <param name="ups"></param>
    /// <param name="ps"></param>
    private static void FillShapeModule(UnityEngine.ParticleSystem ups, ParticleSystem ps)
    {
        var shape = ups.shape;
        shape.alignToDirection = ps.Emitter.directionByShape;
        if (ps.Emitter.shape is BoxShape)
        {
            var asBoxShape = ps.Emitter.shape as BoxShape;
            shape.shapeType = ParticleSystemShapeType.Box;
            shape.box = asBoxShape.getScale();
            
        }
    }

    private static void FillEmissionModule(UnityEngine.ParticleSystem ups, ParticleSystem ps)
    {
        
    }

    private static void FillMainModule(UnityEngine.ParticleSystem ups, ParticleSystem ps)
    {
        UnityEngine.ParticleSystem.MainModule main = ups.main;
        main.maxParticles = ps.RenderParam.ParticleNumber;
        main.loop = ps.RenderParam.Loop;
        main.simulationSpace = ps.RenderParam.IsWorldParticle ? ParticleSystemSimulationSpace.World : ParticleSystemSimulationSpace.Local;
        main.prewarm = ps.RenderParam.Prewarm > 0.0f ? true : false;

        //旋转轴向
        if (ps.RenderParam.RotateAxis == RenderParam.RotateAxis_X)
        {
            main.startRotation3D = true;
            main.startRotationX = new UnityEngine.ParticleSystem.MinMaxCurve(-180.0f, 180.0f);
            main.startRotationY = new UnityEngine.ParticleSystem.MinMaxCurve(0.0f, 0.0f);
            main.startRotationZ = new UnityEngine.ParticleSystem.MinMaxCurve(0.0f, 0.0f);
        }
        else if (ps.RenderParam.RotateAxis == RenderParam.RotateAxis_Y)
        {
            main.startRotation3D = true;
            main.startRotationX = new UnityEngine.ParticleSystem.MinMaxCurve(0.0f, 0.0f);
            main.startRotationY = new UnityEngine.ParticleSystem.MinMaxCurve(-180.0f, 180.0f);
            main.startRotationZ = new UnityEngine.ParticleSystem.MinMaxCurve(0.0f, 0.0f);
        }
        else if (ps.RenderParam.RotateAxis == RenderParam.RotateAxis_Z)
        {
            main.startRotation3D = true;
            main.startRotationX = new UnityEngine.ParticleSystem.MinMaxCurve(0.0f, 0.0f);
            main.startRotationY = new UnityEngine.ParticleSystem.MinMaxCurve(0.0f, 0.0f);
            main.startRotationZ = new UnityEngine.ParticleSystem.MinMaxCurve(-180.0f, 180.0f); 
        }
        else
        {
            main.startRotation3D = false;
        }

        main.startSize3D = true;
        if (ps.Emitter.UniformScale)
        {
            main.startSizeX = ps.Emitter.sizeX.getCurve();
            main.startSizeY = main.startSizeX;
            main.startSizeZ = main.startSizeX;
        }
        else
        {
            main.startSizeX = ps.Emitter.sizeX.getCurve();
            main.startSizeY = ps.Emitter.sizeY.getCurve();
            main.startSizeZ = ps.Emitter.sizeZ.getCurve();
        }

        main.startLifetime = ps.Emitter.lifeTime.getCurve();
        main.startColor = ps.Emitter.color.getGradient();
        var alpha = ps.Emitter.alpha.getCurve();
        main.startColor = MergeColorAndAlpha(main.startColor, alpha);

    }

    private static UnityEngine.ParticleSystem.MinMaxGradient MergeColorAndAlpha(UnityEngine.ParticleSystem.MinMaxGradient startColor, UnityEngine.ParticleSystem.MinMaxCurve alpha)
    {
        UnityEngine.ParticleSystem.MinMaxGradient ret = startColor;
        if (startColor.mode == ParticleSystemGradientMode.Color && alpha.mode == ParticleSystemCurveMode.Constant)
        {
            ret = new UnityEngine.ParticleSystem.MinMaxGradient(new Color(startColor.color.r, startColor.color.g, startColor.color.b, alpha.constant));
        }
        else if (startColor.mode == ParticleSystemGradientMode.Color && alpha.mode == ParticleSystemCurveMode.Curve)
        {
            var alphaKeys = new GradientAlphaKey[alpha.curve.length];
            var colorKeys = new GradientColorKey[2];
            colorKeys[0].time = 0.0f;
            colorKeys[1].time = 1.0f;
            colorKeys[0].color = startColor.color;
            colorKeys[1].color = startColor.color;
            for (int i = 0; i < alphaKeys.Length; ++i)
            {
                alphaKeys[i].time = alpha.curve.keys[i].time;
                alphaKeys[i].alpha = alpha.curve.keys[i].value;
            }
            var gradient = new Gradient();
            gradient.alphaKeys = alphaKeys;
            gradient.colorKeys = colorKeys;
            ret = new UnityEngine.ParticleSystem.MinMaxGradient(gradient);
        }
        else if (startColor.mode == ParticleSystemGradientMode.Gradient && alpha.mode == ParticleSystemCurveMode.Constant)
        {
            var alphaKeys = startColor.gradient.alphaKeys;
            for (int i = 0; i < alphaKeys.Length; ++i)
            {
                alphaKeys[i].alpha = alpha.constant;
            }
            var gradient = new Gradient();
            gradient.alphaKeys = alphaKeys;
            gradient.colorKeys = startColor.gradient.colorKeys;
            ret = new UnityEngine.ParticleSystem.MinMaxGradient(gradient);
        }
        else if (startColor.mode == ParticleSystemGradientMode.Gradient && alpha.mode == ParticleSystemCurveMode.Curve)
        {
            var alphaKeys = new GradientAlphaKey[alpha.curve.length];
            for (int i = 0; i < alphaKeys.Length; ++i)
            {
                alphaKeys[i].time = alpha.curve.keys[i].time;
                alphaKeys[i].alpha = alpha.curve.keys[i].value;
            }
            var gradient = new Gradient();
            gradient.alphaKeys = alphaKeys;
            gradient.colorKeys = startColor.gradient.colorKeys;
            ret = new UnityEngine.ParticleSystem.MinMaxGradient(gradient);
        }
        else
        {
            Debug.LogFormat("ParticleSystemGradientMode:{0},ParticleSystemCurveMode:{1}颜色和Alpha通道的合并未实现", startColor.mode, alpha.mode);
        }
        return ret;
    }

    private static void FillRenderModule(UnityEngine.ParticleSystem ups, ParticleSystem ps)
    {
        ParticleSystemRenderer psr = ups.GetComponent<ParticleSystemRenderer>();
        psr.sortingOrder = ps.Layer;
        psr.renderMode = (ParticleSystemRenderMode)ps.RenderParam.BillboardType;
        psr.material = UnityEditor.AssetDatabase.LoadAssetAtPath(ps.UnityResourceParam.MaterialPath, typeof(Material)) as Material;

    }

    private static Mesh AssembleMesh(ParticleSystem ps, Dictionary<int, object> resource)
    {
        Mesh mesh = null;
        if (ps.SurfId != 0)
        {
            //Surface3D surf = resource[(int)ps.SurfId] as Surface3D;
            //obj = new GameObject("ps.Name");
            //obj.AddComponent<MeshFilter>();
            //obj.AddComponent<MeshRenderer>();
            //obj.GetComponent<MeshRenderer>().sharedMaterial = mtl;

            ps.UnityResourceParam.MeshPath = SceneFileCopy.GetRelativeMeshDir() + ps.Name + ".prefab";

            Surface3D surf = resource[(int)ps.SurfId] as Surface3D;
            mesh = new Mesh();
            mesh.name = ps.Name;
            int verticesCountInMesh = surf.VertexVector.Count / surf.SizePerVertex;
            Vector3[] xyz = new Vector3[surf.NumTriangles * 3];
            Vector2[] uv1 = new Vector2[surf.NumTriangles * 3];

            for (int i = 0; i < verticesCountInMesh; ++i)
            {
                xyz[i] = new Vector3(
                    surf.VertexVector[i * surf.SizePerVertex + surf.Offset[Surface3D.POSITION] + 0],
                    surf.VertexVector[i * surf.SizePerVertex + surf.Offset[Surface3D.POSITION] + 1],
                    surf.VertexVector[i * surf.SizePerVertex + surf.Offset[Surface3D.POSITION] + 2]
                    );
                uv1[i] = new Vector2(
                    surf.VertexVector[i * surf.SizePerVertex + surf.Offset[Surface3D.UV0] + 0],
                    surf.VertexVector[i * surf.SizePerVertex + surf.Offset[Surface3D.UV0] + 1]
                    );
            }

            int[] triangles = new int[surf.IndexVector.Count];
            for (int i = 0; i < triangles.Length; ++i)
            {
                triangles[i] = (int)surf.IndexVector[i];
            }
            mesh.vertices = xyz;
            mesh.uv = uv1;
            mesh.triangles = triangles;
            mesh.RecalculateNormals();

            UnityEditor.AssetDatabase.CreateAsset(mesh, ps.UnityResourceParam.MeshPath);
            UnityEditor.AssetDatabase.Refresh();
        }

        return mesh;
    }

    private static Material AssembleMaterial(ParticleSystem ps, Dictionary<int, object> resource)
    {
        string texName = (resource[ps.TexId] as Texture3D).Name;

        ps.UnityResourceParam.MaterialPath = SceneFileCopy.GetRelativeMaterialDir() + texName.Substring(0, texName.Length - 4) + ".mat";

        Material mtl = MaterialFactory.createMaterial(ps);

        mtl.SetTexture("_MainTex", UnityEditor.AssetDatabase.LoadAssetAtPath(ps.UnityResourceParam.Texture2DPath, typeof(Texture2D)) as Texture2D);
        float baseTilingX = 1f;
        float baseTilingY = 1f;
        float baseOffsetX = 0f;
        float baseOffsetY = 0f;
        mtl.SetTextureScale("_MainTex", new Vector2(baseTilingX, baseTilingY));
        mtl.SetTextureOffset("_MainTex", new Vector2(baseOffsetX, baseOffsetY));
        uint color = 0xFFFFFFFF;
        mtl.SetColor("_Color", new Color(((color >> 16) & 0xFF) / 255.0f,
                                        ((color >> 8) & 0xFF) / 255.0f,
                                        (color & 0xFF) / 255.0f,
                                        ((color >> 24) & 0xFF) / 255.0f));
        UnityEditor.AssetDatabase.CreateAsset(mtl, ps.UnityResourceParam.MaterialPath);
        UnityEditor.AssetDatabase.Refresh();
        return mtl;
    }

    /// <summary>
    /// 把纹理放到编辑器中去
    /// </summary>
    /// <param name="ps"></param>
    private static Texture2D AssembleTexture(ParticleSystem ps, Dictionary<int, object> resource)
    {
        Texture2D tex2D = null;
        Texture3D tex = resource[ps.TexId] as Texture3D;
        if (tex.IsATF)
        {

        }
        else
        {
            //实例化一个Texture2D，宽和高设置可以是任意的，因为当使用LoadImage方法会对Texture2D的宽和高会做相应的调整
            //Texture2D tex2D = new Texture2D(1,1);
            //tex2D.LoadImage(tex.Data);
            SaveFile(SceneFileCopy.GetAbsoluteTextureDir() + tex.Name, tex.Data);
            ps.UnityResourceParam.Texture2DPath = SceneFileCopy.GetRelativeTextureDir() + tex.Name;
            tex2D = UnityEditor.AssetDatabase.LoadAssetAtPath(ps.UnityResourceParam.Texture2DPath, typeof(Texture2D)) as Texture2D;
            UnityEditor.TextureImporter textureImporter = UnityEditor.AssetImporter.GetAtPath(ps.UnityResourceParam.Texture2DPath) as UnityEditor.TextureImporter;
            UnityEditor.TextureImporterSettings settings = new UnityEditor.TextureImporterSettings();
            textureImporter.ReadTextureSettings(settings);
            settings.ApplyTextureType(UnityEditor.TextureImporterType.Default);
            textureImporter.SetTextureSettings(settings);
            textureImporter.textureType = UnityEditor.TextureImporterType.Default;
            //使用透明度
            textureImporter.alphaIsTransparency = true;
            textureImporter.isReadable = true;
            textureImporter.filterMode = (UnityEngine.FilterMode)tex.FilterMode;
            textureImporter.wrapMode = (UnityEngine.TextureWrapMode)tex.WrapMode;
            textureImporter.mipmapEnabled = tex.MipMode > 0;
            UnityEditor.AssetDatabase.ImportAsset(ps.UnityResourceParam.Texture2DPath);
        }
        UnityEditor.AssetDatabase.Refresh();
        return tex2D;
    }


    /// <summary>
    /// 通过文件名和数据来保存文件
    /// </summary>
    /// <param name="fileName"></param>
    /// <param name="data"></param>
    private static void SaveFile(string fileName, byte[] data)
    {
        if (File.Exists(fileName))
        {
            File.Delete(fileName);
        }
        File.WriteAllBytes(fileName, data);
        UnityEditor.AssetDatabase.Refresh();
    }
}
