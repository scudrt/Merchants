using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager
{
    public static List<GameObject> eventsList = new List<GameObject>();
    public static GameObject eventInfo;
    public static EventsPanel panel;

    public static void addEvent(Event.EventFunc clickEvent, Event.EventFunc endEvent, string eventText, object msg = null)
    {
        GameObject evtObj = GameObject.Instantiate(eventInfo, panel.content);

        //set Event's listeners and text
        Event script = evtObj.GetComponent<Event>();
        script.setText(eventText);
        if (clickEvent != null) {
            script.addClickEvent(clickEvent);
        }
        if (endEvent != null) {
            script.addEndEvent(endEvent);
        }

        if (msg != null)
        {
            script.setMsg(msg);
        }

        eventsList.Insert(0, evtObj);

        //panel's effect
        panel.MoveEvents();
    }

    public static void removeEvent(GameObject evt)
    {
        int i = eventsList.IndexOf(evt);
        eventsList.Remove(evt);
        GameObject.Destroy(evt);

        //panel's effect
        panel.MoveEvents();
    }
}
