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

    //objects in blockInfo
    private int serial;//displayed talent's serial number in blocks list
    private Text payment; //cost on talents' salary
    private Text profit;
    private Text type;//building's type
    private Text advertising; // cost on advertising
    private Text run; //cost of running the building

    // Start is called before the first frame update
    void Start()
    {
        blockInfoPrefab = (GameObject)Resources.Load("Prefabs/BlockInfo");

        scrollrect = transform.Find("Blocks").GetComponent<ScrollRect>();
        content = transform.Find("Blocks").transform.Find("Content").gameObject;
        contentTR = content.GetComponent<RectTransform>();
        contentTR.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnOpen()
    {
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
}
