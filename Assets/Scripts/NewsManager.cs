using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Data;
using System.IO;
using System.Xml;
using System.Linq;
using UnityEngine.UI;

public class NewsManager : MonoBehaviour {
    public int seed { get; set; }

    public List<News> newsList;

    private GameObject newsPanel; //newspanel will display news
    private string newsFileName;

    private float intervalTime; //shortest time between two news

    private void Awake() {
        seed = (int)Time.time;
        
    }
    void Start() {
        newsPanel = GameObject.Find("NewsPanel").gameObject;

        Random.InitState(seed);
        newsList = new List<News>();
        newsFileName = Application.dataPath + "/Resources/news.xml";
        loadNewsFile();
        intervalTime = 10;
    }
    void Update() {
        intervalTime -= Time.deltaTime;
        if (intervalTime < 0)
        {
            createNews(Random.Range(0, newsList.Count));
            intervalTime = 10;
        }
    }
    private void sendNewsEvent(News news){
        EventManager.addEvent(delegate(Event evt)
        {
            newsPanel.transform.Find("Title").GetComponent<Text>().text = news.title;
            newsPanel.transform.Find("Content").GetComponent<Text>().text = news.content;
            newsPanel.SendMessage("UIEntry");
        },null,news.title);
    }

    private void createNews(int randint)
    {
        Debug.Log(intervalTime);
        News news = newsList[randint];
        news.newsEventHappen();
        sendNewsEvent(news);
    }

    private bool loadNewsFile() {
        //return true if load successfully
        //news file contains the news ecents and their influence
        //

        string title;
        string content;
        News.FindObjects findObjects;
        News.NewsEffect newsEffect;
        News news;
        XmlNode effects;

        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.Load(newsFileName);
        XmlNode root = xmlDoc.SelectSingleNode("newsinfo");

        //read all news in file
        foreach (XmlNode newsEle in root.SelectNodes("news"))
        {
            title = newsEle.SelectSingleNode("title").InnerText;
            content = newsEle.SelectSingleNode("content").InnerText;

            //create a news
            news = new News(title, content);

            /**********set news effect**********/
            effects = newsEle.SelectSingleNode("effects");
            foreach(XmlElement effect in effects.SelectNodes("effect"))
            {

                // delegate to find all buildings of a type
                findObjects = delegate ()
                {
                    List<object> objs = new List<object>();
                    foreach (Company company in City.companyList)
                    {
                        foreach (Block block in company.blockList)
                        {
                            if (block.building.buildingType == effect.GetAttribute("object"))
                            {
                                objs.Add(block.building);
                            }
                        }
                    }
                    return objs;
                };

                // delegate to generate effects to objects
                newsEffect = delegate (List<object> objs)
                {
                    double value = System.Convert.ToDouble(effect.GetAttribute("value"));//value changed

                    Building building;
                    foreach (object obj in objs)
                    {
                        building = (Building)obj;
                        switch (effect.GetAttribute("attribute"))
                        {
                            case "attrackRate": building.attrackRate += (float) value; break;
                                //add some other attribute here
                            default: break;
                        }
                    }
                };

                news.addNewsEvent(findObjects, newsEffect);
            }
            /**********set news effect**********/

            //add news to news list
            newsList.Add(news);
        }

        return true;
    }
}
