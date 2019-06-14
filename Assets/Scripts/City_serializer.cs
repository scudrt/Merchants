using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

[System.Serializable]
public class City_serializer{
    //public List<Block> blockList { set; get; }
    //public List<Company> companyList { set; get; }
    //public List<Talent> talentList;


    public News newsMaker;
    //public Agent agent;
    //public Company currentCompany { get; set; }


    public void Serialize(City cityObj)
    {
        //this.blockList = City.blockList;
        //this.companyList = City.companyList;
        //this.talentList = City.talentList;
        this.newsMaker = cityObj.newsMaker;
        //this.currentCompany = City.currentCompany;
        

        FileStream fileStream = new FileStream("city.data", FileMode.Create);
        BinaryFormatter b = new BinaryFormatter();
        b.Serialize(fileStream, this);
        fileStream.Close();
        Debug.Log("序列化完成");
    }

    public City_serializer DeSerialize()
    {
        FileStream fileStream = new FileStream("city.data", FileMode.Open);
        BinaryFormatter b = new BinaryFormatter();
        City_serializer c = new City_serializer();
        c = b.Deserialize(fileStream) as City_serializer;
        fileStream.Close();
        Debug.Log("反序列化完成");
        return c;
    }
}