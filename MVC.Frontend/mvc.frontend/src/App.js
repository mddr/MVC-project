import "./App.css";

import React, { Component } from "react";
import { Nav, Navbar, NavItem } from "react-bootstrap";
import { LinkContainer } from "react-router-bootstrap";
import { Link } from "react-router-dom";
import Cart from "./components/Cart";

import Routes from "./Routes";
import AuthService from "./services/AuthService";
import CartService from "./services/CartService";

const auth = new AuthService();

class App extends Component {

    constructor(props) {
        super(props);
        this.state = {
            cartItems: [],
            cartItemsChanged: false,
        };
        this.CartService = new CartService();
    }

    componentDidMount() {
        this.CartService.getCart().then(res => res.json()).then(res => this.setState({ cartItems: res }));
    }

    componentDidUpdate() {
        if (this.state.ItemsChanged) {
            this.CartService.getCart().then(res => res.json()).then(res => this.setState({
                cartItems: res,
                cartItemsChanged: false,
            })
            );
        }
    }

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
            <NavItem style={{ padding: "none" }}>
                      <Cart cartItems={this.state.cartItems} />
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
                  <Cart cartItems={this.state.cartItems} />
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
            <Routes cartItems={this.state.cartItems} />
      </div>
    );
  }
}

export default App;
