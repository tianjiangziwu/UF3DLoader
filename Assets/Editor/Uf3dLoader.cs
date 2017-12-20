using UnityEngine;
using System.Collections;
using System.IO;
using System;

public class Uf3dLoader {

    private string _strFileName = string.Empty;

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
                            // create a BinaryReader used to read the Targa file
                            using (binReader = new BinaryReader(filestream))
                            {
                                //this.LoadTGAFooterInfo(binReader);
                                //this.LoadTGAHeaderInfo(binReader);
                                //this.LoadTGAExtensionArea(binReader);
                                //this.LoadTGAImage(binReader);
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
}
