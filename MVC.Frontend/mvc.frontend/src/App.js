import "./App.css";

import React, { Component } from "react";
import { Nav, Navbar, NavItem } from "react-bootstrap";
import { LinkContainer } from "react-router-bootstrap";
import { Link } from "react-router-dom";
import Cart from "./components/Cart";

import Routes from "./Routes";
import AuthService from "./services/AuthService";
import CartService from "./services/CartService";
import ProductService from "./services/ProductService";

const auth = new AuthService();

class App extends Component {
  constructor(props) {
    super(props);
    this.state = {
      cartItems: [],
      cartItemsInfo: [],
        cartItemsChanged: false,
    };
      this.CartService = new CartService();
      this.ProductService = new ProductService();
  }

    componentDidMount() {
        const requestCartInfo = async () => {
            this.state.cartItems.map(async (cartItem, i) => {
                const response = await this.ProductService.getProduct(cartItem.productId);
                const json = await response.json();
                let infos = this.state.cartItemsInfo;
                infos.push(json);
                this.setState({ cartItemsInfo: infos });
            });
        }
        const requestCart = async () => {
            await this.CartService.getCart()
                .then(res => res.json())
                .then(data => this.setState({ cartItems: data, cartItemsChanged: true }, requestCartInfo));
        }

        requestCart();
    }

  componentDidUpdate() {
      if (this.state.ItemsChanged) {
          this.state.cartItems.map(cartItem => {
              const promise = this.ProductService.getProduct(cartItem.id);
              console.log(promise);
              promise.then(res => res.json())
                  .then(data => {
                      console.log(data);
                      //let items = this.state.cartItems;
                      //let item;
                      //for(int jitems.
                  });
          });
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
          <Nav pullRight>
            <NavItem style={{ padding: "none" }}>
              <Cart cartItems={this.state.cartItems} />
            </NavItem>
            <Nav pullRight>
              <NavItem onClick={this.handleLogout.bind(this)}>
                Wyloguj się
              </NavItem>
            </Nav>
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
            <Routes cartItems={this.state.cartItems.sort((a, b) => a.productId.localeCompare(b.productId))}
                cartItemsInfo={this.state.cartItemsInfo.sort((a, b) => a.id.localeCompare(b.id))} />
      </div>
    );
  }
}

export default App;
