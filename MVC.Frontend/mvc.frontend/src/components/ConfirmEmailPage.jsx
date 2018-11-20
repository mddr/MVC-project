import React, { Component } from "react";
import "./ConfirmEmailPage.css";
import { Button } from "react-bootstrap";
import { LinkContainer } from "react-router-bootstrap";

class ConfirmEmailPage extends Component {
  state = {};
  render() {
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
