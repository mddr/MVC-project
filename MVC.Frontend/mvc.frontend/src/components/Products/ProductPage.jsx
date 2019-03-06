import "../HomePage/Home.css";
import "./ProductPage.css";

import React, { Component } from "react";
import { Button, FormControl, Glyphicon, InputGroup } from "react-bootstrap";
import ReactHtmlParser from "react-html-parser";
import AuthService from "../../services/AuthService";
import UserService from "../../services/UserService";
import CartService from "../../services/CartService";

class ProductPage extends Component {
  constructor(props) {
    super(props);

    this.state = {
      pricePln: -1,
      discount: 0,
      name: "",
      description: "",
      boughtTimes: -1,
      count: 0,
      netto: false,
      taxRate: 0,
      files: []
    };

    this.Auth = new AuthService();
    this.UserService = new UserService();
    this.CartService = new CartService();
    this.fetchData = this.fetchData.bind(this);
    this.buyNow = this.buyNow.bind(this);
    this.downloadFiles = this.downloadFiles.bind(this);
    this.addToCart = this.addToCart.bind(this);
  }

  componentDidMount() {
    this.fetchData();
  }

  fetchData() {
    this.UserService.getUserInfo()
      .then(res => res.json())
      .then(res => {
        this.setState({ netto: res.prefersNetPrice }, () => {
          this.Auth.fetch(
            `${this.Auth.domain}/product/${this.props.match.params.id}`,
            null
          )
            .then(res => res.json())
            .then(res => {
              this.setState({
                pricePln: this.state.netto
                  ? this.afterTaxPrice(res.pricePln, res.taxRate)
                  : res.pricePln,
                discount: res.discount,
                name: res.name,
                description: res.description,
                boughtTimes: res.boughtTimes,
                imageBase64: res.imageBase64,
                id: res.id,
                taxRate: res.taxRate,
                files: res.files
              });
            });
        });
      })
      .catch(x => {
        this.setState({ netto: false }, () => {
          this.Auth.fetch(
            `${this.Auth.domain}/product/${this.props.match.params.id}`,
            null
          )
            .then(res => res.json())
            .then(res => {
              this.setState({
                pricePln: this.state.netto
                  ? this.afterTaxPrice(res.pricePln, res.taxRate)
                  : res.pricePln,
                discount: res.discount,
                name: res.name,
                description: res.description,
                boughtTimes: res.boughtTimes,
                imageBase64: res.imageBase64,
                id: res.id,
                taxRate: res.taxRate,
                files: res.files
              });
            });
        });
      });
  }

  addToCart() {
    for (let i = 0; i < this.props.cartItems.length; i++) {
      if (this.props.cartItems[i].product.id === this.state.id) {
        this.CartService.updateItem(
          this.state.id,
          this.state.count + this.props.cartItems[i].productAmount
        )
          .then(() => window.location.reload())
          .catch(error =>
            alert(
              "Przed dokonaniem zakupu wymagane jest potwierdzenie adresu email"
            )
          );
        return;
      }
    }
    this.CartService.addItem(this.state.id, this.state.count)
      .then(() => window.location.reload())
      .catch(error =>
        alert(
          "Przed dokonaniem zakupu wymagane jest potwierdzenie adresu email"
        )
      );
  }

  afterTaxPrice(price, taxRate) {
    price -= price * (taxRate / 100);
    return price;
  }

  validate() {
    return this.state.count < 1;
  }

  buyNow() {
    this.addToCart();
    this.props.history.replace("/order");
  }

  extractUnits = price => {
    return Math.floor(price - (this.state.discount * price) / 100);
  };

  extractDecimals = price => {
    let decimals =
      "" +
      Math.round(((price - (this.state.discount * price) / 100) % 1) * 100) /
        100;
    decimals = decimals.substr(1);
    if (decimals.length === 2) decimals += "0";
    return decimals;
  };

  downloadFiles() {
    for (var i = 0; i < this.state.files.length; i++) {
      this.download(this.state.files[i].fileName, this.state.files[i].base64);
    }
  }

  download(filename, base64) {
    var element = document.createElement('a');
    element.setAttribute('href', 'data:application/octet-stream;base64,' + base64);
    element.setAttribute('download', filename);

    element.style.display = 'none';
    document.body.appendChild(element);

    element.click();

    document.body.removeChild(element);
  }

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
            <img
              src={"data:image/jpeg;base64," + this.state.imageBase64}
              alt={this.state.name}
            />
          </div>
          <div className="productDescribeSection">
            <div className="productTitle">
              <h3>{this.state.name}</h3>
            </div>
            <hr />

            <div className="priceAndButtonsWrapper">
              <div className="productPrice">
                {this.state.discount ? (
                  <div
                    style={{
                      textDecoration: "line-through",
                      fontSize: "20px",
                      color: "darkgrey",
                      fontWeight: "400",
                      margin: "0"
                    }}
                  >
                    {Math.round(this.state.pricePln * 100) / 100}zł
                  </div>
                ) : (
                  ""
                )}
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
                <Button
                  bsClass="btn buyButton"
                  onClick={this.addToCart}
                  bsSize="large"
                  disabled={this.validate()}
                >
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
                    onChange={x => {
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
                <Button
                  bsClass="btn buyButton"
                  onClick={this.buyNow}
                  bsSize="large"
                  disabled={this.validate()}
                >
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
            <div className="description">
              {ReactHtmlParser(this.state.description)}
            </div>

            {this.state.files.length > 0 ? (<div>
              <span>
                <Button
                  onClick={this.downloadFiles}
                >
                  Pobierz wszystkie pliki związane z produktem</Button>
              </span>
              {this.renderFilesDownloadLink()}
            </div>

          ) : null }
          </div>
        </div>

        {/* TODO: Breadcrumbs lokacja na podstawie kategorii */}
      </div>
    );
  }

  renderFilesDownloadLink() {
    var links = [];
    this.state.files.map((link) => links.push(<li>
      <a href={"data:application/octet-stream;base64,"+link.base64}
        download={link.fileName}
      >
        {link.fileName}
      </a > - {link.description}</li>));
    return (<ul>
      {links}
    </ul>);
  }
}

export default ProductPage;
