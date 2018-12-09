import AuthService from "./AuthService";

export class ProductService {
  constructor() {
    this.Auth = new AuthService();
    this.getProduct = this.getProduct.bind(this);
		this.products = this.products.bind(this);
		this.getVisibleProducts = this.getVisibleProducts.bind(this);
		this.getTop = this.getTop.bind(this);
		this.getNewest = this.getNewest.bind(this);
		this.hideProduct = this.hideProduct.bind(this);
		this.showProduct = this.showProduct.bind(this);
  }

	getProduct(id) {
		console.log(2)
    return this.Auth.fetch(`${this.Auth.domain}/product/${id}`, null);
  }

  products() {
    return this.Auth.fetch(`${this.Auth.domain}/products`, null);
	}

	getVisibleProducts() {
		return this.Auth.fetch(`${this.Auth.domain}/products/visible`, null);
	}

  getTop(amount) {
    return this.Auth.fetch(`${this.Auth.domain}/products/top/${amount}`, {
      method: "get"
    });
  }

  getNewest(amount) {
    return this.Auth.fetch(`${this.Auth.domain}/products/newest/${amount}`, {
      method: "get"
    });
	}

	hideProduct(id) {
		return this.Auth.fetch(`${this.Auth.domain}/products/hide/${id}`, {
			method: "post"
		});
	}

	showProduct(id) {
		return this.Auth.fetch(`${this.Auth.domain}/products/hide/${id}`, {
			method: "post"
		});
	}
}
export default ProductService;
