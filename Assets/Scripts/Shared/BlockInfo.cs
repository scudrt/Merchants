using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BlockInfo:NetMsg
{
    public BlockInfo()
    {
        this.OP = NetOP.block;
    }
    public int Pos_x { set; get; }
    public int Pos_y { set; get; }

}



