export default interface INotificationView{
    id: string;
    text: string;
    created: Date;
    createdUser: string;
    priority: number;
    imageUrl?: string;
    read: boolean;
    readAt?: Date;
}