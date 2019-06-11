public class Hospital: Building {
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

        _price = 60000f;
        buildingType = "Hospital";
        nickName = "医院" + buildingCount;
        if (blockBelong.companyBelong != null) {
            nickName = blockBelong.companyBelong.nickName + "的" + nickName;
        }
    }
    void Start() {
        this.onGenerate();
    }
}
