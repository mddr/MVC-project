import React, { Component } from "react";
import { Button, Glyphicon, FormGroup, FormControl } from "react-bootstrap";

import "./OrderPage.css";
import "./Home.css";
import "./ProductPage.css";
import CartService from "../services/CartService";
import ProductService from "../services/ProductService";
import AddressService from "../services/AddressService";
import OrderService from "../services/OrderService";

class OrderPage extends Component {

    constructor(props) {
        super(props);
        this.state = {
            totalPrice: 0,
            street: "",
            houseNumber: "",
            postalCode: "",
            city: "",
        };
        this.getTotalPrice = this.getTotalPrice.bind(this);
        this.handleChange = this.handleChange.bind(this);
        this.placeOrder = this.placeOrder.bind(this);
        this.renderItems = this.renderItems.bind(this);
        this.CartService = new CartService();
        this.ProductService = new ProductService();
        this.AddressService = new AddressService();
        this.OrderService = new OrderService();
    }

  componentDidUpdate(prevProps) {
    if (this.props.cartItems !== prevProps.cartItems) {
      this.getTotalPrice();
    }
  }

  getTotalPrice() {
    if (this.props.cartItems.length < 1) return 0;
    for (let i = 0; i < this.props.cartItems.length; i++) {
      this.ProductService.getProduct(this.props.cartItems[i].productId)
        .then(res => res.json())
        .then(res => {
          this.setState({
            totalPrice:
              this.state.totalPrice +
              Math.round(
                this.props.cartItems[i].productAmount * res.pricePln * 100
              ) /
                100
          });
        });
    }
  }

    placeOrder() {
        this.AddressService.addAddres(
            this.state.city,
            this.state.postalCode,
            this.state.street,
            this.state.houseNumber
        ).then(() => {
                this.OrderService.order();
            }).then(() => window.location.reload());
    }
	
	handleChange = event => {
        this.setState({
            [event.target.id]: event.target.value
        });
    };
  renderItems() {
      if (!this.props.cartItems ||
          !this.props.cartItemsInfo) return;
        let items = [];
      this.props.cartItems.map((item, i) => {
          console.log(this.props.cartItemsInfo[i]);
        items.push(
            <div className="item">
              <img
                    src={"data:image/jpeg;base64," + this.props.cartItemsInfo[i].imageBase64}
                    alt={this.props.cartItemsInfo[i].name}
                height="64"
                width="64"
              />
              <span className="namespan">
                {item.productAmount} x {this.props.cartItemsInfo[i].name}
              </span>
              <span>
                {Math.round(this.props.cartItemsInfo[i].pricePln * item.productAmount * 100) / 100}
                zł
              </span>
              <span />
            </div>
          );
    });

    return items;
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
            <div>{this.state.totalPrice}zł</div>
          </div>
        </div>
        <div className="shippinginfo">
          <p>Dane wysyłki:</p>
                <hr style={{ width: "95%" }} />
                    <FormGroup style={{ margin: "auto", width: "80%" }} controlId="street">
                        <FormControl
                                value={this.state.street}
                                onChange={this.handleChange}
                                type="street"
                                name="steet"
                                placeholder="Ulica" />
                    </FormGroup>
                    <FormGroup style={{ margin: "auto", width: "80%" }} controlId="houseNumber">
                            <FormControl
                                value={this.state.houseNumber}
                                onChange={this.handleChange}
                                type="houseNumber"
                            name="houseNumber" placeholder="Numer domu" />
                    </FormGroup>
                    <FormGroup style={{ margin: "auto", width: "80%" }} controlId="city">
                            <FormControl
                                value={this.state.city}
                                onChange={this.handleChange}
                                type="city"
                            name="city" placeholder="Miasto " />
                    </FormGroup>
                    <FormGroup style={{ margin: "auto", width: "80%" }} controlId="postalCode">
                            <FormControl
                                value={this.state.postalCode}
                                onChange={this.handleChange}
                                type="postalCode"
                            name="postalCode" placeholder="Kod pocztowy" />
                    </FormGroup>

                <Button
                    onClick={this.placeOrder}
                      bsStyle="buyButton"
                      bsSize="large"
                      style={{ marginTop: 18 }}>
                    <Glyphicon glyph="shopping-cart" /> Kup
                    </Button>
        </div>
      </div>
    );
  }
}

export default OrderPage;
