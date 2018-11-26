import React, { Component } from "react";
import { Route, Switch } from "react-router-dom";

import Home from "./components/Home";
import Login from "./components/Login";
import Register from "./components/Register";
import AdminPanel from "./components/AdminPanel";
import ProductPage from "./components/ProductPage";
import OrderPage from "./components/OrderPage";
import SearchResultsPage from "./components/SearchResultsPage";
import ConfirmEmailPage from "./components/ConfirmEmailPage";
import RequireEmail from "./components/RequireEmail";
import UserPanel from "./components/UserPanel";

class Routes extends Component {
  render() {
    return (
      <Switch>
        <Route exact path="/" component={Home} />
        <Route exact path="/login" component={Login} />
        <Route exact path="/register" component={Register} />
        <Route exact path="/admin" component={AdminPanel} />
        <Route
          exact
          path="/product/:id"
          render={props => (
            <ProductPage
              {...props}
              cartItems={this.props.cartItems}
              cartItemsInfo={this.props.cartItemsInfo}
            />
          )}
        />
        <Route
          exact
          path="/order"
          render={props => (
            <OrderPage
              {...props}
              cartItems={this.props.cartItems}
              cartItemsInfo={this.props.cartItemsInfo}
            />
          )}
        />
        <Route
          exact
          path="/search-results"
          render={props => (
            <SearchResultsPage
              {...props}
              searchInput={this.props.searchInput}
            />
          )}
        />
        <Route exact path="/email-confirmed" component={ConfirmEmailPage} />
        <Route exact path="/email-require" component={RequireEmail} />
        <Route
          exact
          path="/user"
          render={props => (
            <UserPanel {...props} userInfo={this.props.userInfo} />
          )}
        />
      </Switch>
    );
  }
}
export default Routes;
