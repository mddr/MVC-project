import AuthService from "./AuthService";

export class ProductService {
    constructor() {
        this.Auth = new AuthService();
        this.getProduct = this.getProduct.bind(this)

        this.refreshTokenName = 'refresh_token'
    }

    getProduct(id) {
        return this.Auth.fetch(`${this.Auth.domain}/product/${id}`, null);
    }

    products() {
        return this.Auth.fetch(`${this.Auth.domain}/products`, null);
    }

    getTop(amount) {
        return this.Auth.fetch(`${this.Auth.domain}/products/top/${amount}`, {
            method: 'get',
        });
    }



}
export default ProductService;
