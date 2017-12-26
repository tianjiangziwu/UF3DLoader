using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class MaterialFactory
{
    public static UnityEngine.Material createMaterial(ParticleSystem ps)
    {
        UnityEngine.Material mat = new UnityEngine.Material(UnityEngine.Shader.Find(GetShaderType(ps.RenderParam.BlendMode)));
        return mat;
    }

    public static string GetShaderType(int type)
    {
        string ret = string.Empty;
        switch(type)
        {
            case RenderParam.BLEND_NONE:
                ///ONE, ZERO
                ret = "Legacy Shaders/Diffuse";
                break;
            case RenderParam.BLEND_ADDITIVE:
                //one one
                //todo ????????
                ret = "Particles/Multiply (Double)";
                break;
            case RenderParam.BLEND_ALPHA_BLENDED:
                ///SOURCE_ALPHA, ONE_MINUS_SOURCE_ALPHA
                ret = "Particles/Alpha Blended";
                break;
            case RenderParam.BLEND_ALPHA_BLENDED2:
                ///ONE, ONE_MINUS_SOURCE_ALPHA
                ret = "Particles/Alpha Blended Premultiply";
                break;
            case RenderParam.BLEND_MULTIPLY:
                //DESTINATION_COLOR, ONE_MINUS_SOURCE_ALPHA 
                //todo ?????????
                ret = "Particles/Multiply";
                break;
            case RenderParam.BLEND_SCREEN:
                ///ONE, ONE_MINUS_SOURCE_COLOR
                ret = "Particles/Additive (Soft)";
                break;
            case RenderParam.BLEND_PARTICLE_ADDITIVE:
                ///SOURCE_ALPHA, ONE
                ret = "Particles/Additive";
                break;
            case RenderParam.BLEND_PARTICLE_ALPHA_BLEND:
                ///SOURCE_ALPHA, ONE_MINUS_SOURCE_ALPHA
                ret = "Particles/Alpha Blended";
                break;
            
            default:
                ret = "Legacy Shaders/Diffuse";
                break; 
        }
        return ret;
    }
}
