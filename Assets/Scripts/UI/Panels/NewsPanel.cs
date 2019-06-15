using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewsPanel : MonoBehaviour
{
    private Text content;
    private Text title;

    // Start is called before the first frame update
    void Start()
    {
        title = transform.Find("Title").GetComponent<Text>();
        content = transform.Find("Content").GetComponent<Text>();        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //complete this after news
    public void OnOpen(News news = null)
    {
        if (news != null)
        {
            title.text = news.title;
            content.text = news.content;
        }
    }
}
