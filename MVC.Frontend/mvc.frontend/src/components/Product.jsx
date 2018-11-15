import React, { Component } from "react";
import "./Product.css";

class Product extends Component {
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
    //eslint-disable-next-line
    let { discount, name, price } = this.props;

    return (
      <div className="product thumbnail">
        <img
          src={"data:image/jpeg;base64," + this.props.imageBase64}
          alt={this.props.name}
        />
        <div className="namebox">{name}</div>
        <div className="pricebox">
          <span className="units">{this.extractUnits(price)}</span>
          <span className="decimals">{this.extractDecimals(price)}</span>
        </div>
      </div>
    );
  }
}

export default Product;
