import AuthService from "./AuthService";

export class AdminService {
	constructor() {
		this.Auth = new AuthService();
		this.sendNewsletters = this.sendNewsletters.bind(this)
	}

	sendNewsletters() {
		return this.Auth.fetch(`${this.Auth.domain}/users/newsletter`, {
			method: 'post',
		});
	}

}
export default AdminService;
