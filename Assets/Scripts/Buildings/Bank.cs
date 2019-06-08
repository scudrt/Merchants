public class Bank : Building {
    private static int buildingCount = 0; //count of this type of building
    public override void onGenerate() {
        ++buildingCount;

        price = 12000f;
        buildingType = "Bank";
        nickName = blockBelong.companyBelong.nickName + "的银行" + buildingCount;
    }
    // Start is called before the first frame update
    void Start() {
        onGenerate();
    }
}
