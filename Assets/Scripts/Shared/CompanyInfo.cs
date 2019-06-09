using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class CompanyInfo : NetMsg
{
    public CompanyInfo(Company cp)
    {
        this.OP = NetOP.company;
        this.fame = cp.fame;
        this.fund = cp.fund;
        this.id = id;
    }
    public CompanyInfo()
    {
        this.OP = NetOP.company;

    }
    public int id { get; set; }
    public string nickName { get; set; }
    public float fund { get; set; }
    public float fame { get; set; }
    //public Color companyColor { get; set; }

    //public List<Block> blockList;
    //public List<Talent> talentList;

    public void setData()
    {
        List<Company> companyList = City.companyList;
        foreach( Company companyObj in companyList)
        {
            if (companyObj.id == this.id)
            {
                companyObj.nickName = nickName;
                companyObj.fame = fame;
                companyObj.fund = fund;
            }
        }
    }
}