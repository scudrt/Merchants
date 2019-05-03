using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomMoveScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        ;
    }

    // Update is called once per frame
    void Update()
    {
        float x = Random.Range(-0.5f, 0.0f), y = Random.Range(-0.5f, 0.0f), z = Random.Range(-0.5f, 0.0f);
        this.transform.Rotate(x, y, z);
    }
}
