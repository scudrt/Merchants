using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    public Company company;

    private GameObject playerInfoPanel;
    private GameObject bottomPanel;
    private GameObject talentsManagePanel;
    private GameObject blocksManagePanel;
    private GameObject talentsMarketPanel;
    private GameObject companiesPanel;


    /******get all data text on bottom panel*******/
    private Text property;
    private Text reputation;
    private Text GDP;
    private Text population;
    private Text trend;

    // Start is called before the first frame update
    void Start()
    {
        company = City.currentCompany;

        bottomPanel = transform.Find("BottomPanel").gameObject;
        talentsManagePanel = transform.Find("TalentsManagePanel").gameObject;
        blocksManagePanel = transform.Find("BlocksManagePanel").gameObject;
        talentsMarketPanel = transform.Find("TalentsMarketPanel").gameObject;
        companiesPanel = transform.Find("CompaniesPanel").gameObject;

        property = bottomPanel.transform.Find("Property").GetComponent<Text>();
        reputation = bottomPanel.transform.Find("Reputation").GetComponent<Text>();
        GDP = bottomPanel.transform.Find("GDP").GetComponent<Text>();
        population = bottomPanel.transform.Find("Population").GetComponent<Text>();
        trend = bottomPanel.transform.Find("Trend").GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if (City.currentCompany == null) {
            return;
        }
        property.text = City.currentCompany.fund.ToString();
        reputation.text = City.currentCompany.fame.ToString();
        population.text = Population.amount.ToString();
    }

    public void PlayerInfoPanelEntry()
    {
        playerInfoPanel.SetActive(true);
    }

    public void PlayerInfoPanelExit()
    {
        playerInfoPanel.SetActive(false);
    }

    public void TalentsManagePanelEntry()
    {
        talentsManagePanel.GetComponent<FullScreenPanel>().UIEntry();
        talentsManagePanel.GetComponent<TalentManageUI>().OnOpen();
    }

    public void BlocksManagePanelEntry()
    {
        blocksManagePanel.GetComponent<FullScreenPanel>().UIEntry();
        blocksManagePanel.GetComponent<BuildingManagement>().OnOpen();
    }

    public void TalentsManagePanelExit()
    {
        talentsManagePanel.SetActive(false);
    }

    public void TalentsMarketPanelEntry()
    {
        talentsMarketPanel.GetComponent<FullScreenPanel>().UIEntry();
        talentsMarketPanel.GetComponent<TalentsMarket>().OnOpen();
    }

    public void CompaniesPanelEntry()
    {
        companiesPanel.GetComponent<FullScreenPanel>().UIEntry();
        companiesPanel.GetComponent<CompaniesUI>().OnOpen();
    }
}
