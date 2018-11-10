import React from "react";
import { Route, Switch } from "react-router-dom";

import Home from "./components/Home";
import Login from "./components/Login";
import Register from "./components/Register";
import AdminPanel from "./components/AdminPanel";
import ProductPage from "./components/ProductPage";
import OrderPage from "./components/OrderPage";

export default () => (
  <Switch>
    <Route exact path="/" component={Home} />
    <Route exact path="/login" component={Login} />
    <Route exact path="/register" component={Register} />
    <Route exact path="/admin" component={AdminPanel} />
    <Route exact path="/product" component={ProductPage} />
    <Route exact path="/order" component={OrderPage} />
  </Switch>
);
