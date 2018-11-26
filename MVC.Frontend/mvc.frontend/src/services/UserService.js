import AuthService from "./AuthService";

export class UserService {
    constructor() {
        this.Auth = new AuthService();
        this.getUserInfo = this.getUserInfo.bind(this)
        this.getUserOrders = this.getUserOrders.bind(this)

    }

    getUserInfo() {
        return this.Auth.fetch(`${this.Auth.domain}/user/`, {
            method: 'get',
        });
    }

    getUserOrders() {
        return this.Auth.fetch(`${this.Auth.domain}/products/history`, {
            method: 'get',
        });
    }


}
export default UserService;
