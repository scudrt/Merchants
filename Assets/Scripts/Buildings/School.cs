public class School: Building {
    public new float price {
        get {
            return _price + budget;
        }
        private set { }
    }
    private float _price;
    private static int buildingCount = 0; //count of this type of building
    public override void onGenerate() {
        ++buildingCount;

        _price = 100000f;
        buildingType = "School";
        nickName = "学校" + buildingCount;
        if (blockBelong.companyBelong != null) {
            nickName = blockBelong.companyBelong.nickName + "的" + nickName;
        }
    }
    void Start() {
        onGenerate();
    }
}
