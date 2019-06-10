using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewsPanel : MonoBehaviour
{
    private Text content;

    // Start is called before the first frame update
    void Start()
    {
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
            
        }
    }
}
