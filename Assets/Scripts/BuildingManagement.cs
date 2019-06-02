using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingManagement : MonoBehaviour
{
    public GameObject blockInfoPrefab;

    private ScrollRect scrollrect;//blocks' scroll view
    private GameObject content;//content contains talent informations
    private RectTransform contentTR;
    private Transform details;

    //objects in blockInfo
    private int serial;//displayed talent's serial number in blocks list
    private Text buildingName;
    private Text industry;
    private Text costumer;
    private InputField budget;
    private Text profit;
    private Text advertising; // cost on advertising
    private Text operating; //cost of running the building
    private Slider budgetUse;

    // Start is called before the first frame update
    void Start()
    {
        blockInfoPrefab = (GameObject)Resources.Load("Prefabs/BlockInfo");

        scrollrect = transform.Find("Blocks").GetComponent<ScrollRect>();
        content = transform.Find("Blocks").transform.Find("Content").gameObject;
        contentTR = content.GetComponent<RectTransform>();
        contentTR.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 0);

        details = transform.Find("Details");
        buildingName = details.Find("Name").GetComponent<Text>();
        industry = details.Find("Industry").GetComponent<Text>();
        budget = details.Find("BudgetInput").GetComponent<InputField>();
        costumer = details.Find("Costumer").GetComponent<Text>();
        profit = details.Find("Profit").GetComponent<Text>();
        advertising = details.Find("Advertising").GetComponent<Text>();
        operating = details.Find("Operating").GetComponent<Text>();
        budgetUse = details.Find("BudgetUse").GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnOpen()
    {
        details.gameObject.SetActive(false);

        //clear the previous content
        BroadcastMessage("DestroyItemInfo");

        List<Block> blocks = City.currentCompany.blockList;
        GameObject blockInfo;
        RectTransform rectTransform;
        ItemInfo script; 
        int i = 0;//i is the number of column

        foreach (Block block in blocks)
        {
            //add talent's information
            blockInfo = GameObject.Instantiate(blockInfoPrefab, content.transform);
            rectTransform = blockInfo.GetComponent<RectTransform>();
            script = blockInfo.GetComponent<ItemInfo>();

            script.serial = i;

            //set position
            rectTransform.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Top, i * 50, 50);

            //set text
        }

        //change content rect's height
        contentTR.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, i * 50);
    }

    public void DisplayItemInfo(int _serial)
    {
        this.serial = _serial;
        
    }
}
