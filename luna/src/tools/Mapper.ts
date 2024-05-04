import IOption from "../models/tools/IOption";

export function mapToOption(data: any[]): IOption[]{
    return data.map(o => {return{label: o.name, value: o.id}});
}

export function mapToOptionUser(data: any[]): IOption[]{
    return data.map(o => {return{label: o.username, value: o.id}});
}