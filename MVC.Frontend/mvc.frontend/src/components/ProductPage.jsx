import "./ProductPage.css";
import "./Home.css";

import React, { Component } from "react";
import { Button, Glyphicon, InputGroup, FormControl } from "react-bootstrap";

import AuthService from '../services/AuthService';

class ProductPage extends Component {

   constructor(props) {
        super(props);

        this.state = {
            pricePln: -1,
            name: "",
            description: "",
            boughtTimes: -1,
            count: 0
        };

       this.Auth = new AuthService();
       this.fetchData = this.fetchData.bind(this);
       this.buyNow = this.buyNow.bind(this);
       this.addToCart = this.addToCart.bind(this);
    }

    componentDidMount() {
        this.fetchData();
    }

    fetchData() {
        this.Auth.fetch(`${this.Auth.domain}/product/${this.props.match.params.id}`, null
        ).then(res => res.json()).then(res => {
            this.setState({
                pricePln: res.pricePln,
                name: res.name,
                description: res.description,
                boughtTimes: res.boughtTimes,
                imageBase64: res.imageBase64,
                id: res.id
            });
        });
    }

    addToCart() {
        let body = "";
        const obj = {
            productId: this.state.id,
            productAmount: this.state.count,
        };

        body = JSON.stringify(obj);

        this.Auth.fetch(`${this.Auth.domain}/cart/add`, {
            method: 'post',
            body
        });
    }

    validate() {
        return this.state.count < 1;
    }

    buyNow() {
        this.addToCart();
        this.props.history.replace("/order");
    }

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
                    <img src={"data:image/jpeg;base64," + this.state.imageBase64} alt={this.state.name} />
          </div>
          <div className="productDescribeSection">
            <div className="productTitle">
              <h3>{this.state.name}</h3>
            </div>
            <hr />

            <div className="priceAndButtonsWrapper">
              <div className="productPrice">
                <span className="unitsPriceValue">
                  {this.extractUnits(this.state.pricePln)}
                </span>
                <span className="decimalPriceValue">
                    {this.extractDecimals(this.state.pricePln)}
                </span>
                <span className="currencySign">zł</span>
              </div>
              <div
                className="buyButtonWrapper"
                style={{
                  marginBottom: "3px"
                }}
                        >
                            <Button bsStyle="buyButton" onClick={this.addToCart} bsSize="large" disabled={this.validate()} >
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
                <Button bsStyle="buyButton" onClick={this.buyNow} bsSize="large" disabled={this.validate()}>
                  <Glyphicon glyph="ok" /> Kup teraz
                </Button>
              </div>
            </div>

            <div className="boughtCounter">
              <span style={{ color: "gray" }}>
                            ten przedmiot kupiono {this.state.boughtTimes} razy
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
