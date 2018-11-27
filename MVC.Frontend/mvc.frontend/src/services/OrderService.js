import AuthService from "./AuthService";

export class OrderService {
    constructor() {
        this.Auth = new AuthService();
        this.add = this.add.bind(this)
        this.orders = this.orders.bind(this)
        this.order = this.order.bind(this)
        this.delete = this.order.bind(this)
        this.getUserOrders = this.getUserOrders.bind(this)
    }

    add() {
        return this.Auth.fetch(`${this.Auth.domain}/order/add`, {
            method: 'post',
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

    getUserOrders() {
        return this.Auth.fetch(`${this.Auth.domain}/orders/history`, {
            method: 'get',
        });
    }
}
export default OrderService;
