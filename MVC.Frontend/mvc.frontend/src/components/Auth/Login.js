import "./Login.css";

import React, { Component } from "react";
import { Link, Redirect } from "react-router-dom";
import { Button, FormControl, FormGroup, Panel } from "react-bootstrap";

import AuthService from "../../services/AuthService";

export class Login extends Component {
  constructor(props) {
    super(props);

    this.state = {
      email: "",
      password: ""
    };
    this.Auth = new AuthService();
    this.handleSubmit = this.handleSubmit.bind(this);
    this.resetPassword = this.resetPassword.bind(this);
  }

  componentWillMount() {
    if (this.Auth.loggedInWithRefresh()) this.props.history.replace("/");
  }

  validateForm() {
    return this.state.email.length > 0 && this.state.password.length > 0;
  }

  handleChange = event => {
    this.setState({
      [event.target.id]: event.target.value
    });
  };

  resetPassword() {
    this.Auth.resetpassword();
  }

  handleSubmit = event => {
    event.preventDefault();

    this.Auth.login(this.state.email, this.state.password)
      .then(() => {
        this.props.history.replace("/");
        window.location.reload();
      })
      .catch(() => {
        alert("Nieprawidłowy email lub hasło");
      });
  };

  render() {
    return (
      <div className="LoginForm">
        <Panel>
          <Panel.Heading>Logowanie</Panel.Heading>
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
            <FormGroup controlId="password">
              <FormControl
                value={this.state.password}
                onChange={this.handleChange}
                type="password"
                name="password"
                placeholder="Podaj hasło"
              />
            </FormGroup>
            <Button
              disabled={!this.validateForm()}
              type="submit"
              className="btn btn-success"
              block
            >
              Zaloguj się
            </Button>

            <Link to="/Account/ResetPassword">Nie pamietam hasła</Link>
          </form>
        </Panel>
      </div>
    );
  }
}
export default Login;
