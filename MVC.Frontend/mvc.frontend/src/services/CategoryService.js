import AuthService from "./AuthService";

export class CategoryService {
	constructor() {
		this.Auth = new AuthService();
		this.getCategories = this.getCategories.bind(this)
	}

	getCategories() {
		return this.Auth.fetch(`${this.Auth.domain}/categories`, null);
	}

	getVisibleCategories() {
		return this.Auth.fetch(`${this.Auth.domain}/categories/visible`, null);
	}

	hideCategory(id) {
		return this.Auth.fetch(`${this.Auth.domain}/category/hide/${id}`, {
			method: "post"
		});
	}

	showCategory(id) {
		return this.Auth.fetch(`${this.Auth.domain}/category/hide/${id}`, {
			method: "post"
		});
	}

}
export default CategoryService;
