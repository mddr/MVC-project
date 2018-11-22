import React, { Component } from "react";
import "./Product.css";

class Product extends Component {
  extractUnits = (price, discount) => {
    price = discount ? price - (discount * price) / 100 : price;
    return Math.floor(price, discount);
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
        <div className="productimage">
          {discount ? <div className="discount"> P R O M O C J A </div> : ""}
          <img
            src={"data:image/jpeg;base64," + this.props.imageBase64}
            alt={this.props.name}
            style={{
              flexBasis: "60%"
            }}
          />
        </div>
        <div className="namebox">{name}</div>
        <div className="pricebox">
          <span className="units">{this.extractUnits(price, discount)}</span>
          <span className="decimals">{this.extractDecimals(price)}</span>
        </div>
      </div>
    );
  }
}

export default Product;
