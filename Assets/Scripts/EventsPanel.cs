using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventsPanel : MonoBehaviour
{
    public GameObject eventsPrefab;
    public List<GameObject> eventsList;

    // Start is called before the first frame update
    void Start()
    {
        eventsPrefab = (GameObject)Resources.Load("Prefabs/Event");

        eventsList = new List<GameObject>(); 
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddEvent(GameObject evt)
    {
        eventsList.Add(evt);
    }

    public void DelEvent(GameObject evt)
    {
        eventsList.Remove(evt);
    }
}
