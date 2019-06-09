using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPanel : MonoBehaviour
{
    private Animator animator;
    // Start is called before the first frame update
    void Start(){
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UIEntry()
    {
        if (animator.GetBool("isDisplayed") != true)
        {
            PlayerUI.addUI();
        }
        animator.SetBool("isDisplayed", true);
    }

    public void UIExit()
    {
        if(animator.GetBool("isDisplayed") != false)
        {
            PlayerUI.delUI();
        }
        animator.SetBool("isDisplayed", false);
    }
}
