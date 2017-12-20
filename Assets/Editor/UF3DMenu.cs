using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Runtime.InteropServices;
using System.Text;
using System.IO;

public class UF3DMenu
{
    private static string sourceFile = string.Empty;
    private static string sourceFileName = string.Empty;
    private static string sourcePath = string.Empty;

    [MenuItem("WDWebTools/导入特效", false, 0)]
    public static void OpenDiagle()
    {
        if (OpenUf3dFile())
        {
            Uf3dLoader loader = new Uf3dLoader();
            loader.parse(sourceFile);
        }
    }

    public static bool OpenUf3dFile()
    {
        OpenFileName ofn = new OpenFileName();
        ofn.structSize = Marshal.SizeOf(ofn);
        ofn.filter = "(*.uf3d)\0*.uf3d";
        ofn.file = new string(new char[1024]);
        ofn.maxFile = ofn.file.Length;
        ofn.fileTitle = new string(new char[512]);
        ofn.maxFileTitle = ofn.fileTitle.Length;
        ofn.initialDir = UnityEngine.Application.dataPath;//默认路径  
        ofn.title = "选择特效文件";
        ofn.flags = 0x00080000 | 0x00001000 | 0x00000800 | 0x00000008;//OFN_EXPLORER|OFN_FILEMUSTEXIST|OFN_PATHMUSTEXIST|OFN_NOCHANGEDIR  
        if (DllTest.GetOpenFileName(ofn))
        {
            copyData(ofn);
            return true;
        }
        return false;
    }

    private static void copyData(OpenFileName ofn)
    {
        sourceFile = ofn.file;
        sourceFileName = ofn.fileTitle;
        sourcePath = ofn.file.Substring(0, ofn.file.IndexOf(ofn.fileTitle));
    }
}