import AuthService from "./AuthService";

export class CategoryService {
	constructor() {
		this.Auth = new AuthService();
		this.getCategories = this.getCategories.bind(this)
		this.getVisibleCategories = this.getVisibleCategories.bind(this)
		this.hideCategory = this.hideCategory.bind(this)
		this.showCategory = this.showCategory.bind(this)
		this.getPdfSummary = this.getPdfSummary.bind(this)
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
		return this.Auth.fetch(`${this.Auth.domain}/category/show/${id}`, {
			method: "post"
		});
	}

	getPdfSummary(id) {
		return this.Auth.fetch(`${this.Auth.domain}/category/${id}/summary`, {
			method: "get"
		});
	}

}
export default CategoryService;
