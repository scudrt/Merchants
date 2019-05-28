using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Building : MonoBehaviour{
    //Data Area
    public string nickName; //building's name made by player
    public string buildingType;
    public Company companyBelong;
    public List<Talent> talentList;
    public int level;
    public float price;
    public float profitRate;
    //Abstract functions
    public abstract void onGenerate();
    public abstract void makeMoney();
}
