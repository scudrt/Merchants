using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class Event : MonoBehaviour
{
    public delegate void clickEvent(); //define delegate
    public clickEvent onClick;  //implement clickEvent

    public Vector3 targetPos; //position the item should be in

    private const int speed = 200; //speed when event info moving
    private string text;
    private Text eventText;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Event start");
        eventText = transform.Find("Text").GetComponent<Text>();
        eventText.text = text;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position != targetPos)
        {
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, targetPos, speed * Time.deltaTime);
        }
    }

    public void OnClick()
    {
        onClick();
        SendMessageUpwards("DelEvent", this.gameObject);
    }
    
    public void setText(string text)
    {
        this.text = text;
    }
}
