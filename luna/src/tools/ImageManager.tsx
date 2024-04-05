export default class ImageManager {

    private static basicUrl = "http://localhost:7005/";

    public static getImageSrc(name: string) {
        return this.basicUrl + name;
    }
}