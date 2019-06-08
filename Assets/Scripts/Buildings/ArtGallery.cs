public class ArtGallery: Building {
    private static int buildingCount = 0; //count of this type of building
    public override void onGenerate() {
        ++buildingCount;

        price = 4000f;
        buildingType = "ArtGallery";
        nickName = blockBelong.companyBelong.nickName + "的美术馆" + buildingCount;
    }
    void Start() {
        onGenerate();
    }
}
