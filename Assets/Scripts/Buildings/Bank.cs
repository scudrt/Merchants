public class Bank : Building {
    public new static float PRICE = 80000f;
    private static int buildingCount = 0; //count of this type of building
    public override void onGenerate() {
        ++buildingCount;

        basicPrice = PRICE;
        buildingType = "Bank";
        nickName = "银行" + buildingCount;
        if (blockBelong.companyBelong != null) {
            nickName = blockBelong.companyBelong.nickName + "的" + nickName;
        }
    }
    // Start is called before the first frame update
    void Start() {
        onGenerate();
    }
}
