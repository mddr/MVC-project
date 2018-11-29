import "./App.css";

import React, { Component } from "react";
import {
  Nav,
  Navbar,
  NavItem,
  FormControl,
  Button,
  Form
} from "react-bootstrap";
import { LinkContainer } from "react-router-bootstrap";
import { Link, Redirect } from "react-router-dom";
import Cart from "./components/Products/Cart";

import Routes from "./Routes";
import AuthService from "./services/AuthService";
import CartService from "./services/CartService";
import ProductService from "./services/ProductService";
import UserService from "./services/UserService";

const auth = new AuthService();

class App extends Component {
  constructor(props) {
    super(props);
    this.state = {
      cartItems: [],
      cartItemsInfo: [],
      cartItemChanged: -1,
      searchInput: "",
      userInfo: {},
      pressedLogout: false
    };
    this.CartService = new CartService();
    this.ProductService = new ProductService();
    this.UserService = new UserService();
    this.loadCart = this.loadCart.bind(this);
    this.cartItemChanged = this.cartItemChanged.bind(this);
  }

  componentDidMount() {
    const isUserLogged = auth.loggedInWithRefresh();
    if (isUserLogged) {
      this.loadCart();
      this.UserService.getUserInfo()
        .then(res => res.json())
        .then(data => {
          this.setState({ userInfo: { ...data } });
        });
    }
  }

  componentDidUpdate(prevProps, prevState) {
    if (this.state.cartItemChanged !== -1) {
      let items = this.state.cartItems;
      for (let i = 0; i < items.length; i++) {
        if (items[i].productId === this.state.cartItemChanged.productId) {
          items[i] = { ...this.state.cartItemChanged };
        }
      }

      this.setState({
        cartItems: items,
        cartItemChanged: -1
      });
    }
  }

  cartItemChanged(item) {
    this.setState({
      cartItemChanged: item
    });
  }

  async loadCart() {
    const loadCartInfo = async () => {
      let infos = [];
      this.state.cartItems.map(async (cartItem, i) => {
        const response = await this.ProductService.getProduct(
          cartItem.productId
        );
        const json = await response.json();
        infos.push({ ...json });
        this.setState({
          cartItemsInfo: infos.sort((a, b) => a.id.localeCompare(b.id))
        });
      });
    };

    await this.CartService.getCart()
      .then(res => res.json())
      .then(data =>
        this.setState(
          {
            cartItems: data.sort((a, b) =>
              a.productId.localeCompare(b.productId)
            )
          },
          loadCartInfo
        )
      );
  }

  async handleLogout() {
    auth.logout();
    this.setState({
      pressedLogout: true,
      cartItems: [],
      cartItemsInfo: [],
    })
  }

  handleInput;

  render() {
    const isUserLogged = auth.loggedInWithRefresh();
    if (this.state.pressedLogout === true && isUserLogged) {
      this.setState({ pressedLogout: false })
      return <Redirect to='/' />
    }
    const searchBox = (
      <Navbar.Form pullLeft>
        <FormControl
          id="search_input"
          type="text"
          placeholder="Wpisz nazwę..."
          style={{ margin: "auto", width: "25em" }}
          onChange={e => this.setState({ searchInput: e.target.value })}
        />{" "}
        <Link to="/search-results">
          <Button type="submit" style={{ margin: "auto" }}>
            Szukaj
          </Button>
        </Link>
      </Navbar.Form>
    );
    let loginControl;
    if (!isUserLogged) {
      loginControl = (
        <Navbar.Collapse>
          {searchBox}
          <Nav pullRight>
            <NavItem style={{ padding: "none" }}>
              <Cart
                cartItems={this.state.cartItems}
                cartItemChanged={this.cartItemChanged}
                cartItemsInfo={this.state.cartItemsInfo}
              />
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
          {searchBox}
          <Nav pullRight>
            <NavItem style={{ padding: "none" }}>
              <Cart
                cartItems={this.state.cartItems}
                cartItemChanged={this.cartItemChanged}
                cartItemsInfo={this.state.cartItemsInfo}
              />
            </NavItem>
            <Nav pullRight>
              <LinkContainer to="/user">
                <NavItem>Cześć {this.state.userInfo.firstName}!</NavItem>
              </LinkContainer>
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
        <Routes
          cartItems={this.state.cartItems}
          cartItemChanged={this.cartItemChanged}
          cartItemsInfo={this.state.cartItemsInfo}
          searchInput={this.state.searchInput}
          userInfo={this.state.userInfo}
        />
      </div>
    );
  }
}

export default App;
