using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class OptionPanel : MonoBehaviour
{
    public AudioSource audioSource;

    private Slider slider;
    // Start is called before the first frame update
    void Start()
    {
        slider = transform.Find("Slider").GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnSliderChanged()
    {
        audioSource.volume = slider.value;
    }

    public void OnExitButtonClicked()
    {
        SceneManager.LoadScene("GameScene");
        Time.timeScale = 0;
        SceneManager.LoadScene("MainMenu");
    }
}
