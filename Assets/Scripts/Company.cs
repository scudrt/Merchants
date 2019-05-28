using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Company : MonoBehaviour{
    public int id { get; set; }
    public float fund { get; set; }
    public float fame{get; set;}
    public Color companyColor { get; set; }

    public List<Block> blockList;
    public List<Talent> talentList;
    
    void Start() {
        this.onGenerate();
    }

    private void onGenerate() {
        this.id = 0;
        this.fund = 100000f;
        this.fame = 50f;
        this.blockList = new List<Block>();
        this.talentList = new List<Talent>();
        this.companyColor = Color.green;

        Debug.Log("Company init done");
    }
    public bool buyBlock(ref Block block) {
        //return false if block buying failed
        if (block.isOwned || this.fund < block.price) {
            return false;
        }
        this.fund -= block.price;
        block.companyBelong = this;
        block.color = this.companyColor;
        return true;
    }

    public bool buildOnBlock(ref Block block, string buildingType = "Sword") {
        //return false if building buying failed
        //TODO: this implementation is ugly
        if (block.companyBelong != this) {
            return false;
        }
        if (this.fund < Building.purchasePrice) {
            return false;
        }
        bool flag = block.build(buildingType);
        if (flag) {
            Debug.Log("building price: " + Building.purchasePrice);
            this.fund -= Building.purchasePrice;
        } else { }
        return flag;
    }

    public bool canBuildOn(Block block){ // if can build, return true and spend money
        return block.isEmpty && this.fund >= block.building.price;
    }

    // Update is called once per frame
    void Update(){
    }
}
