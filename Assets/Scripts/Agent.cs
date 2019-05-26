using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Agent : MonoBehaviour {
    // Start is called before the first frame update
    public static void enableAllRoomScripts() {
        //enable all scripts of game room
        GameObject.FindObjectOfType<AIController>().enabled = true;
        GameObject.FindObjectOfType<Population>().enabled = true;
        GameObject.FindObjectOfType<Camera>().enabled = true;
        GameObject.FindObjectOfType<Timer>().enabled = true;
        GameObject.FindObjectOfType<City>().enabled = true;
        GameObject.FindObjectOfType<News>().enabled = true;
        GameObject.FindObjectOfType<Sun>().enabled = true;
        GameObject.FindObjectOfType<TalentManageUI>().gameObject.SetActive(true);
    }
    void Start() {
    }
    // Update is called once per frame
    void Update() {

    }
}
