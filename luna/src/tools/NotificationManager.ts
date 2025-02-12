import {toast} from "react-toastify";

export default class NotificationManager{
    public static makeError(title: string){
        toast.error(title)
    }

    public static makeWarning(title: string){
        toast.warning(title)
    }

    public static makeSuccess(title: string){
        toast.success(title)
    }

    public static makeInfo(title: string){
        toast.info(title)
    }
}