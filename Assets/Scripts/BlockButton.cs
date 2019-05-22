using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockButton : MonoBehaviour
{
    public BlockUI blockUI;//reference to Canvas so that all buttons can access to it

    // Start is called before the first frame update
    void Start()
    {
        blockUI = Canvas.FindObjectOfType<BlockUI>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnExitButtonClick()
    {
        SendMessageUpwards("UIExit");
    }

    public void OnBuyButtonClick()
    {
        Company company = City.currentCompany;
        if (!company.buyBlock(ref blockUI.targetBlock)){
            //block buying faied
            Debug.Log("block buying failed.");
            return;
        }

        SendMessageUpwards("UIExit");
        SendMessageUpwards("BoughtPanelEntry");
    }

    public void OnBuildButtonClick()
    {
        Block block = blockUI.targetBlock; //the Block controlled

        block.build(); //call build() function of Block

        SendMessageUpwards("UIExit");
        SendMessageUpwards("BuildingPanelEntry");
    }
}
