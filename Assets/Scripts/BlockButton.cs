using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockButton : MonoBehaviour
{

    public Canvas BlockUI;//reference to BlockUI so that all buttons can access to it

    public Object prefab;//Block's prefab

    // Start is called before the first frame update
    void Start()
    {
        
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
        Company onlyCompany = UnityEngine.Object.FindObjectOfType<Company>();
        Block block = BlockUI.GetComponent<BlockUI>().Block.GetComponent<Block>();
        bool flag = onlyCompany.tryBuyBlock(ref block);
        Debug.Log(flag);

        SendMessageUpwards("UIExit");
        SendMessageUpwards("BoughtPanelEntry");
    }

    public void OnBuildButtonClick()
    {
        GameObject Block = BlockUI.GetComponent<BlockUI>().Block; //the Block controlled

        Block.GetComponent<Block>().build(); //call build() function of Block

        GameObject newBuilding = (GameObject) GameObject.Instantiate(prefab, Block.transform);

        SendMessageUpwards("UIExit");
        SendMessageUpwards("BuildingPanelEntry");
    }
}
