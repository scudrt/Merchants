using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FullScreenPanel : MonoBehaviour
{

    private Animator animator;
    
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UIExit()
    {
        gameObject.SetActive(false);
    }

    public void UIEntry()
    {
        gameObject.SetActive(true);
    }
}
