using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class ContractInfo:NetMsg
{
    public Contract contract { set; get; }
    public ContractInfo(Contract contract = null)
    {
        if (contract!=null)
        {
            this.contract = contract;
        }
    }
    public void setData()
    {
        return 
    }  
}