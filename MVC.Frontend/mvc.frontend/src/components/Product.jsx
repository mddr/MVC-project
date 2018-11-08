import React, { Component } from "react";
import "./Product.css";

class Product extends Component {
  state = {
    imgpath: "placeholder.png",
    discount: false,
    name: "Skarbonka NZS",
    price: 5.5
  };

  extractUnits = price => {
    return Math.floor(price);
  };

  extractDecimals = price => {
    let decimals = "" + (price % 1);
    decimals = decimals.substr(1);
    if (decimals.length === 2) decimals += "0";
    return decimals + "zł";
  };

  render() {
    return (
      <div className="product thumbnail">
        <a href="#">
          {/*<img src={ this.state.imgpath } alt="zdjęcie"/>*/}
          <div className="greenbox" />
          <div className="namebox">{this.state.name}</div>
          <div className="pricebox">
            <span className="units">{this.extractUnits(this.state.price)}</span>
            <span className="decimals">
              {this.extractDecimals(this.state.price)}
            </span>
          </div>
        </a>
      </div>
    );
  }
}

export default Product;
