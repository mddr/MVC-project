import React, { Component } from "react";
import {
  Panel,
  Button,
  FormGroup,
  FormControl,
} from "react-bootstrap";
import "./Login.css";

export class Login extends Component {
  constructor(props) {
    super(props);

    this.state = {
      email: "",
      password: ""
    };
  }

  validateForm() {
    return this.state.email.length > 0 && this.state.password.length > 0;
  }

  handleChange = event => {
    this.setState({
      [event.target.id]: event.target.value
    });
  };

  handleSubmit = event => {
    event.preventDefault();
    const data = new FormData(event.target);

    fetch("https://localhost:34249/login", {
      method: "POST",
      body: data
    }).then(res => console.log(res.json()));
  };

  render() {
    return (
      <div className="LoginForm">
        <Panel>
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
          </form>
        </Panel>
      </div>
    );
  }
}
export default Login;
