using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json.Linq;

public class SpriteSheetEffector : IEffector
{
    private uint rows;
    private uint columns;
    /// <summary>
    /// 单位：s
    /// </summary>
    private float duration;
    private uint count;
    /// <summary>
    /// duration随生命期
    /// </summary>
    private bool lifeDuration;
    private bool randomFrame;

    public void deserialize(JObject data)
    {
        rows = (uint)data["rows"];
        columns = (uint)data["columns"];
        duration = (float)data["duration"];
        count = data["count"] == null ? rows * columns : (uint)data["count"];
        randomFrame = (bool)data["randomFrame"];
        lifeDuration = (bool)data["lifeDuration"];
    }
}
