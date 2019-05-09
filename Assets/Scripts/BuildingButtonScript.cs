using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingButtonScript : MonoBehaviour
{

    public Canvas buildingUI;//reference to BuildingUI so that all buttons can access to it

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
        buildingUI.GetComponent<BuildingUIScript>().Building.GetComponent<BuildingScript>().status 
            = BuildingScript.Status.BOUGHT;

        SendMessageUpwards("UIExit");
    }
}
