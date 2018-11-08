import "./App.css";

import React, { Component } from "react";
import { Nav, Navbar, NavItem } from "react-bootstrap";
import { LinkContainer } from "react-router-bootstrap";
import { Link } from "react-router-dom";
import Footer from "./components/Footer";

import Routes from "./Routes";
import AuthService from "./services/AuthService";

const auth = new AuthService();

class App extends Component {
  async handleLogout() {
    auth.logout();
    await window.location.reload();
  }

  render() {
    const isUserLogged = auth.loggedInWithRefresh();
    let loginControl;

    if (!isUserLogged) {
      loginControl = (
        <Navbar.Collapse>
          <Nav pullRight>
            <LinkContainer to="/login">
              <NavItem>Zaloguj się</NavItem>
            </LinkContainer>
            <LinkContainer to="/register">
              <NavItem>Zarejestruj się</NavItem>
            </LinkContainer>
          </Nav>
        </Navbar.Collapse>
      );
    } else {
      loginControl = (
        <Navbar.Collapse>
          <Nav pullRight>
            <NavItem onClick={this.handleLogout.bind(this)}>
              Wyloguj się
            </NavItem>
          </Nav>
        </Navbar.Collapse>
      );
    }

    return (
      <div className="App">
        <Navbar fluid collapseOnSelect>
          <Navbar.Header>
            <Navbar.Brand>
              <Link to="/">Strona główna</Link>
            </Navbar.Brand>
            <Navbar.Toggle />
          </Navbar.Header>

          {loginControl}
        </Navbar>
        <Routes />
        <Footer />
      </div>
    );
  }
}

export default App;
