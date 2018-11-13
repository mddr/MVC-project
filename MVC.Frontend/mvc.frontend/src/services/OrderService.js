import AuthService from "./AuthService";

export class OrderService {
    constructor() {
        this.Auth = new AuthService();
        this.order = this.order.bind(this)

    }

    order() {
        return this.Auth.fetch(`${this.Auth.domain}/order/add`, {
            method: 'post',
        });
    }

}
export default OrderService;
