using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Runtime.InteropServices;
using System.Text;
using System.IO;

[InitializeOnLoad]
public class SceneFileCopy
{
    public static string unityCurrentDir = System.IO.Directory.GetCurrentDirectory();
    // Assets Directory
    public static string workBaseDirectory = Application.dataPath;

    private static string sourceFile = string.Empty;
    private static string sourceFileName = string.Empty;
    private static string sourcePath = string.Empty;

    public static string SourceFile
    {
        get
        {
            return sourceFile;
        }

        set
        {
            sourceFile = value;
        }
    }

    public static string SourceFileName
    {
        get
        {
            return sourceFileName;
        }

        set
        {
            sourceFileName = value;
        }
    }

    public static string SourcePath
    {
        get
        {
            return sourcePath;
        }

        set
        {
            sourcePath = value;
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
        SourceFileName = ofn.fileTitle;
        SourcePath = ofn.file.Substring(0, ofn.file.IndexOf(ofn.fileTitle));
        createDirectory(GetAbsoluteTextureDir());
        createDirectory(GetAbsoluteMaterialDir());
    }

    public static string GetAbsoluteTextureDir()
    {
        return workBaseDirectory + "/Resources/Textures/";
    }

    public static string GetRelativeTextureDir()
    {
        return "Assets/Resources/Textures/";
    }

    public static string GetAbsoluteMaterialDir()
    {
        return workBaseDirectory + "/Resources/Materials/";
    }

    public static string GetRelativeMaterialDir()
    {
        return "Assets/Resources/Materials/";
    }

    public static void createDirectory(string path, bool removeOld = false)
    {
        if (Directory.Exists(path))
        {
            if (removeOld)
            {
                foreach (var v in Directory.GetFileSystemEntries(path))
                {
                    if (File.Exists(v))
                    {
                        File.Delete(v);
                    }
                    else
                    {
                        Directory.Delete(v, true);
                    }
                }
            }
        }
        else
        {
            Directory.CreateDirectory(path);
        }
    }

}
