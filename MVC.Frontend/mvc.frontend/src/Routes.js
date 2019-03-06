import React, { Component } from "react";
import { Route, Switch } from "react-router-dom";

import Home from "./components/HomePage/Home";
import Login from "./components/Auth/Login";
import Register from "./components/Auth/Register";
import AdminPanel from "./components/AdminPanel/AdminPanel";
import ProductPage from "./components/Products/ProductPage";
import OrderPage from "./components/Products/OrderPage";
import SearchResultsPage from "./components/Search/SearchResultsPage";
import ConfirmEmailPage from "./components/Auth/ConfirmEmailPage";
import RequireEmail from "./components/Auth/RequireEmail";
import ChangePasswordPage from "./components/Auth/ChangePasswordPage";
import ResetPasswordPage from "./components/Auth/ResetPasswordPage";
import UserPanel from "./components/UserPanel/UserPanel";

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
              userInfo={this.props.userInfo}
            />
          )}
        />
        <Route exact path="/Account/ConfirmEmail/:token" component={ConfirmEmailPage} />
				<Route exact path="/Account/SetPassword/:token" component={ChangePasswordPage} />
				<Route exact path="/Account/ResetPassword/" component={ResetPasswordPage} />
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
