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
    let { imgpath, discount, name, price } = this.props;

    return (
      <div className="product thumbnail">
        <a href="#">
          {/*<img src={ this.state.imgpath } alt="zdjęcie"/>*/}
          <div className="greenbox" />
          <div className="namebox">{name}</div>
          <div className="pricebox">
            <span className="units">{this.extractUnits(price)}</span>
            <span className="decimals">{this.extractDecimals(price)}</span>
          </div>
        </a>
      </div>
    );
  }
}

export default Product;
