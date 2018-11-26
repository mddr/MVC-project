import AuthService from "./AuthService";

export class UserService {
    constructor() {
        this.Auth = new AuthService();
        this.add = this.add.bind(this)
        this.getCurrentUserInfo = this.getCurrentUserInfo.bind(this)
        this.orders = this.orders.bind(this)
        this.order = this.order.bind(this)
        this.delete = this.order.bind(this)

    }

    getUserInfo() {
        return this.Auth.fetch(`${this.Auth.domain}/user/`, {
            method: 'get',
        });
    }

    getUserOrders() {
        return this.Auth.fetch(`${this.Auth.domain}/user/`, {
            method: 'get',
        });
    }


}
export default UserService;
