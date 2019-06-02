using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TalentsMarket : MonoBehaviour
{

    public GameObject talentInfoPrefab;

    private ScrollRect scrollrect;//talents' scroll view
    private GameObject content;//content contains talent informations
    private TalentManageUI talentManageUI;
    private RectTransform contentTR;

    //details panel's objects
    private int serial;//displayed talent's serial number in talents list
    private Transform details;
    private Text talentName;
    private Text capacity;
    private Text charm;
    private Text salary;

    // Start is called before the first frame update
    void Start()
    {
        talentInfoPrefab = (GameObject)Resources.Load("Prefabs/UnhiredTalentInfo");

        talentManageUI = GameObject.FindObjectOfType<TalentManageUI>();

        scrollrect = transform.Find("Talents").GetComponent<ScrollRect>();
        content = transform.Find("Talents").transform.Find("Content").gameObject;
        contentTR = content.GetComponent<RectTransform>();
        contentTR.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 0);

        details = transform.Find("Details");
        talentName = details.Find("Name").GetComponent<Text>();
        capacity = details.Find("Capacity").GetComponent<Text>();
        charm = details.Find("Charm").GetComponent<Text>();
        salary = details.Find("Salary").GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnOpen()
    {
        //disable the details panel
        details.gameObject.SetActive(false);

        //clear the previous content
        BroadcastMessage("DestroyItemInfo");

        List<Talent> talents = City.talentsMarketList;
        GameObject talentInfo;
        RectTransform rectTransform;
        ItemInfo script;
        int i = 0;//i is the number of column

        foreach (Talent talent in talents)
        {
            //add talent's information
            talentInfo = GameObject.Instantiate(talentInfoPrefab, content.transform);
            rectTransform = talentInfo.GetComponent<RectTransform>();
            script = talentInfo.GetComponent<ItemInfo>();

            script.serial = i;

            //set position
            rectTransform.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Top, i * 50, 50);

            //set text
            talentInfo.transform.Find("Name").GetComponent<Text>().text = talent.name;
            talentInfo.transform.Find("Salary").GetComponent<Text>().text = talent.salary.ToString();
            talentInfo.transform.Find("Capacity").GetComponent<Text>().text = talent.capacity.ToString();
            talentInfo.transform.Find("Charm").GetComponent<Text>().text = talent.charm.ToString();


            i++;
        }

        //change content rect's height
        contentTR.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, i * 50);
    }

    public void DisplayItemInfo(int _serial)
    {
        this.serial = _serial;
        details.gameObject.SetActive(true);

        Talent talent = City.talentsMarketList[serial];

        talentName.text = talent.name;
        capacity.text = talent.capacity.ToString();
        charm.text = talent.charm.ToString();
        salary.text = talent.salary.ToString();
    }

    public void OnHireButtonClicked()
    {
        City.currentCompany.talentList.Add(City.talentsMarketList[serial]);
        City.talentsMarketList.RemoveAt(serial);
        this.OnOpen();
        talentManageUI.OnOpen();
    }
}
