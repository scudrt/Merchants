using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class Event : MonoBehaviour
{
    public delegate void clickEvent(); //define delegate
    public clickEvent onClick;  //implement clickEvent

    private Animator animator;
    private Text eventText;

    // Start is called before the first frame update
    void Start()
    {
        eventText = transform.Find("Text").GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClick()
    {
        onClick();
        SendMessageUpwards("DelEvent", this.gameObject);
    }
    
    public void setText(string text)
    {
        eventText.text = text;
    }
}
