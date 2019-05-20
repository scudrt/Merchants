using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Company : MonoBehaviour{
    public int companyId { get; set; }
    public float fund { get; set; }
    public float fame{get; set;}

    private List<Block> blockList;
    private List<Talent> talentList;

    // Start is called before the first frame update
    void Start(){
        this.companyId = 0;
        this.fund = 0f;
        this.fame = 50f;
        this.blockList = new List<Block>();
        this.talentList = new List<Talent>();
    }

    public bool tryBuyBlock(ref Block block){
        if (block.canBeOwned() && this.fund >= block.price){
            block.belong = this.companyId;
            this.fund -= block.price;
            blockList.Add(block);
            return true;
        }
        else{
            return false;
        }
    }

    public bool tryBuild(ref Block block){
        if (block.isEmpty && this.fund >= block.building.price) {
            this.fund -= block.building.price;
            block.build();
            return true;
        } else {
            return false;
        }
    }

    // Update is called once per frame
    void Update(){
        
    }
}
