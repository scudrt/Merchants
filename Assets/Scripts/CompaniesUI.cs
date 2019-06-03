using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CompaniesUI : MonoBehaviour
{
    public GameObject companyInfoPrefab;

    private ScrollRect scrollrect;//companies' scroll view
    private GameObject content;//content contains company informations
    private RectTransform contentTR;
    private Transform details;

    //details' objs
    private int serial;//displayed serial number
    private Text companyName;
    private Text property;
    private Text reputation;

    // Start is called before the first frame update
    void Start()
    {
        companyInfoPrefab = (GameObject)Resources.Load("Prefabs/CompanyInfo");

        scrollrect = transform.Find("Companies").GetComponent<ScrollRect>();
        content = transform.Find("Companies").transform.Find("Content").gameObject;
        contentTR = content.GetComponent<RectTransform>();
        contentTR.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 0);

        details = transform.Find("Details");
        companyName = details.Find("Name").GetComponent<Text>();
        property = details.Find("Property").GetComponent<Text>();
        reputation = details.Find("Reputation").GetComponent<Text>();
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

        List<Company> companies = City.companyList;
        GameObject companyInfo;
        RectTransform rectTransform;
        ItemInfo script;
        int i = 0;//i is the number of column

        foreach (Company company in companies)
        {
            //add talent's information
            companyInfo = GameObject.Instantiate(companyInfoPrefab, content.transform);
            rectTransform = companyInfo.GetComponent<RectTransform>();
            script = companyInfo.GetComponent<ItemInfo>();

            script.serial = i;

            //set position
            rectTransform.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Top, i * 50, 50);

            //set text
            companyInfo.transform.Find("Name").GetComponent<Text>().text = company.nickName;
            companyInfo.transform.Find("ID").GetComponent<Text>().text = company.id.ToString();
        }

        //change content rect's height
        contentTR.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, i * 50);
    }

    public void DisplayItemInfo(int _serial)
    {
        this.serial = _serial;
        details.gameObject.SetActive(true);

        Company company = City.companyList[serial];

        companyName.text = company.nickName;
        property.text = company.fund.ToString();
        reputation.text = company.fame.ToString();
    }

    public void onPoachButtonClicked()
    {
        Company other = City.companyList[serial];
        Company current = City.currentCompany;


    }
}
