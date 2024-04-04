import {Store} from "react-notifications-component";

export default class NotificationManager{
    public static makeError(title: string){
        Store.addNotification({title: title, type: "danger", insert: "top", container: "top-right", dismiss: {duration: 3000}})
    }

    public static makeWarning(title: string){
        Store.addNotification({title: title, type: "warning", insert: "top", container: "top-right", dismiss: {duration: 3000}})
    }

    public static makeSuccess(title: string){
        Store.addNotification({title: title, type: "success", insert: "top", container: "top-right", dismiss: {duration: 3000}})
    }

    public static makeInfo(title: string){
        Store.addNotification({title: title, type: "info", insert: "top", container: "top-right", dismiss: {duration: 3000}})
    }
}