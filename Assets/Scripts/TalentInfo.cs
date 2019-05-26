using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalentInfo : MonoBehaviour
{
    public int serial;//talent's serial number

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DestroyTalentInfo()
    {
        Destroy(gameObject);
    }

    public void OnClick()
    {
        SendMessageUpwards("DisplayTalentInfo", serial);
    }
}
