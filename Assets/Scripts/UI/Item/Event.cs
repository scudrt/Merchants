using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class Event : MonoBehaviour
{
    public delegate void EventFunc(Event evt); //define delegate
    public EventFunc onClick;  //implement clickEvent
    public EventFunc onEnd;

    public object msg; //message object transferred

    public Vector3 targetPos; //position the item should be in

    private const int speed = 200; //speed when event info moving
    private string text;
    private Text eventText;

    private float existTime;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Event start");
        eventText = transform.Find("Text").GetComponent<Text>();
        eventText.text = text;

        existTime = 15f;
    }

    // Update is called once per frame
    void Update(){
        existTime -= Time.deltaTime;
        if (existTime <= 0){
            OnEnd();
        }

        if (transform.position != targetPos){
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, targetPos, speed * Time.deltaTime);
        }
    }

    public void addClickEvent(Event.EventFunc clickEvent)
    {
        this.onClick = clickEvent;
    }

    public void addEndEvent(Event.EventFunc endEvent)
    {
        this.onEnd = endEvent;
    }

    public void OnClick()
    {
        if (onClick != null) {
            onClick(this);
        }
        EventManager.removeEvent(this.gameObject);
    }

    public void OnEnd()
    {
        if (onEnd != null) {
            onEnd(this);
        }
        EventManager.removeEvent(this.gameObject);
    }
    
    public void setText(string text)
    {
        this.text = text;
    }

    public void setMsg(object msg)
    {
        this.msg = msg;
    }
}
