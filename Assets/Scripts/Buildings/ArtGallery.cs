public class ArtGallery: Building {
    public override void onGenerate() {
        this.price = 4000f;
        this.buildingType = "ArtGallery";
    }
    void Start() {
        onGenerate();
    }
}
