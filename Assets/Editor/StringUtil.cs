using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class StringUtil
{
    public static List<T> SplitString<T>(string str, char[] separator)
    {
        List<T> ret = new List<T>();
        var datas = str.Split(separator);
        foreach(var data in datas)
        {
            ret.Add((T)Convert.ChangeType(data, typeof(T)));
        }
        return ret;
    }
}
