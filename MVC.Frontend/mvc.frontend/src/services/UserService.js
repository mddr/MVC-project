import AuthService from "./AuthService";

export class UserService {
    constructor() {
        this.Auth = new AuthService();
        this.getUserInfo = this.getUserInfo.bind(this)
    }

    getUserInfo() {
        return this.Auth.fetch(`${this.Auth.domain}/user/`, {
            method: 'get',
        });
    }

}
export default UserService;
