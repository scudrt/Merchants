using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventsPanel : MonoBehaviour
{
    public Transform content { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        content = transform.Find("Content");
        EventManager.panel = this;
        EventManager.eventInfo = (GameObject)Resources.Load("Prefabs/Event");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MoveEvents()
    {
        //move all previous event to correct places
        Event eventScript;
        foreach (GameObject eventInfo in EventManager.eventsList)
        {
            eventScript = eventInfo.GetComponent<Event>();
            eventScript.targetPos.y = (EventManager.eventsList.IndexOf(eventInfo)) * 50;
        }

        content.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical
            , EventManager.eventsList.Count * 50);
    }
}
