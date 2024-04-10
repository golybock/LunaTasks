import {Guid} from "guid-typescript";

export default class IBlockedCardView{
    constructor(comment: string, blockedUserId: Guid, startBlockTimestamp: string, endBlockTimestamp: string) {
        this.comment = comment;
        this.blockedUserId = blockedUserId;
        this.startBlockTimestamp = startBlockTimestamp;
        this.endBlockTimestamp = endBlockTimestamp;
    }

    public comment: string;
    public blockedUserId: Guid;
    public startBlockTimestamp: string;
    public endBlockTimestamp: string;
}