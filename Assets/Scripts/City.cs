using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class City : MonoBehaviour
{
    /**********data area**********/
    public static List<Block> blockList;
    public static List<Company> companyList;

    private const int BLOCK_NUMBER = 0;
    public static int numOfPlayers = 2;

    public static Company currentCompany { get; set; }
    /**********data area**********/

    private void makeBlocks() {
        blockList = new List<Block>();
        for (int i = 0; i < BLOCK_NUMBER; ++i) {
                //generate empty blocks
            Block temp = gameObject.AddComponent<Block>();
            blockList.Add(temp);
            //ATTENTION: remember to set position for each block
            ;
        }
    }

    private void makeCompanies() {
        companyList = new List<Company>();
        for (int i = 0; i < numOfPlayers; ++i) {
            Company temp = gameObject.AddComponent<Company>();
            temp.id = i;
            companyList.Add(temp);
        }
        currentCompany = companyList[0]; //zero is the host of game
    }
    
    void Start() {
        makeBlocks();
        makeCompanies();
        Debug.Log("City init done");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
