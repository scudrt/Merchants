using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Building : MonoBehaviour{
    //Data Area
    public string nickName = "BuildingNickName"; //building's name made by player
    public string buildingType = "Building";
    public Block blockBelong = null;
    public int level = 0;
    public float price = 1000f;
    public float budget = 0f;
    public float ADBudgetProportion = 0.5f;
    //statistics data
    public float profit = 0f;
    public int customerCount = 0;
    //private data
    private float profitRate;
    private int talentCountLimit;
    private List<Talent> talentList; //index 0 is the building's manager
    //common functions
    public float getADBudget() {
        return budget * ADBudgetProportion;
    }
    public float getOPBudget() {
        return budget * (1 - ADBudgetProportion);
    }
    public int getTalentCount() {
        return talentList.Count;
    }
    //virtual functions
    public virtual void upgrade() { }
    public virtual bool addTalent(Talent talent) {
        if (talent == null) {
            return false;
        }
        if (talentList.Count == talentCountLimit) {
            return false;
        }
        talentList.Add(talent);
        return true;
    }
    public virtual bool removeTalent(int talentId) {
        for (int i = 0; i < talentList.Count; ++i) {
            if (talentList[i].id == talentId) {
                //remove the talent from this building
                talentList.RemoveAt(i);
                return true;
            }
        }
        return false;
    }
    public virtual void onGenerate() {
        //special buildings should override this function
    }
    public virtual void makeMoney() { }
}
