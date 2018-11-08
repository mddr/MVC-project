import "./Home.css";

import React, { Component } from "react";
import ProductSlider from "./ProductSlider";
import { SideNav } from "react-sidenav";

export default class Home extends Component {
  constructor(props) {
    super(props);

    this.state = {
      isUserLogged: false
    };
  }

  render() {
    return (
      <div className="home">
        <div className="ad">
          <div className="ad_img" />
          {/* <img src="img/hangers_long.png" alt="hangers_long" /> */}
        </div>
        <div className="categories">
          <SideNav />
        </div>
        <div className="sale">
          Promocje
          <hr />
          {<ProductSlider />}
        </div>
        <div className="news">
          Nowości
          <hr />
          {<ProductSlider />}
        </div>
        <div className="mostpopular">{}</div>
      </div>
    );
  }
}
