import React, { Component } from 'react';
import { Button, FormControl, FormGroup, Panel } from 'react-bootstrap';

import AuthService from '../services/AuthService';
import "./Register.css";

export default class Register extends Component {
  constructor(props) {
    super(props);

    this.state = {
      email: "",
      password: "",
      password2: "",
      firstName: "",
      lastName: ""
    };
    this.Auth = new AuthService();
    this.handleSubmit = this.handleSubmit.bind(this);
  }

  componentWillMount() {
    if (this.Auth.loggedInWithRefresh()) this.props.history.replace("/");
  }

  validateForm() {
    if (!(this.state.email.length > 0 && this.state.password.length > 0 && this.state.firstName.length > 0 && this.state.lastName.length > 0))
      return false;
    if (this.state.password !== this.state.password2)
      return false;
    return true;
  }

  handleChange = event => {
    this.setState({
      [event.target.id]: event.target.value
    });
  };

  handleSubmit = event => {
    event.preventDefault();

    this.Auth.register(this.state.email, this.state.password, this.state.firstName, this.state.lastName)
      .then(() => {
        this.props.history.replace("/login");
      })
      .catch(err => {
        if (err.response.status === 409)
          alert("Podany adres email jest już w użyciu.")
      });
  };

  render() {
    return (
      <div className="RegisterForm">
        <Panel>
          <Panel.Heading>
            Rejestracja
          </Panel.Heading>
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
            <FormGroup controlId="password2">
              <FormControl
                value={this.state.password2}
                onChange={this.handleChange}
                type="password"
                name="password2"
                placeholder="Wpisz hasło jeszcze raz"
              />
            </FormGroup>

            <FormGroup controlId="firstName">
              <FormControl
                value={this.state.firstName}
                onChange={this.handleChange}
                type="text"
                name="firstName"
                placeholder="Imię"
              />
            </FormGroup>
            <FormGroup controlId="lastName">
              <FormControl
                value={this.state.lastName}
                onChange={this.handleChange}
                type="text"
                name="lastName"
                placeholder="Nazwisko"
              />
            </FormGroup>

            <Button
              disabled={!this.validateForm()}
              type="submit"
              className="btn btn-success"
              block
            >
              Zarejestruj się
            </Button>
          </form>
        </Panel>
      </div>
    )
  }
}
