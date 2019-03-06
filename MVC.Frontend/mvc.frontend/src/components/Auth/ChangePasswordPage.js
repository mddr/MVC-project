
import React, { Component } from 'react';
import { Button, Panel, FormGroup, FormControl } from 'react-bootstrap';

import AuthService from '../../services/AuthService';

class ChangePasswordPage extends Component {

	constructor(props) {
		super(props);
		this.state = {
			password: "",
			email: "",
		};
		this.AuthService = new AuthService();
		this.handleSubmit = this.handleSubmit.bind(this)
	}

	handleSubmit() {
		this.AuthService.setpassword(this.state.password, this.props.match.params.token, this.state.email);
	}

	validateForm() {
    if (!(this.state.password.length > 0)) return false;
    if (!(this.state.email.length > 0)) return false;
		return true;
	}

	handleChange = event => {
		this.setState({
			[event.target.id]: event.target.value
		});
	};

	render() {
		return (
			<div>
				{this.renderPassChange()}
			</div>
				);
	}

	renderPassChange() {
		return (
      <div className="pass_change_box">
        <Panel>
          <Panel.Heading />
					<form onSubmit={this.handleSubmit}>
						<FormGroup controlId="email">
							<FormControl
								value={this.state.email}
								onChange={this.handleChange}
								type="email"
								name="email"
								placeholder="Podaj email"
							/>
						</FormGroup>
						<FormGroup controlId="password">
							<FormControl
								value={this.state.password}
								onChange={this.handleChange}
								type="password"
								name="password"
								placeholder="Podaj nowe has³o"
							/>
						</FormGroup>
						<Button
								disabled={!this.validateForm()}
								type="submit"
								className="btn btn-success"
								block
							>
								Ustaw
							</Button>
					</form>
			</Panel>
		</div>
    );
	}
}

export default ChangePasswordPage;
