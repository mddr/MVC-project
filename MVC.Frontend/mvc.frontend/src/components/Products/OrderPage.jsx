import React, { Component } from "react";
import { Button, Glyphicon, FormGroup, FormControl } from "react-bootstrap";

import "./OrderPage.css";
import "../HomePage/Home.css";
import "./ProductPage.css";
import CartService from "../../services/CartService";
import ProductService from "../../services/ProductService";
import AddressService from "../../services/AddressService";
import OrderService from "../../services/OrderService";

class OrderPage extends Component {
  constructor(props) {
    super(props);
    this.state = {
      totalPrice: 0,
      street: "",
      houseNumber: "",
      postalCode: "",
        city: "",
        hasAddress: false
    };
    this.getTotalPrice = this.getTotalPrice.bind(this);
    this.handleChange = this.handleChange.bind(this);
    this.placeOrder = this.placeOrder.bind(this);
    this.renderItems = this.renderItems.bind(this);
    this.disableSubmit = this.disableSubmit.bind(this);
    this.addressChanged = this.addressChanged.bind(this);
    this.CartService = new CartService();
    this.ProductService = new ProductService();
    this.AddressService = new AddressService();
    this.OrderService = new OrderService();
  }

    componentDidMount() {
        this.AddressService.userAddress()
            .then(res => res.json())
            .then(data => {
                this.setState({
                    ...data,
                    hasAddress: true,
                    remoteAddres: {...data}
                })
            })
    }

    getTotalPrice() {
    if (this.props.cartItems.length < 1) return 0;
    if (this.props.cartItemsInfo.length < 1) return 0;
    if (this.props.cartItemsInfo.length != this.props.cartItems.length)
      return 0;
    let price = 0;
    for (let i = 0; i < this.props.cartItemsInfo.length; i++) {
      price +=
        (this.props.cartItemsInfo[i].pricePln -
          (this.props.cartItemsInfo[i].discount *
            this.props.cartItemsInfo[i].pricePln) /
            100) *
        this.props.cartItems[i].productAmount;
    }
    return price;
  }

    addressChanged() {
        if (this.state.city.localeCompare(this.state.remoteAddres.city) !== 0) return true;
        if (this.state.postalCode.localeCompare(this.state.remoteAddres.postalCode) !== 0) return true;
        if (this.state.street.localeCompare(this.state.remoteAddres.street) !== 0) return true;
        if (this.state.houseNumber.localeCompare(this.state.remoteAddres.houseNumber) !== 0) return true;
        return false;
    }

    placeOrder() {
        if (this.state.hasAddress && !this.addressChanged()) {
        this.OrderService.add()
            .then(() => window.location.reload());
    } else
        this.AddressService.add(
        this.state.city,
        this.state.postalCode,
        this.state.street,
        this.state.houseNumber
        )
          .then(() => {
            this.OrderService.add();
          })
          .then(() => window.location.reload());
    }

    handleChange = event => {
    this.setState({
      [event.target.id]: event.target.value
    });
  };

    renderItems() {
    if (this.props.cartItems.length < 1) return;
    if (this.props.cartItemsInfo.length < 1) return;
    if (this.props.cartItemsInfo.length != this.props.cartItems.length) return;
    let items = [];
    this.props.cartItems.map((item, i) => {
      items.push(
        <div className="item">
          <img
            src={
              "data:image/jpeg;base64," +
              this.props.cartItemsInfo[i].imageBase64
            }
            alt={this.props.cartItemsInfo[i].name}
            height="64"
            width="64"
          />
          <span className="namespan">
            {item.productAmount} x {this.props.cartItemsInfo[i].name}
          </span>
          <span>
            {Math.round(
              (this.props.cartItemsInfo[i].pricePln -
                (this.props.cartItemsInfo[i].discount *
                  this.props.cartItemsInfo[i].pricePln) /
                  100) *
                item.productAmount *
                100
            ) / 100}
            zł
          </span>
          <span />
        </div>
      );
    });

    return items;
  }

    disableSubmit() {
        return this.state.city.length < 1 ||
            this.props.cartItems.length < 1 ||
            this.state.postalCode.length < 1 ||
            this.state.houseNumber.length < 1 ||
            this.state.street.length < 1;
    }
  render() {
    const items =
      this.props.cartItems.length > 0 ? (
        this.renderItems()
      ) : (
        <div>Zamówienie jest puste</div>
      );
    return (
      <div className="orderpage">
        <div className="banner">
          <div className="banner_img" />
        </div>
        <div className="summary">
          <p>Zamówienie:</p>
          <hr style={{ width: "95%" }} />
          {items}
          <hr style={{ width: "95%" }} />
          <div className="ordersum">
            <div className="ordersumtext">Suma: </div>
            <div>{this.getTotalPrice()}zł</div>
          </div>
        </div>
        <div className="shippinginfo">
          <p>Dane wysyłki:</p>
          <hr style={{ width: "95%" }} />
          <FormGroup
            style={{ margin: "auto", width: "80%" }}
            controlId="street"
          >
            <FormControl
              value={this.state.street}
              onChange={this.handleChange}
              type="street"
              name="steet"
              placeholder="Ulica"
            />
          </FormGroup>
          <FormGroup
            style={{ margin: "auto", width: "80%" }}
            controlId="houseNumber"
          >
            <FormControl
              value={this.state.houseNumber}
              onChange={this.handleChange}
              type="houseNumber"
              name="houseNumber"
              placeholder="Numer domu"
            />
          </FormGroup>
          <FormGroup style={{ margin: "auto", width: "80%" }} controlId="city">
            <FormControl
              value={this.state.city}
              onChange={this.handleChange}
              type="city"
              name="city"
              placeholder="Miasto "
            />
          </FormGroup>
          <FormGroup
            style={{ margin: "auto", width: "80%" }}
            controlId="postalCode"
          >
            <FormControl
              value={this.state.postalCode}
              onChange={this.handleChange}
              type="postalCode"
              name="postalCode"
              placeholder="Kod pocztowy"
            />
          </FormGroup>

          <Button
                    onClick={this.placeOrder}
                    disabled={this.disableSubmit()}
            bsStyle="buyButton"
            bsSize="large"
            style={{ marginTop: 18 }}
          >
            <Glyphicon glyph="shopping-cart" /> Kup
          </Button>
        </div>
      </div>
    );
  }
}

export default OrderPage;
