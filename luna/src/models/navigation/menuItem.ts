export default class MenuItem {

    constructor(title: string, href: string, image: string | null) {
        this.title = title;
        this.href = href;
        this.image = image;
    }

    public title: string;
    public href: string;
    public image: string | null;
}