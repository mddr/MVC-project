import "./App.css";

import React, { Component } from "react";
import {
  Nav,
  Navbar,
  NavItem,
  FormControl,
  Button,
  Glyphicon
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

const bkgCol = { bright: "#fafafa", dark: "#888888" };

class App extends Component {
  constructor(props) {
    super(props);
    this.state = {
      cartItems: [],
      cartItemChanged: -1,
      searchInput: "",
      userInfo: {},
      pressedLogout: false,
      navInverse: false
    };
    this.CartService = new CartService();
    this.ProductService = new ProductService();
    this.loadCart = this.loadCart.bind(this);
    this.cartItemChanged = this.cartItemChanged.bind(this);
    this.UserService = new UserService();
  }

  componentDidMount() {
    const isUserLogged = auth.loggedInWithRefresh();
    if (isUserLogged) {
      this.loadCart();
      this.UserService.getUserInfo()
        .then(data => data.json())
        .then(data => this.setState({ userInfo: data }));
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
    await this.CartService.getCart()
      .then(res => res.json())
      .then(data =>
        this.setState({
          cartItems: data.sort((a, b) => a.productId.localeCompare(b.productId))
        })
      )
      .catch(error => {
        if (auth.loggedIn)
          alert(
            "Do poprawnego dzialania strony wymagane jest potwierdzenie adresu email"
          );
      });
  }

  async handleLogout() {
    auth.logout();
    this.setState({
      pressedLogout: true,
      cartItems: [],
      cartItemsInfo: []
    });
  }
  darkMode = () => {
    console.log("Style: ", document.styleSheets);
    if (this.state.navInverse) {
      this.setState({ navInverse: false });
      document.getElementById("AppID").style.backgroundColor = bkgCol.bright;
    } else {
      this.setState({ navInverse: true });
      document.getElementById("AppID").style.backgroundColor = bkgCol.dark;
    }
  };

  render() {
    if (this.state.pressedLogout === true) {
      this.setState({ pressedLogout: false });
      return <Redirect to="/" />;
    }

    return (
      <div id="AppID" className="App">
        <Navbar fluid collapseOnSelect inverse={this.state.navInverse}>
          <Navbar.Header>
            <Navbar.Brand>
              <Link to="/">Strona główna</Link>
            </Navbar.Brand>
            <Navbar.Toggle />
          </Navbar.Header>
          {this.renderTopNavBar()}
        </Navbar>
        <Routes
          cartItems={this.state.cartItems}
          cartItemChanged={this.cartItemChanged}
          searchInput={this.state.searchInput}
          userInfo={this.state.userInfo}
        />
      </div>
    );
  }

  renderTopNavBar() {
    const isUserLogged = auth.loggedInWithRefresh();
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
              <button
                style={{ background: "none", border: "none", padding: 0 }}
                onClick={this.darkMode}
              >
                <i className="fa fa-moon-o" style={{ fontSize: 22 }} />
              </button>
            </NavItem>
            <NavItem style={{ padding: "none" }}>
              <Cart
                cartItems={this.state.cartItems}
                cartItemChanged={this.cartItemChanged}
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
      const userInfo = auth.getProfile();
      var keys = Object.keys(userInfo);
      loginControl = (
        <Navbar.Collapse>
          {searchBox}
          <Nav pullRight>
            <NavItem style={{ padding: "none" }}>
              <button
                style={{ background: "none", border: "none", padding: 0 }}
                onClick={this.darkMode}
              >
                <i className="fa fa-moon-o" style={{ fontSize: 22 }} />
              </button>
            </NavItem>

            <NavItem style={{ padding: "none" }}>
              <Cart
                cartItems={this.state.cartItems}
                cartItemChanged={this.cartItemChanged}
                userInfo={this.state.userInfo}
              />
            </NavItem>
            <Nav pullRight>
              <LinkContainer to="/user">
                <NavItem>Cześć {userInfo[keys[0]]}!</NavItem>
              </LinkContainer>
              <NavItem onClick={this.handleLogout.bind(this)}>
                Wyloguj się
              </NavItem>
            </Nav>
          </Nav>
        </Navbar.Collapse>
      );
    }
    return loginControl;
  }
}

export default App;
