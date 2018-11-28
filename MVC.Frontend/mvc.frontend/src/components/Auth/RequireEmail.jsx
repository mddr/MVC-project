import React, { Component } from "react";
import "./RequireEmail.css";
import { LinkContainer } from "react-router-bootstrap";

class RequireEmail extends Component {
  state = {};
  render() {
    return (
      <div className="wrapper">
        <div className="simpleDiv">
          <h1>Aktywacja konta</h1>
          <p>
            Użytkowniku, na podany adres email został wysłany link aktywacyjny.
            Przed pierwszym logowaniem wymagana jest aktywacja konta.
          </p>
        </div>
      </div>
    );
  }
}

export default RequireEmail;
