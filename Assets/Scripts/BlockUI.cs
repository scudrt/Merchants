using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class BlockUI : MonoBehaviour
{
    public Block targetBlock;//which Block's information to display

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
    void Update()
    {
        
    }

    public void EmptyBlockPanelEntry()
    {
        BroadcastMessage("UIExit");//make all other panel exit

        //display UI
        emptyBlockPanel.GetComponent<UIPanel>().UIEntry();

        //set the information
        emptyBlockPanel.transform.Find("BlockPrice").GetComponent<Text>().text
            = targetBlock.price.ToString();
    }

    public void BuildingInfoPanelEntry()
    {
        BroadcastMessage("UIExit");//make all other panel exit

        //display UI
        buildingInfoPanel.GetComponent<UIPanel>().UIEntry();

        //set the information
    }

    public void OwnedBlockPanelEntry()
    {
        BroadcastMessage("UIExit");//make all other panel exit

        //display UI
        ownedBlockPanel.GetComponent<UIPanel>().UIEntry();

        //set the information
    }

    public void OnBuyButtonClick()
    {
        Company company = City.currentCompany;
        if (!company.buyBlock(ref targetBlock))
        {
            //block buying faied
            Debug.Log("block buying failed.");
            return;
        }
        emptyBlockPanel.GetComponent<UIPanel>().UIExit();
        SendMessageUpwards("OwnedBlockPanelEntry");
    }

    public void OnBuildButtonClick()
    {
        //build on the block
        City.currentCompany.buildOnBlock(ref targetBlock, type);

        ownedBlockPanel.GetComponent<UIPanel>().UIExit();
        SendMessageUpwards("BuildingInfoPanelEntry");
    }

    public void OnBuildingTypeButtonClicked(string type)
    {
        this.type = type;
    }
}
