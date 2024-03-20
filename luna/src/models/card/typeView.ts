import {Guid} from "guid-typescript";

export default class TypeView {
    constructor(id: Guid, name: string, color: string) {
        this.id = id;
        this.name = name;
        this.color = color;
    }

    public id: Guid;
    public name: string;
    public color: string;
}