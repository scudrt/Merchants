using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUI : MonoBehaviour
{
    public Company company;

    private GameObject playerInfoPanel;
    private GameObject bottomPanel;
    private GameObject talentsManagePanel;

    // Start is called before the first frame update
    void Start()
    {
        playerInfoPanel = transform.Find("PlayerInfoPanel").gameObject;
        bottomPanel = transform.Find("BottomPanel").gameObject;
        talentsManagePanel = transform.Find("TalentsManagePanel").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
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
        talentsManagePanel.SetActive(true);
    }

    public void TalentsManagePanelExit()
    {
        talentsManagePanel.SetActive(false);
    }
}
