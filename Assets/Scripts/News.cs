using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class News
{
    public delegate List<object> FindObjects(); //function to find affected objects
    public delegate void NewsEffect(List<object> objs); //function to generate effects
    public struct NewsEvent
    {
        public FindObjects findObjects;
        public NewsEffect newsEffect;
    }
    public List<NewsEvent> newsEvents = new List<NewsEvent>();
    public string title;
    public string content;

    public News(string title, string content)
    {
        this.title = title;
        this.content = content;   
    }

    public void addNewsEvent(FindObjects findObjects, NewsEffect newsEffect)
    {
        NewsEvent newsEvent = new NewsEvent();
        newsEvent.findObjects = findObjects;
        newsEvent.newsEffect = newsEffect;
        newsEvents.Add(newsEvent);
    }

    public void newsEventHappen()
    {
        List<object> objs;
        foreach(NewsEvent newsEvent in newsEvents)
        {
            objs = newsEvent.findObjects();
            newsEvent.newsEffect(objs);
        }
    }
}
