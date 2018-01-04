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
    

    [MenuItem("WDWebTools/导入特效", false, 0)]
    public static void OpenDiagle()
    {
        //if (SceneFileCopy.OpenUf3dFile())
        //{
        //    Uf3dLoader loader = new Uf3dLoader();
        //    loader.parse(SceneFileCopy.SourceFile);
        //}
        Uf3dLoader loader = new Uf3dLoader();
        string[] names1 = new string[] { "Dun", "FengBaoShuangXiong_JuQi" , "FengBaoShuangXiong_QiangKouHuoHua", "ChiBang" };
        string[] names = new string[] { "smoke", "xuanfeng0", "explosion1", "xuanfeng" , "UltraISO", "baofa" , "chuizi", "shuoji", "jian1", "GLOW", "HanDiYiJi", "HuoZhiQuan", "DianShanLeiMing", "TianLei_BaoZha", "JinQuTeXiao", "TianLei", "hjnbe" };
        var tmp = names1;
        loader.parse("D:\\Users\\Administrator\\Desktop\\uf3d\\" + tmp[tmp.Length - 1] + ".uf3d");
    }
}