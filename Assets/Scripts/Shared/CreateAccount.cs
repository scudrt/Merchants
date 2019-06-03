using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class CreateAccount : NetMsg
{
    public CreateAccount()
    {
        OP = NetOP.CreateAccount;
    }

    public string userName { set; get; }
    public string passWd { set; get; }
    public string email { set; get; }
}
