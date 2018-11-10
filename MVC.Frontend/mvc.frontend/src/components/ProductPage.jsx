import "./ProductPage.css";
import "./Home.css";

import React, { Component } from "react";
import { Button, Glyphicon, InputGroup, FormControl } from "react-bootstrap";

class ProductPage extends Component {
  state = {
    price: 100.01,
    name: "NAJNOWSZA RZECZ PROSTO Z FABRYKI",
    description:
      "Lorem ipsum dolor sit, amet consectetur adipisicing elit. Quam ex illo assumenda veritatis illum soluta, perspiciatis sint adipisci autem iusto deleniti necessitatibus provident quo debitis excepturi aliquam, atque, odit tempora consectetur architecto. Repudiandae vitae atque exercitationem, repellat voluptatum numquam at iste distinctio voluptate recusandae tempora commodi sed reprehenderit consequatur eos?",
    timesBought: 12,
    count: 1
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
    const addValue = () => {
      const count = this.state.count + 1;
      this.setState({ count });
    };

    const subtractValue = () => {
      const count = this.state.count > 1 ? this.state.count - 1 : 1;
      this.setState({ count });
    };
    return (
      <div className="productContainer">
        <div
          className="banner"
          style={{
            gridColumn: "1"
          }}
        >
          <div className="banner_img" />
        </div>
        <div className="productSection">
          <div className="productImageSection">
            <div
              className="ph600x500"
              style={{
                width: "600px",
                height: "500px",
                backgroundColor: "#EEE000"
              }}
            />
          </div>
          <div className="productDescribeSection">
            <div className="productTitle">
              <h3>{this.state.name}</h3>
            </div>
            <hr />

            <div className="priceAndButtonsWrapper">
              <div className="productPrice">
                <span className="unitsPriceValue">
                  {this.extractUnits(this.state.price)}
                </span>
                <span className="decimalPriceValue">
                  {this.extractDecimals(this.state.price)}
                </span>
                <span className="currencySign">zł</span>
              </div>
              <div
                className="buyButtonWrapper"
                style={{
                  marginBottom: "3px"
                }}
              >
                <Button bsStyle="buyButton" onClick="" bsSize="large">
                  <Glyphicon glyph="plus" /> Dodaj do koszyka
                </Button>
              </div>
              <div className="numberPicker">
                <InputGroup style={{ margin: "auto" }}>
                  <InputGroup.Button>
                    <Button onClick={addValue}>
                      <Glyphicon glyph="chevron-up" />
                    </Button>
                  </InputGroup.Button>
                  <FormControl
                    value={this.state.count}
                    style={{ width: "50px", textAlign: "center" }}
                    onChange={(
                      x: React.FormEvent<FormControl & HTMLInputElement>
                    ) => {
                      this.setState({
                        count: isNaN(parseInt(x.currentTarget.value))
                          ? 1
                          : parseInt(x.currentTarget.value)
                      });
                    }}
                  />
                  <InputGroup.Button>
                    <Button onClick={subtractValue}>
                      <Glyphicon glyph="chevron-down" />
                    </Button>
                  </InputGroup.Button>
                </InputGroup>
              </div>
              <div
                className="buyButtonWrapper"
                style={{
                  marginTop: "3px"
                }}
              >
                <Button bsStyle="buyButton" onClick="" bsSize="large">
                  <Glyphicon glyph="ok" /> Kup teraz
                </Button>
              </div>
            </div>

            <div className="boughtCounter">
              <span style={{ color: "gray" }}>
                ten przedmiot kupiono {this.state.timesBought} razy
              </span>
            </div>
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
