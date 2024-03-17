import {Guid} from "guid-typescript";

export default interface IUserView{
    id: Guid;
    username : string;
    email: string;
    phoneNumber: string;
    createdTimestamp: string;
    emailConfirmed: boolean;
    image: string;
}