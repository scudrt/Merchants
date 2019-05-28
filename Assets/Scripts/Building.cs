using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Building : MonoBehaviour{
    public static float purchasePrice = 5000f;
    //Data Area
    public string nickName; //building's name made by player
    public string buildingType;
    public Block blockBelong;
    public int level;
    public float price;

    private float profitRate;
    private int talentCountLimit;
    private List<Talent> talentList; //index 0 is the building's leader
    //Abstract functions
    public virtual void upgrade() { }
    public virtual void addTalent(Talent talent) { }
    public virtual void removeTalent(string talentName) { }
    public virtual void onGenerate() {
        //special buildings should override this function
    }
    public virtual void makeMoney() { }
}
