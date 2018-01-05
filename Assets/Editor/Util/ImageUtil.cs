using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class ImageUtil
{

    public static Texture2D rotateTexture180(Texture2D image)
    {
        Texture2D target = new Texture2D(image.width, image.height, TextureFormat.ARGB32, true);    //flip image width<>height, as we rotated the image, it might be a rect. not a square image

        Color32[] pixels = image.GetPixels32(0);
        pixels = rotateTextureGrid180(pixels, image.width, image.height);
        target.SetPixels32(pixels);
        target.Apply();

        //flip image width<>height, as we rotated the image, it might be a rect. not a square image
        return target;
    }

    //顺时针转180度
    private static Color32[] rotateTextureGrid180(Color32[] tex, int wid, int hi)
    {
        Color32[] ret = new Color32[wid * hi];      //reminder we are flipping these in the target

        int len = wid * hi;
        for (int i = 0; i < len; ++i)
        {
            ret[i] = tex[len - 1 - i];
        }

        return ret;
    }

    public static Texture2D rotateTexture(Texture2D image, bool isClockWise = true)
    {

        Texture2D target = new Texture2D(image.height, image.width, TextureFormat.ARGB32, true);    //flip image width<>height, as we rotated the image, it might be a rect. not a square image

        Color32[] pixels = image.GetPixels32(0);
        pixels = rotateTextureGrid(pixels, image.width, image.height, isClockWise);
        target.SetPixels32(pixels);
        target.Apply();

        //flip image width<>height, as we rotated the image, it might be a rect. not a square image
        return target;
    }

    //顺时针转90度
    private static Color32[] rotateTextureGrid(Color32[] tex, int wid, int hi, bool isClockWise = true)
    {
        Color32[] ret = new Color32[wid * hi];      //reminder we are flipping these in the target


        for (int y = 0; y < hi; y++)
        {
            for (int x = 0; x < wid; x++)
            {
                if (isClockWise)
                {
                    ret[(hi - 1) - y + x * hi] = tex[x + y * wid];         //juggle the pixels around
                }
                else
                {
                    ret[y + (wid - 1 - x) * hi] = tex[x + y * wid];
                }
            }
        }

        return ret;
    }
}