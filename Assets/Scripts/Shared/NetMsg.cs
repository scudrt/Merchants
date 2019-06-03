﻿public static class NetOP
{
    public const int None = 0;
    public const int CreateAccount = 1;
    public const int block = 2;
}
[System.Serializable]
public abstract class NetMsg
{
    public byte OP { set; get; }
    public float x { set; get; }
    public float y { set; get; }

    public NetMsg()
    {
        OP = NetOP.None;
    }
}