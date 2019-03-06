import AuthService from "./AuthService";

export class UserService {
  constructor() {
      this.Auth = new AuthService();
    this.getUserInfo = this.getUserInfo.bind(this);
    this.update = this.update.bind(this);
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

    return this.Auth.fetch(
      `${this.Auth.domain}/user/update`,
      {
        method: "post",
        body
      }
    );
	}

}
export default UserService;
