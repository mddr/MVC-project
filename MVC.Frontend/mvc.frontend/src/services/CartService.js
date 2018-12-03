import AuthService from "./AuthService";
import ProductService from "./ProductService";

export class CartService {
  constructor() {
    this.Auth = new AuthService();
    this.ProductService = new ProductService();
    this.getCart = this.getCart.bind(this)
		this.addItem = this.addItem.bind(this)

    this.refreshTokenName = 'refresh_token'
  }

  getCart() {
    return this.Auth.fetch(`${this.Auth.domain}/cart`, null);
  }

  addItem(id, amount) {
    let body = "";
    const obj = {
        productId: id,
        productAmount: amount,
    };

    body = JSON.stringify(obj);

    return this.Auth.fetch(`${this.Auth.domain}/cart/add`, {
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

    return this.Auth.fetch(`${this.Auth.domain}/cart/update`, {
        method: 'post',
        body
    });
  }

  removeCart() {
    return this.Auth.fetch(`${this.Auth.domain}/cart/delete`, {
        method: 'delete'
    });
  }

  removeItem(id) {
    let body = "";
    const obj = {
        productId: id,
    };

    body = JSON.stringify(obj);

    return this.Auth.fetch(`${this.Auth.domain}/cart/delete/${id}`, {
        method: 'delete',
        body
    });
  }

}
export default CartService;
