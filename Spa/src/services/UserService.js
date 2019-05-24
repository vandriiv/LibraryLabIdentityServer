import { UserManagerSettings, UserManager } from 'oidc-client';
import { createUserManager } from 'redux-oidc';
export default class UserService {

    _authApiURI = "http://localhost:54990/api/";

    constructor(settings) {
        this.manager = new UserManager(settings);
    }

    login = async () => {
        return this.manager.signinRedirect();
    };

    logout = async () => {
        return this.manager.signoutRedirect();
    };

    postRequest = async (url, body) => {
        let headers = new Headers();
        headers.append('Content-Type', 'application/json');
        headers.append('Accept', 'application/json');

        const authHeader = localStorage.getItem("token")
            ? "Bearer " + localStorage.getItem("token")
            : "";

        if (authHeader !== "") {
            headers.append('Authorization', authHeader);
        }
        const res = await fetch(`${this._authApiURI}${url}`, {
            method: "POST",
            credentials: "same-origin",
            headers: headers,
            body: JSON.stringify(body)
        });
        if (!res.ok) {
            var errorMsg = await res.json();

            throw errorMsg;
        }

        return await res.json();
    };

    registration = async (userRegistrationData) => {
        const path = "account/";

        const result = await this.postRequest(path, userRegistrationData);

        return result;
    };

    userInfo = async () => {
        return this.manager.getUser();
    };

};