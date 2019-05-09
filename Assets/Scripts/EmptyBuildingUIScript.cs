using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class EmptyBuildingUIScript : MonoBehaviour
{

    public GameObject Building;//which building's information to display

    private Animator animator;//panel's animator

    private Transform panel;//

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        panel = transform.Find("Panel");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UIEntry()
    {
        //display the UI
        animator.SetBool("isDisplayed", true);

        //set the information
        panel.Find("LandPrice").GetComponent<Text>().text = Building.GetComponent<BuildingScript>().LandPrice.ToString();
    }

    //called when click exit button
    public void UIExit() 
    {
        animator.SetBool("isDisplayed", false);
    }

}
