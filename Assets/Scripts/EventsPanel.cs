using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventsPanel : MonoBehaviour
{
    public GameObject eventsPrefab;
    public List<GameObject> eventsList;
    private Transform content;

    // Start is called before the first frame update
    void Start()
    {
        eventsPrefab = (GameObject)Resources.Load("Prefabs/Event");

        content = transform.Find("Content");
        eventsList = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddEvent(Event.clickEvent clickEvent, string eventText)
    {
        //move all previous event upward
        Event eventScript;
        foreach (GameObject eventInfo in eventsList)
        {
            eventScript = eventInfo.GetComponent<Event>();
            eventScript.targetPos.y = (eventsList.IndexOf(eventInfo) + 1) * 50;
        }

        GameObject evt = GameObject.Instantiate(eventsPrefab, content);
        evt.GetComponent<Event>().onClick = clickEvent;
        evt.GetComponent<Event>().setText(eventText);

        eventsList.Insert(0, evt);//add evt to the first

        content.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical
            ,eventsList.Count * 50);
    }

    public void DelEvent(GameObject evt)
    {
        int i = eventsList.IndexOf(evt);
        eventsList.Remove(evt);
        Destroy(evt);

        //move events
        for (; i < eventsList.Count; i++)
        {
            eventsList[i].GetComponent<Event>().targetPos.y = i * 50;
        }

        content.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical,eventsList.Count * 50);
    }
}
