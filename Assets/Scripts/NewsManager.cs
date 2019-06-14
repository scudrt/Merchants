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
                /*******************define all find objects function here************************/
                string obj = effect.GetAttribute("object");
                string range = effect.GetAttribute("range");

                if (obj == "Company" && range == "all")
                {
                    findObjects = delegate ()
                    {
                        List<object> objs = new List<object>();
                        object temp;
                        foreach (Company company in City.companyList)
                        {
                            temp = company;
                            objs.Add(temp);
                        }
                        return objs;
                    };
                }
                else if (obj == "Company" && range == "random")
                {
                    findObjects = delegate ()
                    {
                        List<object> objs = new List<object>();
                        object temp = City.companyList[Random.Range(0, City.companyList.Count)];
                        objs.Add(temp);
                        return objs;
                    };
                }
                else if (obj == "City") //population is static...
                {
                    findObjects = delegate ()
                    {
                        return null;
                    };
                }
                else if (obj == "Talent" && range == "all")
                {
                    findObjects = delegate ()
                    {
                        List<object> objs = new List<object>();
                        foreach (Company company in City.companyList)
                        {
                            foreach (Talent talent in company.talentList)
                            {
                                objs.Add(talent);
                            }
                        }
                        return objs;
                    };
                }
                else
                {
                    // delegate to find all buildings of a type
                    findObjects = delegate ()
                    {
                        List<object> objs = new List<object>();
                        foreach (Company company in City.companyList)
                        {
                            foreach (Block block in company.blockList)
                            {
                                if (!block.isEmpty && block.building.buildingType == effect.GetAttribute("object"))
                                {
                                    objs.Add(block.building);
                                }
                            }
                        }
                        return objs;
                    };
                }

                /**********************define all effects function here************************/
                // delegate to generate effects to objects
                if (obj == "City")
                {
                    newsEffect = delegate (List<object> objs)
                    {
                        double value = System.Convert.ToDouble(effect.GetAttribute("value"));

                        switch (effect.GetAttribute("attribute"))
                        {
                            case "population":
                                if (effect.HasAttribute("is_rate_value"))
                                    Population.amount = (int)(Population.amount * (1 + value));
                                else
                                    Population.amount += (int)value;
                                break;
                            //add other options
                            default: break;
                        }
                    };
                }
                else if (obj == "Talent")
                {
                    newsEffect = delegate (List<object> objs)
                    {
                        double value = System.Convert.ToDouble(effect.GetAttribute("value"));

                        Talent talent;
                        foreach (object temp in objs)
                        {
                            talent = (Talent)temp;
                            switch (effect.GetAttribute("attribute"))
                            {
                                case "satisfication":
                                    if (effect.HasAttribute("is_rate_value"))
                                        talent.satisfaction = (int)(talent.satisfaction * (1 + value));
                                    else
                                        talent.satisfaction += (int)value;
                                    break;
                                //add other options
                                default: break;
                            }
                        }
                    };
                }
                else if (obj == "Company")
                {
                    newsEffect = delegate (List<object> objs)
                    {
                        double value = System.Convert.ToDouble(effect.GetAttribute("value"));

                        Company company;
                        foreach (object temp in objs)
                        {
                            company = (Company)temp;
                            switch (effect.GetAttribute("attribute"))
                            {
                                case "fund": company.fund -= (float)value; break;
                                //add other options
                                default: break;
                            }
                        }
                    };
                }
                else
                {
                    newsEffect = delegate (List<object> objs)
                    {
                        double value = System.Convert.ToDouble(effect.GetAttribute("value"));//value changed

                    Building building;
                        foreach (object temp in objs)
                        {
                            building = (Building)temp;
                            switch (effect.GetAttribute("attribute"))
                            {
                                case "attrackRate": building.attrackRate += (float)value; break;
                            //add some other attribute here
                            default: break;
                            }
                        }
                    };
                }
                news.addNewsEvent(findObjects, newsEffect);
            }
            /**********set news effect**********/

            //add news to news list
            newsList.Add(news);
        }

        return true;
    }
}
