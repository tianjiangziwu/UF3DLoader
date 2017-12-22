using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json.Linq;

public class GradientColorEffector : IEffector
{
    private List<uint> colors = new List<uint>();
    private List<uint> alphas = new List<uint>();
    private List<float> colorRatios = new List<float>();
    private List<float> alphaRatios = new List<float>();

    private bool justAlpha = false;

    public void deserialize(JObject data)
    {
        colors = StringUtil.SplitString<uint>((string)data["colors"], new char[]{','});
        alphas = StringUtil.SplitString<uint>((string)data["alphas"], new char[] { ',' });

        colorRatios = StringUtil.SplitString<float>((string)data["colorRatios"], new char[] { ',' });
        alphaRatios = StringUtil.SplitString<float>((string)data["alphaRatios"], new char[] { ',' });

        justAlpha = (bool)data["justAlpha"];
    }
}
