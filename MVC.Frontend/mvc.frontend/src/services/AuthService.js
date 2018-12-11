import decode from 'jwt-decode';

export class AuthService {
    constructor(domain) {
        this.domain = domain || 'https://localhost:44364'
        this.fetch = this.fetch.bind(this)
        this.login = this.login.bind(this)
        this.getProfile = this.getProfile.bind(this)
				this.changepassword = this.changepassword.bind(this)
				this.setpassword = this.setpassword.bind(this)
				this.resetpassword = this.resetpassword.bind(this)

        this.accessTokenName = 'access_token'
        this.refreshTokenName = 'refresh_token'
    }

    login(email, password) {
        return this.fetch(`${this.domain}/account/login`, {
            method: 'POST',
            body: JSON.stringify({
                email,
                password
            })
        }).then(res => res.json()).then( res => {
            this.setToken(res.token, this.accessTokenName);
            this.setToken(res.refreshToken, this.refreshTokenName);
            return Promise.resolve(res);
        })
    }

    register(email, password, firstName, lastName) {
        return this.fetch(`${this.domain}/account/signup`, {
            method: 'POST',
            body: JSON.stringify({
                email,
                password,
                firstName,
                lastName
            })
        }).then(res => {
            return Promise.resolve(res);
        })
    }

	confirmMail(token) {
        return this.fetch(`${this.domain}/account/ConfirmEmail/${token}`, {
            method: 'POST'
        });
    }

    loggedInWithRefresh() {
        let token = this.getToken(this.accessTokenName)
        if (!!token && this.isTokenExpired(token)) {
            this.refresh()
        }

        token = this.getToken(this.accessTokenName)
        return !!token && !this.isTokenExpired(token)
    }

    loggedIn() {
        const token = this.getToken(this.accessTokenName)
        return !!token && !this.isTokenExpired(token)
    }

    isTokenExpired(token) {
        try {
            const decoded = decode(token);
            if (decoded.exp < Date.now() / 1000)
                return true;
            else
                return false;
        }
        catch (err) {
            return false;
        }
    }

    refresh() {
        const accessToken = this.getToken(this.accessTokenName);
        const refreshToken = this.getToken(this.refreshTokenName)
        const body = JSON.stringify({
            accessToken,
            refreshToken
        });

        return this.fetch(`${this.domain}/token/refresh`, {
            method: 'POST',
            body: body
        }).then(res => res.json()).then( res => {
            this.setToken(res.token, this.accessTokenName);
            this.setToken(res.refreshToken, this.refreshTokenName);
        })
    }

    setToken(idToken, name) {
        localStorage.setItem(name, idToken)
    }

    getToken(name) {
        return localStorage.getItem(name)
    }

    logout() {
        return this.fetch(`${this.domain}/token/revoke`, {
            method: 'POST',
        }).then(() => {
            localStorage.removeItem(this.accessTokenName);
            localStorage.removeItem(this.refreshTokenName);
        }).catch(() => {
            localStorage.removeItem(this.accessTokenName);
            localStorage.removeItem(this.refreshTokenName);
        })
    }

    getProfile() {
        return decode(this.getToken(this.accessTokenName));
    }

		changepassword(oldpassword, newpassword) {
				let body = "";
				const obj = {
					oldpassword,
					newpassword
				};

				body = JSON.stringify(obj);

				return this.fetch(`${this.domain}/account/changepassword`, {
					method: 'post',
					body
				});
		}

		setpassword(newpassword, token, email) {
			let body = "";
			const obj = {
				newpassword,
				token,
				email,
			};

			body = JSON.stringify(obj);

			return this.fetch(`${this.domain}/account/setpassword`, {
				method: 'post',
				body
			});
		}

		resetpassword(email) {
			let body = "";
			const obj = {
				email,
			};
			body = JSON.stringify(obj);
			return this.fetch(`${this.domain}/account/resetpassword`, {
				method: 'post',
				body
			});
		}

    fetch(url, options) {
        const headers = {
            'Content-Type': 'application/json'
        }

        if (this.loggedIn()) {
            headers['Authorization'] = 'Bearer ' + this.getToken(this.accessTokenName)
        }

        return fetch(url, {
            headers,
            ...options
        })
            .then(this._checkStatus)
    }

    fetchParseJson(url, options) {
        const headers = {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        }

        if (this.loggedIn()) {
            headers['Authorization'] = 'Bearer ' + this.getToken(this.accessTokenName)
        }

        return fetch(url, {
            headers,
            ...options
        })
            .then(this._checkStatus)
            .then(response => response.json())
    }

    _checkStatus(response) {
        if (response.status >= 200 && response.status < 300) {
            return response
        } else {
            var error = new Error(response.statusText)
            error.response = response
            throw error
        }
    }

}
export default AuthService;
