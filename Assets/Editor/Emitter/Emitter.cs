using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class Emitter : IDeserialize
{
    // 发射器的形状
    public IShape shape = null;
    // 单个粒子的生命周期
    public IOneDValue lifeTime;
    // 单个粒子在粒子系统启动后，延时发射的时间。只对第一个周期有效
    public IOneDValue startTime;
    // 单个粒子的初始颜色，将与采样后的颜色相乘
    public IColorValue color;
    // 单个粒子的初始Alpha
    public IOneDValue alpha;
    // 单个粒子的初始尺寸
    public IOneDValue sizeX;
    public IOneDValue sizeY;
    public IOneDValue sizeZ;
    // 单个粒子的初始速度值
    public IOneDValue velocity;
    // 单个粒子的初始旋转角度
    public IOneDValue rot;
    // 单个粒子的角速度值
    public IOneDValue rotVelocity;
    // 单个粒子的初始位置
    public UnityEngine.Vector3 initialPosition;
    // 单个粒子的初始速度方向
    public IThreeDValue direction;
    //
    public bool directionByShape;
    // YZ的缩放是否与X保持一致
    private bool _uniformScale;

    public bool UniformScale
    {
        get
        {
            return _uniformScale;
        }

        set
        {
            _uniformScale = value;
        }
    }

    public void deserialize(Newtonsoft.Json.Linq.JObject data)
    {
        deserializeAttribute(data, "shape");
        deserializeAttribute(data, "lifeTime");
        deserializeAttribute(data, "startTime");
        deserializeAttribute(data, "color");
        deserializeAttribute(data, "alpha");
        deserializeAttribute(data, "sizeX");
        deserializeAttribute(data, "sizeY");
        deserializeAttribute(data, "sizeZ");
        deserializeAttribute(data, "velocity");
        deserializeAttribute(data, "rot");
        deserializeAttribute(data, "rotVelocity");
        deserializeAttribute(data, "direction");
        directionByShape = (bool)data["directionByShape"];
    }

    private void deserializeAttribute(Newtonsoft.Json.Linq.JObject data, string v)
    {
        Newtonsoft.Json.Linq.JObject dict = data[v] as Newtonsoft.Json.Linq.JObject;
        string classType = (string)dict["type"];
        classType = classType.Substring(classType.IndexOf("::") + 2);
        //UnityEngine.Debug.LogFormat("classType{0}", classType);
        System.Type ct = System.Type.GetType(classType, true);
        IDeserialize o = (IDeserialize)Activator.CreateInstance(ct);
        //需要用到反射，但是private数据会获取不到，所以改成public
        this.GetType().GetField(v).SetValue(this, o);
        o.deserialize(dict);
    }
}
