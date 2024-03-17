import axios from "axios";

export default class ProviderBase {
    // todo add to config
    static baseAddress = "http://localhost:7000/api";

    constructor() {
        axios.defaults.withCredentials = true
    }

    // go to main page to show login window
    navigateToHome() {
        window.location.replace("https://localhost:3000")
    }
}