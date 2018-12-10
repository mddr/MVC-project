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
		this.getFile = this.getFile.bind(this);
		this.addFile = this.addFile.bind(this);
		this.removeFile = this.removeFile.bind(this);
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

	getFile(productId, fileId) {
		return this.Auth.fetch(`${this.Auth.domain}/product/${productId}/file/${fileId}`, {
			method: "get"
		});
	}

	addFile(productId, filename, base64) {
		let body = "";
		const obj = {
			productId,
			filename,
			base64: `data:text/plain;base64,${base64}`,
		};

		body = JSON.stringify(obj);
		return this.Auth.fetch(`${this.Auth.domain}/product/${productId}/file/add`, {
			method: "post",
			body
		});
	}

	removeFile(productId, filename, base64) {
		let body = "";
		const obj = {
			productId,
			filename,
			base64: `data:text/plain;base64,${base64}`,
		};

		body = JSON.stringify(obj);
		return this.Auth.fetch(`${this.Auth.domain}/product/${productId}/file/delete/${fileId}`, {
			method: "DELETE",
			body
		});
	}

}
export default ProductService;
