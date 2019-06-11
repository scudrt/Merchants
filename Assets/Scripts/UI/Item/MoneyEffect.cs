using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoneyEffect : MonoBehaviour
{
    public static GameObject moneyEffectPrefab;

    private float speed = 100f;
    private Transform tr;
    private float exitTime;

    public static void CreateMoneyEffect(Transform parent, string text) {
        if (moneyEffectPrefab == null) {
            moneyEffectPrefab = (GameObject)Resources.Load("Prefabs/MoneyEffect");
        }
        GameObject newEffect = GameObject.Instantiate(moneyEffectPrefab, parent);
        newEffect.GetComponent<Text>().text = text;
    }
    public static void CreateMoneyEffect(Transform parent, string text, Color color) {
        if (moneyEffectPrefab == null) {
            moneyEffectPrefab = (GameObject)Resources.Load("Prefabs/MoneyEffect");
        }
        GameObject newEffect = GameObject.Instantiate(moneyEffectPrefab, parent);
        newEffect.GetComponent<Text>().text = text;
        newEffect.GetComponent<Text>().color = color;
    }

    private void Awake()
    {

    }

    // Start is called before the first frame update
    void Start()
    {
        tr = GetComponent<Transform>();
        exitTime = 1.2f;
    }

    // Update is called once per frame
    void Update()
    {
        exitTime -= Time.deltaTime;
        if (exitTime <= 0)
            GameObject.Destroy(this.gameObject);
        tr.Translate(Vector3.up * speed * Time.deltaTime);
        if (speed > 0)
        {
            speed -= 5;
        }
    }
}
