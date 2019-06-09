using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonGroup : MonoBehaviour
{
    public List<Button> buttons;

    public Color defaultColor;
    public Color selectedColor;

    private int selectedIndex = 0;

    public void setSelect(int index)
    {
        buttons[selectedIndex].GetComponent<Image>().color = defaultColor;
        selectedIndex = index;
        buttons[selectedIndex].GetComponent<Image>().color = selectedColor;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
