using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Talent : MonoBehaviour{
    private static int talentTick = 0; //record the talents have ever generated
    public static Talent generateTalent() {
        //return a randomly generated talent
        //generate name
        Talent _talent = GameObject.FindObjectOfType<City>().gameObject.AddComponent<Talent>();

        int lenOfName = Random.Range(3, 6);
        string _name = "" + (char)Random.Range(65, 90); //'A' - 'Z'
        for (int i = 1; i < lenOfName; ++i) {
            _name += (char)Random.Range(97, 122); //'a' - 'z'
        }
        _talent.name = _name;
        //generate talent's capacity
        float _charm = Mathf.Floor(City.generateNormalDistribution(45f, 40f)),
            _capacity = Mathf.Floor(City.generateNormalDistribution(45f, 40f));
        _talent.capacity = _capacity;
        _talent.charm = _charm;

        _talent.satisfaction = 50f;
        _talent.expectedSalary = Mathf.Floor(1.5f * _charm + 1.5f * _capacity +
            City.generateNormalDistribution(20, 80));
        _talent.salary = _talent.expectedSalary;
        //distribute id for every talent
        _talent.id = talentTick++;
        return _talent;
    }


    public int id = 0;
    public new string name { get; set; }
    public bool isHired { get {
            return companyBelong != null;
        }private set { } }
    public Building workPlace = null;
    public Company companyBelong = null;
    
    private int currentDay = Timer.day;

    private float _satisfaction;
    public float satisfaction{
        get{
            return _satisfaction;
        }
        set{
            if (value > 100f)
                value = 100f;
            if (value < 0)
                value = 0;
            _satisfaction = value;
        }
    }
    public float expectedSalary;
    
    private float _salary;
    public float salary
    {
        get{
            return _salary;
        }
        set{
            if (value < 0)
                value = 0;
            //salary influence employee's satisfation
            //remember to sent event
            //********************TO BE DONE********************
            _salary = value;
        }
    }

    private float _capacity;//capacity will increase speed of serving a customer
    public float capacity{
        get{
            return _capacity;
        }
        set{
            if (value > 100f)
                value = 100f;
            if (value < 0)
                value = 0;
            _capacity = value;
        }
    }

    private float _charm;//charm will increase Block's reputation
    public float charm {
        get{
            return _charm;
        }
        set{
            if (value > 100f)
                value = 100f;
            if (value < 0)
                value = 0;
            _charm = value;
        }
    }

    private void leaveCompany() {
        if (companyBelong == null) {
            return;
        }
        //send event
        EventManager.addEvent(null, null,
            name + "因工资被拖欠而辞职");
        //leave company
        companyBelong.fireTalent(this);
        GameObject.DestroyImmediate(this);
    }
    private void getPaid() {
        //get salary from its company
        if (companyBelong != null) {
            if (companyBelong.costMoney(_salary) == false) { //no enough money
                //lose satisfaction
                _satisfaction -= 10;
                //send message to company's player
                EventManager.addEvent(null, null,
                    name + ":你拖欠我的工资!(满意度下降)");
            }
        }
    }
    void Start() {
        ;
    }

    void Update() {
        if (currentDay != Timer.day) {
            currentDay = Timer.day;

            getPaid();

            if (_satisfaction <= 0) {
                leaveCompany();
            }
        }
    }
}
