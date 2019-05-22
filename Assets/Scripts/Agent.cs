using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Agent : MonoBehaviour {
    // Start is called before the first frame update
    public static void enableAllRoomScripts() {
        //enable all scripts of room
        GameObject.FindObjectOfType<AIController>().enabled = true;
        GameObject.FindObjectOfType<Population>().enabled = true;
        GameObject.FindObjectOfType<Camera>().enabled = true;
        GameObject.FindObjectOfType<Timer>().enabled = true;
        GameObject.FindObjectOfType<City>().enabled = true;
        GameObject.FindObjectOfType<News>().enabled = true;
        GameObject.FindObjectOfType<Sun>().enabled = true;
    }
    private void Awake() {
        //room scripts don't act before game starts
        GameObject.FindObjectOfType<AIController>().enabled = false;
        GameObject.FindObjectOfType<Population>().enabled = false;
        GameObject.FindObjectOfType<Camera>().enabled = false;
        GameObject.FindObjectOfType<Timer>().enabled = false;
        GameObject.FindObjectOfType<City>().enabled = false;
        GameObject.FindObjectOfType<News>().enabled = false;
        GameObject.FindObjectOfType<Sun>().enabled = false;
    }
    void Start() {
    }
    // Update is called once per frame
    void Update() {

    }
}
