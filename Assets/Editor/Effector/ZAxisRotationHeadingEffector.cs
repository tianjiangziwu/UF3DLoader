using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json.Linq;
using UnityEngine;

public class ZAxisRotationHeadingEffector : IEffector
{
    public void ApplyToUnityParticleSystem(UnityEngine.ParticleSystem ups, ParticleSystem ps)
    {
        var material = UnityEditor.AssetDatabase.LoadAssetAtPath(ps.UnityResourceParam.MaterialPath, typeof(Material)) as Material;
        var texture = UnityEditor.AssetDatabase.LoadAssetAtPath(ps.UnityResourceParam.Texture2DPath, typeof(Texture2D)) as Texture2D;
        var newtexture = ImageUtil.rotateTexture180(texture);
        newtexture.alphaIsTransparency = true;
        newtexture.filterMode = texture.filterMode;
        newtexture.wrapMode = texture.wrapMode;
        newtexture.name = texture.name + "_rot180";
        material.SetTexture("_MainTex", newtexture);
        float baseTilingX = 1f;
        float baseTilingY = 1f;
        float baseOffsetX = 0f;
        float baseOffsetY = 0f;
        material.SetTextureScale("_MainTex", new Vector2(baseTilingX, baseTilingY));
        material.SetTextureOffset("_MainTex", new Vector2(baseOffsetX, baseOffsetY));
        uint color = 0xFFFFFFFF;
        material.SetColor("_Color", new Color(((color >> 16) & 0xFF) / 255.0f,
                                        ((color >> 8) & 0xFF) / 255.0f,
                                        (color & 0xFF) / 255.0f,
                                        ((color >> 24) & 0xFF) / 255.0f));
        ParticleSystemRenderer render = ups.GetComponent<ParticleSystemRenderer>();
        render.material = material;
        render.lengthScale = 1.0f;
        render.renderMode = ParticleSystemRenderMode.Stretch;
    }

    public void deserialize(JObject data)
    {
        
    }
}
