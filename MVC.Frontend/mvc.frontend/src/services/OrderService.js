import AuthService from "./AuthService";

export class OrderService {
    constructor() {
        this.Auth = new AuthService();
        this.add = this.order.bind(this)
        this.userOrders = this.order.bind(this)
        this.orders = this.orders.bind(this)
        this.order = this.order.bind(this)
        this.delete = this.order.bind(this)

    }

    add() {
        return this.Auth.fetch(`${this.Auth.domain}/order/add`, {
            method: 'post',
        });
    }

    userOrders(id) {
        return this.Auth.fetch(`${this.Auth.domain}/orders/${id}`, {
            method: 'get',
        });
    }

    orders() {
        return this.Auth.fetch(`${this.Auth.domain}/orders`, {
            method: 'get',
        });
    }

    order(id) {
        return this.Auth.fetch(`${this.Auth.domain}/order/${id}`, {
            method: 'get',
        });
    }

    delete(id) {
        return this.Auth.fetch(`${this.Auth.domain}/order/delete/${id}`, {
            method: 'delete',
        });
    }
}
export default OrderService;
