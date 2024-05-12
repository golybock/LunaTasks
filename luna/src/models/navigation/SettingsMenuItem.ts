import SettingsSections from "../../tools/SettingsSections";

export default class SettingsMenuItem {

    constructor(title: string, href: SettingsSections, image: string | null) {
        this.title = title;
        this.href = href;
        this.image = image;
    }

    public title: string;
    public href: SettingsSections;
    public image: string | null;
}