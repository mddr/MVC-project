import React, { Component } from "react";
import { Route, Switch } from "react-router-dom";

import Home from "./components/Home";
import Login from "./components/Login";
import Register from "./components/Register";
import AdminPanel from "./components/AdminPanel";
import ProductPage from "./components/ProductPage";
import OrderPage from "./components/OrderPage";

class Routes extends Component {
    render() {
        return (
            <Switch >
                <Route exact path="/" component={Home} />
                <Route exact path="/login" component={Login} />
                <Route exact path="/register" component={Register} />
                <Route exact path="/admin" component={AdminPanel} />
                <Route exact path="/product/:id" component={ProductPage} />
                <Route exact path="/order" render={() => <OrderPage cartItems={this.props.cartItems} />}  />
            </Switch>
        );
    }
}
export default Routes;
