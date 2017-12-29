using System.Collections.Generic;
using UnityEngine;

public static class PrimitiveHelper
{
    private static Dictionary<PrimitiveType, Mesh> primitiveMeshes = new Dictionary<PrimitiveType, Mesh>();

    public static GameObject CreatePrimitive(PrimitiveType type, bool withCollider)
    {
        if (withCollider) { return GameObject.CreatePrimitive(type); }

        GameObject gameObject = new GameObject(type.ToString());
        MeshFilter meshFilter = gameObject.AddComponent<MeshFilter>();
        meshFilter.sharedMesh = PrimitiveHelper.GetPrimitiveMesh(type, true);
        gameObject.AddComponent<MeshRenderer>();

        return gameObject;
    }

    public static Mesh GetPrimitiveMesh(PrimitiveType type, bool rotate = false)
    {
        if (!PrimitiveHelper.primitiveMeshes.ContainsKey(type))
        {
            PrimitiveHelper.CreatePrimitiveMesh(type, rotate);
        }

        return PrimitiveHelper.primitiveMeshes[type];
    }

    private static Mesh CreatePrimitiveMesh(PrimitiveType type, bool rotate = false)
    {
        GameObject gameObject = GameObject.CreatePrimitive(type);
        Mesh mesh = gameObject.GetComponent<MeshFilter>().sharedMesh;

        var retMesh = new Mesh();
        retMesh.name = "quad00";
        retMesh.vertices = mesh.vertices;
        retMesh.uv = mesh.uv;
        retMesh.triangles = mesh.triangles;
        //UnityEditor.AssetDatabase.CreateAsset(tmpmesh, SceneFileCopy.GetRelativeMeshDir() + "quad00.prefab");
        //UnityEditor.AssetDatabase.Refresh();

        if (rotate)
        {
            RotateMeshVertices(retMesh, new Vector3(0, 0, 0), new Vector3(90, 0, 0));
            retMesh.RecalculateBounds();
            retMesh.RecalculateNormals();
        }

        PrimitiveHelper.primitiveMeshes[type] = retMesh;

        GameObject.DestroyImmediate(gameObject);
        return retMesh;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="mesh"></param>
    /// <param name="center">any V3 you want as the pivot point.</param>
    /// <param name="eulerAngles">the degrees the vertices are to be rotated, for example (0,90,0) </param>
    public static void RotateMeshVertices(Mesh mesh, Vector3 center, Vector3 eulerAngles)
    {
        Quaternion newRotation = new Quaternion();
        newRotation.eulerAngles = eulerAngles;//
        Vector3[] verts = mesh.vertices;
        for (int i = 0; i < verts.Length; i++)
        {//vertices being the array of vertices of your mesh
            verts[i] = newRotation * (verts[i] - center) + center;
        }
        mesh.vertices = verts;
        mesh.name = "quad90";
        //var tmpmesh = new Mesh();
        //tmpmesh.name = "quad90";
        //tmpmesh.vertices = verts;
        //tmpmesh.uv = mesh.uv;
        //tmpmesh.triangles = mesh.triangles;
        //UnityEditor.AssetDatabase.CreateAsset(tmpmesh, SceneFileCopy.GetRelativeMeshDir() + "quad90.prefab");
        //UnityEditor.AssetDatabase.Refresh();
    }

    private static Mesh BuildQuad(float width, float height)
    {
        Mesh mesh = new Mesh();

        // Setup vertices
        Vector3[] newVertices = new Vector3[4];
        float halfHeight = height * 0.5f;
        float halfWidth = width * 0.5f;
        newVertices[0] = new Vector3(-halfWidth, -halfHeight, 0);
        newVertices[1] = new Vector3(-halfWidth, halfHeight, 0);
        newVertices[2] = new Vector3(halfWidth, -halfHeight, 0);
        newVertices[3] = new Vector3(halfWidth, halfHeight, 0);

        // Setup UVs
        Vector2[] newUVs = new Vector2[newVertices.Length];
        newUVs[0] = new Vector2(0, 0);
        newUVs[1] = new Vector2(0, 1);
        newUVs[2] = new Vector2(1, 0);
        newUVs[3] = new Vector2(1, 1);

        // Setup triangles
        int[] newTriangles = new int[] { 0, 1, 2, 3, 2, 1 };

        // Setup normals
        Vector3[] newNormals = new Vector3[newVertices.Length];
        for (int i = 0; i < newNormals.Length; i++)
        {
            newNormals[i] = Vector3.forward;
        }

        // Create quad
        mesh.vertices = newVertices;
        mesh.uv = newUVs;
        mesh.triangles = newTriangles;
        mesh.normals = newNormals;

        return mesh;
    }
}