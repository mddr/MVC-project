
import React, { Component } from "react";
import { Button, FormControl, FormGroup, Panel } from "react-bootstrap";

import AuthService from "../../services/AuthService";

export class ResetPasswordPage extends Component {
	constructor(props) {
		super(props);

		this.state = {
			email: "",
		};
		this.Auth = new AuthService();
		this.resetPassword = this.resetPassword.bind(this);
	}

	componentWillMount() {
		if (this.Auth.loggedInWithRefresh()) this.props.history.replace("/");
	}

	validateForm() {
		return this.state.email.length > 0;
	}

	handleChange = event => {
		this.setState({
			[event.target.id]: event.target.value
		});
	};

	resetPassword = event => {
		event.preventDefault();

		this.Auth.resetpassword(this.state.email)
			.then(() => {
				this.props.history.replace("/login");
			})
			.catch(() => {
				alert("Nieprawid³owy email lub has³o");
			});
	};

	render() {
		return (
			<div className="LoginForm">
				<Panel>
					<Panel.Heading>Reset has³a</Panel.Heading>
					<form onSubmit={this.handleSubmit}>
						<FormGroup controlId="email">
							<FormControl
								autoFocus
								type="email"
								value={this.state.email}
								onChange={this.handleChange}
								placeholder="Podaj adres email"
								name="email"
							/>
						</FormGroup>

						<Button
							onClick={this.resetPassword}>
							Reset has³a
						</Button>
					</form>
				</Panel>
			</div>
		);
	}
}
export default ResetPasswordPage;
