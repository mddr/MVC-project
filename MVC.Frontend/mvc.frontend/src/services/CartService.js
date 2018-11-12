import AuthService from "./AuthService";
import ProductService from "./ProductService";

export class CartService {
    constructor() {
        this.Auth = new AuthService();
        this.ProductService = new ProductService();
        this.getCart = this.getCart.bind(this)
        this.getCartt = this.getCartt.bind(this)

        this.refreshTokenName = 'refresh_token'
    }

    getCart() {
        return this.Auth.fetch(`${this.Auth.domain}/cart`, null);
    }

    getCartt() {
        return this.Auth.fetch(`${this.Auth.domain}/cart`, null).then(res => res.json());
    }

    addItem(id, amount) {
        let body = "";
        const obj = {
            productId: id,
            productAmount: amount,
        };

        body = JSON.stringify(obj);

        this.Auth.fetch(`${this.Auth.domain}/cart/add`, {
            method: 'post',
            body
        });
    }


    updateItem(id, amount) {
        let body = "";
        const obj = {
            productId: id,
            productAmount: amount,
        };

        body = JSON.stringify(obj);

        this.Auth.fetch(`${this.Auth.domain}/cart/update`, {
            method: 'post',
            body
        });
    }

    removeCart() {
        this.Auth.fetch(`${this.Auth.domain}/cart/delete`, {
            method: 'delete'
        });
    }

    removeItem(id) {
        let body = "";
        const obj = {
            productId: id,
        };

        body = JSON.stringify(obj);

        this.Auth.fetch(`${this.Auth.domain}/cart/delete/${id}`, {
            method: 'delete',
            body
        });
    }

    getTotalPrice(Items) {
        let output = 0;
        if (Items.length < 1) return 0;
        for (let i = 0; i < Items.length; i++) {
            this.ProductService.getProduct(Items[i].productId).then(res => res.json()).then(res => {
                output +=
                    Math.round(Items[i].productAmount * res.price * 100) / 100;
            });
        }
        return output;
    }

}
export default CartService;
