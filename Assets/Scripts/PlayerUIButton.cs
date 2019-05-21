using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUIButton : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPlayerButtonClicked()
    {
        SendMessageUpwards("PlayerInfoPanelEntry");
    }

    public void OnTalentsManageClicked()
    {
        SendMessageUpwards("TalentsManagePanelEntry");
    }

    public void OnExitButton()
    {
        SendMessageUpwards("UIExit");
    }
}
