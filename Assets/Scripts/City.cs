using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class City : MonoBehaviour
{
    /**********data area**********/
    public static List<Block> blockList;
    public static List<Company> companyList;
    public static List<Talent> talentList;
    
    public News newsMaker;
    private Agent agent;

    //building type list
    public static string[] buildingTypes =
        { "ArtGallery", "Bank", "Cinema",
        "Hospital", "Restaurant", "Scenic",
        "School", "Stadium", "SuperMarket"};

    private const int NATURAL_BUILDING_COUNT = 8;
    public static int numOfCompany = 3;
    public static int BLOCK_NUMBER = 64; // it must be a square of integer

    private float talentRefreshTime;

    public static Company currentCompany { get; set; }
    /**********data area**********/

    public static void generateTalentsMarket(){
        talentList.Clear();
        int num = Random.Range(10, 15);
        for(int i = 0; i < num; i++){
            talentList.Add(Talent.generateTalent());
        }
    }
    public static float generateNormalDistribution(float expectation, float radius) {
        //it is not a normal distribution in fact:)
        float ret = Random.Range(expectation - radius, expectation);
        return ret + Random.Range(0, radius);
    }

    private void makeBlocks() {
        GameObject prefabBlock = (GameObject)Resources.Load("Prefabs/brickBlock");
        blockList = new List<Block>();

        int n = (int)Mathf.Sqrt((float)BLOCK_NUMBER);
        float mapSize = gameObject.GetComponent<Collider>().bounds.size.x;
        float blockSize = mapSize / n;
        float blockScale = blockSize / mapSize;
        int blockTick = 0;
        for (int i = 0; i < n; ++i) {
            for (int j = 0; j < n; ++j) {
                //generate blocks using prefab
                GameObject temp = Object.Instantiate(prefabBlock,
                    new Vector3(blockSize * (i + 0.5f) - 0.5f * mapSize, 0.01f, blockSize * (j + 0.5f) - 0.5f * mapSize),
                    new Quaternion());
                temp.transform.localScale = new Vector3(blockScale, blockScale, blockScale);
                temp.transform.parent = this.transform;
                //add Block script
                Block newBlock = temp.AddComponent<Block>();
                newBlock.Pos_x = i;
                newBlock.Pos_y = j;
                newBlock.id = blockTick++;
                blockList.Add(newBlock);
            }
        }
    }

    public static Company generateNewCompany() {
        Company temp =  GameObject.FindObjectOfType<City>().gameObject.AddComponent<Company>();
        //distribute random color to every company
        float r = Random.Range(0f, 1f), g = Random.Range(0f, 1f), b = Random.Range(0f, 1f);
        temp.companyColor = new Color(r, g, b);
        companyList.Add(temp);
        return temp;
    }
    private void makeCompanies() {
        companyList = new List<Company>();
        for (int i = 0; i < numOfCompany; ++i) {
            generateNewCompany();
        }
        StartCoroutine(waitForPlayerDistribution());
    }

    private IEnumerator waitForPlayerDistribution() {
        yield return new WaitUntil(()=> { return agent.initDone; });
        currentCompany = agent.distributePlayer();
    }

    private IEnumerator generateNaturalBuildings() {
        //wait for city generating all blocks
        yield return new WaitUntil(()=>{ return City.BLOCK_NUMBER == City.blockList.Count; });

        //randomly generate buildings in city
        int index;
        for (int k = 1; k <= NATURAL_BUILDING_COUNT; ++k) {
            do {
                index = Random.Range(0, BLOCK_NUMBER - 1);
            } while (blockList[index].isEmpty == false);
            string type = buildingTypes[Random.Range(0, 8)];
            blockList[index].build(type);
        }
    }

    void Start() {
        agent = GameObject.FindObjectOfType<Agent>();

        talentRefreshTime = 120;

        makeBlocks();
        makeCompanies();
        StartCoroutine(generateNaturalBuildings());
        talentList = new List<Talent>();
        generateTalentsMarket();

        Debug.Log("City init done");
    }

    // Update is called once per frame
    void Update()
    {
        talentRefreshTime -= Time.deltaTime;
        if (talentRefreshTime <= 0)
        {
            generateTalentsMarket();
            talentRefreshTime = 120;
        }
    }
}
