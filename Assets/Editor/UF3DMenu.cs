using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Runtime.InteropServices;
using System.Text;
using System.IO;

/// <summary>
/// 与UnityEditor编辑器相关
/// </summary>
public class UF3DMenu
{
    public static List<string> GetFilesPath(string dir)
    {
        List<string> ret = new List<string>();
        DirectoryInfo info = new DirectoryInfo(dir);
        foreach (FileInfo child in info.GetFiles("*.uf3d", SearchOption.AllDirectories))
        {
            ret.Add(child.FullName);
        }
        return ret;
    }

    [MenuItem("WDWebTools/导入特效", false, 0)]
    public static void OpenDiagle()
    {
        //if (SceneFileCopy.OpenUf3dFile())
        //{
        //    Uf3dLoader loader = new Uf3dLoader();
        //    loader.parse(SceneFileCopy.SourceFile);
        //}
        var list = GetFilesPath(@"D:\特效\out");
        foreach (var item in list)
        {
            Uf3dLoader loader = new Uf3dLoader();
            SceneFileCopy.openUF3D(item);
            loader.parse(item);
        }
        

        //"D:\特效\out\战斗中特效\粒子特效\命中效果\圆心扩散\冲击波 绿\ZhenJi.uf3d"
        //D:\特效\out\待整理文档\2017.8月需求\RPG\征服者机甲\核子轰击
        //"D:\特效\out\翅膀分类专题\创世之翼\ChiBangShouQi.uf3d"
        //Uf3dLoader loader = new Uf3dLoader();
        //string[] names1 = new string[] { "Dun", "FengBaoShuangXiong_JuQi" , "FengBaoShuangXiong_QiangKouHuoHua", "ChiBang", "ChiBangShouQi", "BeiJi", "c1"};
        //string[] names = new string[] { "smoke", "xuanfeng0", "explosion1", "xuanfeng" , "UltraISO", "baofa" , "chuizi", "shuoji", "jian1", "GLOW", "HanDiYiJi", "HuoZhiQuan", "DianShanLeiMing", "TianLei_BaoZha", "JinQuTeXiao", "TianLei", "hjnbe" };
        //var tmp = names;
        //loader.parse("D:\\Users\\Administrator\\Desktop\\uf3d\\" + tmp[tmp.Length - 4] + ".uf3d");
    }
}