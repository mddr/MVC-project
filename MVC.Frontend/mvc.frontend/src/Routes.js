import React from 'react';
import { Route, Switch } from 'react-router-dom';

import { Home } from './components/Home';
import { Login } from './components/Login';

export default () => (
  <Switch>
    <Route exact path="/" exact component={Home}/>
    <Route path="/login" exact component={Login} />
  </Switch>
);
