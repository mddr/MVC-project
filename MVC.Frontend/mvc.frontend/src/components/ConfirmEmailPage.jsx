import React, { Component } from "react";
import "./ConfirmEmailPage.css";
import { Button } from "react-bootstrap";
import { LinkContainer } from "react-router-bootstrap";
import AuthService from "../services/AuthService";

class ConfirmEmailPage extends Component {

    constructor(props) {
        super(props);
        this.state = {
            confirmed: false
        };
        this.AuthService = new AuthService();
    }
    render() {
        this.AuthService.confirmMail();
    return (
      <div className="confirmView">
        <div className="simpleDiv">
          <h1>Udało Się!</h1>
          <p>
            Twój adres email został potwierdzony. Teraz możesz zalogować się do
            serwisu.
          </p>

          <LinkContainer to="/login">
            <Button bsStyle="success">Zaloguj się</Button>
          </LinkContainer>
        </div>
      </div>
    );
  }
}

export default ConfirmEmailPage;
