using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemInfo : MonoBehaviour
{
    public int serial;//talent's serial number
    public bool isChosen;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void DestroyItemInfo()
    {
        Destroy(gameObject);
    }

    public void OnClick()
    {
        SendMessageUpwards("DisplayItemInfo", serial);
    }

    public void OnClickAndSetObject()
    {
        SendMessageUpwards("SetObjectBySerial", serial);
    }
}
