using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class BlockUI : MonoBehaviour
{
    private Block targetBlock;//which Block's information to display

    private string type;//constructed building's type

    private GameObject emptyBlockPanel;
    private GameObject buildingInfoPanel;
    private GameObject ownedBlockPanel; //UI when the Block is bought but not constructed any Block

    //ownedBlockPanel's objects
    private InputField nameInput;
    private Text price;
    private ButtonGroup buildingType;

    //buildingInfoPanel's objects
    private Text profit;
    private new Text name;
    private Text typeText;

    // Start is called before the first frame update
    void Start()
    {
        targetBlock = null;
        emptyBlockPanel = transform.Find("EmptyBlockPanel").gameObject;
        buildingInfoPanel = transform.Find("BuildingInfoPanel").gameObject;
        ownedBlockPanel = transform.Find("OwnedBlockPanel").gameObject;

        nameInput = ownedBlockPanel.transform.Find("NameInput").GetComponent<InputField>();
        buildingType = ownedBlockPanel.transform.Find("BuildingType").GetComponent<ButtonGroup>();
        price = ownedBlockPanel.transform.Find("Price").GetComponent<Text>();
        buildingType.setSelect(0);//set the first type the default building type
        type = "Restaurant";
        price.text = "4000";

        profit = buildingInfoPanel.transform.Find("Profit").GetComponent<Text>();
        name = buildingInfoPanel.transform.Find("Name").GetComponent<Text>();
        typeText = buildingInfoPanel.transform.Find("Type").GetComponent<Text>();
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
        profit.text = targetBlock.building.monthlyProfit.ToString();
        name.text = targetBlock.building.nickName;
        typeText.text = targetBlock.building.buildingType;
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
        if (targetBlock.isEmpty) {
            SendMessageUpwards("OwnedBlockPanelEntry");
        } else {
            SendMessageUpwards("BuildingInfoPanelEntry");
        }

        /***test code***/
        EventManager.addEvent(delegate (Event evt){
            evt.SendMessageUpwards("BlocksManagePanelEntry", null);
        },null, "你购买了一块地！");
        /***test code***/
    }


    public void OnBuildButtonClick(){
        //build on the block
        City.currentCompany.buildOnBlock(ref targetBlock, this.type);
        
        StartCoroutine(WaitUntilBuildingComplete());
    }

    private IEnumerator WaitUntilBuildingComplete()
    {
        yield return new WaitUntil(() => { return !targetBlock.isEmpty; });

        if (targetBlock.building != null && nameInput.text != "")
        {
            targetBlock.building.nickName = nameInput.text;
        }

        ownedBlockPanel.GetComponent<UIPanel>().UIExit();
        BuildingInfoPanelEntry();
    }

    public void onExitButtonClicked() {
        this.targetBlock.isChosen = false;
        BroadcastMessage("UIExit");
    }

    public void OnBuildingTypeButtonClicked(Button button){
        buildingType.setSelect(buildingType.buttons.IndexOf(button));
        //clear the name input
        if (type != button.name)
        {
            nameInput.text = "";
        }

        type = button.name;

        

        //set price( should be modified after making price static)
        switch (type)
        {
            case "ArtGallery": price.text = ArtGallery.PRICE.ToString(); break;
            case "Bank": Bank.PRICE.ToString(); break;
            case "Cinema": Cinema.PRICE.ToString(); break;
            case "Hospital": Hospital.PRICE.ToString(); break;
            case "Restaurant": Restaurant.PRICE.ToString(); break;
            case "Scenic": Scenic.PRICE.ToString(); break;
            case "School": School.PRICE.ToString(); break;
            case "Stadium": Stadium.PRICE.ToString(); break;
            case "SuperMarket": SuperMarket.PRICE.ToString(); break;
            default: break;
        }
    }

    public void OnDetailsButtonClicked()
    {
        SendMessageUpwards("BlocksManagePanelEntry", targetBlock);
    }

    public void OnDestroyButtonClicked()
    {
        //destroy building
        targetBlock.sellBuilding();
        BroadcastMessage("UIExit");
        OwnedBlockPanelEntry();
    }
}
