using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TalentsMarket : MonoBehaviour
{

    public GameObject talentInfoPrefab;

    private ScrollRect scrollrect;//talents' scroll view
    private GameObject content;//content contains talent informations
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
        talentInfoPrefab = (GameObject)Resources.Load("Prefabs/TalentContent");

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
        BroadcastMessage("DestroyTalentInfo");

        List<Talent> talents = City.currentCompany.talentList;// use unhired talent list
        GameObject talentInfo;
        RectTransform rectTransform;
        TalentInfo script;
        int i = 0;//i is the number of column

        foreach (Talent talent in talents)
        {
            //add talent's information
            talentInfo = GameObject.Instantiate(talentInfoPrefab, content.transform);
            rectTransform = talentInfo.GetComponent<RectTransform>();
            script = talentInfo.GetComponent<TalentInfo>();

            script.serial = i;

            //set position
            rectTransform.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Top, i * 50, 50);

            //set text
            talentInfo.transform.Find("Name").GetComponent<Text>().text = talent.name;
            talentInfo.transform.Find("Salary").GetComponent<Text>().text = talent.salary.ToString();

            i++;
        }

        //change content rect's height
        contentTR.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, i * 50);
    }
}
