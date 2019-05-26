using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FullScreenPanel : MonoBehaviour
{

    private CanvasGroup canvasGroup;
    
    // Start is called before the first frame update
    void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        Debug.Log("Full Screen Panel init Done");
        UIExit();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UIExit() {
        canvasGroup.alpha = 0;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
    }

    public void UIEntry(){
        canvasGroup.alpha = 1;
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
    }
}
