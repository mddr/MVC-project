import AuthService from "./AuthService";

export class UserService {
  constructor() {
      this.Auth = new AuthService();
      this.getUserInfo = this.getUserInfo.bind(this)
  }

  getUserInfo() {
      return this.Auth.fetch(`${this.Auth.domain}/user/`, {
          method: 'get',
      });
	}

	update(id, firstName, lastName, email, currency, emailConfirmed, prefersNetPrice, acceptsNewsletters, productsPerPage) {
		let body = "";
		const user = {
			id,
			firstName,
			lastName,
			email,
			currency,
			emailConfirmed,
			prefersNetPrice,
			acceptsNewsletters,
			productsPerPage
		};

		body = JSON.stringify(user);

		this.props.Auth.fetch(
			`${this.props.Auth.domain}/user/update`,
			{
				method: "post",
				body
			}
		).then(() => {
			this.props.updateData(user);
		});
	}

}
export default UserService;
