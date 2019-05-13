using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopulationScript : MonoBehaviour
{
    //public data
    public static int totalAmount = 0;
    public static int[] amountAtAge = new int[101];

    //config data
    float populationRate = 0.01f;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i <= 100; ++i)
        {
            amountAtAge[i] = 0;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
