import ProviderBase from "../providerBase";
import INotificationView from "../../models/notification/INotificationView";
import INotificationBlank from "../../models/notification/INotificationBlank";

export default class NotificationProvider extends ProviderBase {

    static async getNotifications(withRead: boolean): Promise<Array<INotificationView>> {

        let url = this.baseAddress + "/Notification/GetNotifications?withRead=" + withRead;

        return await this.get(url)
            .then(async res => {

                if (res.status === 200) {
                    return res.data;
                }

                return [];
            })
            .catch(() => {
                return [];
            });
    }

    static async getNotificationsByCreator(userId: string): Promise<Array<INotificationView>> {

        let url = this.baseAddress + "/Notification/GetNotificationsByCreator?createdUserId=" + userId;

        return await this.get(url)
            .then(async res => {

                if (res.status === 200) {
                    return res.data;
                }

                return [];
            })
            .catch(() => {
                return [];
            });
    }

    static async createNotification(notificationBlank: INotificationBlank) : Promise<boolean>{

        let url = this.baseAddress + "/Notification/CreateNotification";

        return await this.post(url, notificationBlank)
            .then(async res => {

                return res.status == 200;
            })
            .catch(() => {
                return false;
            });
    }

    static async updateNotification(id: string, notificationBlank: INotificationBlank) : Promise<boolean>{

        let url = this.baseAddress + "/Notification/UpdateNotification?id=" + id;

        return await this.put(url, notificationBlank)
            .then(async res => {

                return res.status == 200;
            })
            .catch(() => {
                return false;
            });
    }

    static async readNotification(id: string) : Promise<boolean>{

        let url = this.baseAddress + "/Notification/ReadNotification?id=" + id;

        return await this.post(url, null)
            .then(async res => {

                return res.status == 200;
            })
            .catch(() => {
                return false;
            });
    }

    static async readNotifications(ids: Array<string>) : Promise<boolean>{

        let url = this.baseAddress + "/Notification/ReadNotifications";

        return await this.post(url, ids)
            .then(async res => {

                return res.status == 200;
            })
            .catch(() => {
                return false;
            });
    }

    static async unreadNotifications(ids: Array<string>) : Promise<boolean>{

        let url = this.baseAddress + "/Notification/UnReadNotifications";

        return await this.post(url, ids)
            .then(async res => {

                return res.status == 200;
            })
            .catch(() => {
                return false;
            });
    }

    static async unreadNotification(id: string) : Promise<boolean>{

        let url = this.baseAddress + "/Notification/UnReadNotification?id=" + id;

        return await this.post(url, null)
            .then(async res => {

                return res.status == 200;
            })
            .catch(() => {
                return false;
            });
    }

    static async deleteNotification(id: string) : Promise<boolean>{

        let url = this.baseAddress + "/Notification/DeleteNotification?id=" + id;

        return await this.post(url, null)
            .then(async res => {

                return res.status == 200;
            })
            .catch(() => {
                return false;
            });
    }
}