import "./ProductPage.css";

import React, { Component } from "react";
import { Button, Glyphicon } from "react-bootstrap";

class ProductPage extends Component {
  state = {
    price: 100.01,
    name: "NAJNOWSZA RZECZ PROSTO Z FABRYKI",
    description:
      "Lorem ipsum dolor sit, amet consectetur adipisicing elit. Quam ex illo assumenda veritatis illum soluta, perspiciatis sint adipisci autem iusto deleniti necessitatibus provident quo debitis excepturi aliquam, atque, odit tempora consectetur architecto. Repudiandae vitae atque exercitationem, repellat voluptatum numquam at iste distinctio voluptate recusandae tempora commodi sed reprehenderit consequatur eos?"
  };

  extractUnits = price => {
    return Math.floor(price);
  };

  extractDecimals = price => {
    let decimals = "" + Math.round((price % 1) * 100) / 100;
    decimals = decimals.substr(1);
    if (decimals.length === 2) decimals += "0";
    return decimals;
  };

  render() {
    return (
      <div className="productContainer">
        <div className="banner">
          <div className="banner_img" />
        </div>
        <div className="product">
          <div className="productImageSection">
            <div
              className="ph600x600"
              style={{
                width: "600px",
                height: "600px",
                backgroundColor: "#EEE000"
              }}
            />
          </div>
          <div className="productDescribeSection">
            <div className="productTitle">
              <h3>{this.state.name}</h3>
            </div>
            <hr />

            <div className="priceAndButtonWrapper">
              <div className="productPrice">
                <span className="unitsPriceValue">
                  {this.extractUnits(this.state.price)}
                </span>
                <span className="decimalPriceValue">
                  {this.extractDecimals(this.state.price)}
                </span>
                <span className="currencySign">zł</span>
              </div>
              <div className="buyButtonWrapper">
                <Button bsStyle="buyButton" onClick="" bsSize="large">
                  <Glyphicon glyph="plus" /> Dodaj do koszyka
                </Button>
              </div>
            </div>

            <div className="boughtCounter">ten przedmiot kupiono {12} razy</div>
            <hr />
            <div className="description">{this.state.description}</div>
          </div>
        </div>

        {/* TODO: Breadcrumbs lokacja na podstawie kategorii */}
      </div>
    );
  }
}

export default ProductPage;
