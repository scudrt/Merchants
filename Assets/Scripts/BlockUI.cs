using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class BlockUI : MonoBehaviour
{
    private Block targetBlock;//which Block's information to display

    private string type = "Sword";//constructed building's type

    private GameObject emptyBlockPanel;
    private GameObject buildingInfoPanel;
    private GameObject ownedBlockPanel; //UI when the Block is bought but not constructed any Block

    // Start is called before the first frame update
    void Start()
    {
        targetBlock = null;
        emptyBlockPanel = transform.Find("EmptyBlockPanel").gameObject;
        buildingInfoPanel = transform.Find("BuildingInfoPanel").gameObject;
        ownedBlockPanel = transform.Find("OwnedBlockPanel").gameObject;
    }

    // Update is called once per frame
    void Update(){
        
    }

    public void setBlock(Block block) {
        if (this.targetBlock != null) {
            this.targetBlock.isChosen = false;
        }
        block.isChosen = true;
        this.targetBlock = block;
    }
    
    public void EmptyBlockPanelEntry() {
        //let all other panel exit
        BroadcastMessage("UIExit");

        //display UI
        emptyBlockPanel.GetComponent<UIPanel>().UIEntry();

        //set the information
        emptyBlockPanel.transform.Find("BlockPrice").GetComponent<Text>().text
            = targetBlock.price.ToString();
    }

    public void BuildingInfoPanelEntry() {
        //let all other panel exit
        BroadcastMessage("UIExit");

        //display UI
        buildingInfoPanel.GetComponent<UIPanel>().UIEntry();

        //set the information
    }

    public void OwnedBlockPanelEntry() {
        //let all other panel exit
        BroadcastMessage("UIExit");

        //display UI
        ownedBlockPanel.GetComponent<UIPanel>().UIEntry();

        //set the information
    }

    public void OnBuyButtonClick(){
        Company company = City.currentCompany;
        if (!company.buyBlock(ref targetBlock)){
            //block buying faied
            Debug.Log("block buying failed.");
            return;
        }
        emptyBlockPanel.GetComponent<UIPanel>().UIExit();
        SendMessageUpwards("OwnedBlockPanelEntry");
    }

    public void OnBuildButtonClick(){
        //build on the block
        City.currentCompany.buildOnBlock(ref targetBlock, this.type);

        ownedBlockPanel.GetComponent<UIPanel>().UIExit();
        SendMessageUpwards("BuildingInfoPanelEntry");
    }

    public void onExitButtonClicked() {
        this.targetBlock.isChosen = false;
        this.GetComponent<UIPanel>().UIExit();
    }

    public void OnBuildingTypeButtonClicked(string type){
        this.type = type;
    }
}
