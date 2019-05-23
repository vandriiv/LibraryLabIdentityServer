import { UserManagerSettings,UserManager } from 'oidc-client';
import { createUserManager } from 'redux-oidc';
export default class UserService {   
    constructor(settings){
        this.manager = new UserManager(settings);
    }

    login=async ()=>{
        return this.manager.signinRedirect();
    };

    logout=async()=>{
        return this.manager.signoutRedirect();
    };

    registration=async()=>{
        console.log("not implemented yet");
    };

    userInfo=async()=>{
       return this.manager.getUser();
    };

};