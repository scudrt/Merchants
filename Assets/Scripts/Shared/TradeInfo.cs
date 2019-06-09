using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class TradeInfo : NetMsg
{
    public TradeInfo()
    {
        this.OP = NetOP.trade;
    }
    

}



