import "./App.css";

import React, { Component } from "react";
import { Nav, Navbar, NavItem } from "react-bootstrap";
import { LinkContainer } from "react-router-bootstrap";
import { Link } from "react-router-dom";
import Footer from "./components/Footer";
import Cart from "./components/Cart";

import Routes from "./Routes";
import AuthService from "./services/AuthService";

const auth = new AuthService();

class App extends Component {
  async handleLogout() {
    auth.logout();
    await window.location.reload();
  }

  render() {
    const CartItems = [
      {
        product: {
          imgpath: "nadal nie wiem jak to działa",
          name: "Nazwa produktu 1",
          price: 49.99
        },
        count: 2
      },

      {
        product: {
          imgpath: "nadal nie wiem jak to działa",
          name: "Nazwa produktu 2",
          price: 24.99
        },
        count: 1
      }
    ];
    const isUserLogged = auth.loggedInWithRefresh();
    let loginControl;

    if (!isUserLogged) {
      loginControl = (
        <Navbar.Collapse>
          <Nav pullRight>
            <NavItem style={{ padding: "none" }}>
              <Cart Items={CartItems} />
            </NavItem>
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
          <Nav>
            <Cart Items={CartItems} />
          </Nav>
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
      </div>
    );
  }
}

export default App;
