import React, { Component } from "react";
import { OverlayTrigger, Popover, Glyphicon, Button } from "react-bootstrap";
import { Link } from "react-router-dom";

import "./Cart.css";
import "./ProductPage.css";
import CartService from "../services/CartService";
import ProductService from "../services/ProductService";

class Cart extends Component {
  constructor(props) {
    super(props);
    this.state = {
      totalPrice: 0
    };
    this.getTotalPrice = this.getTotalPrice.bind(this);
    this.renderItems = this.renderItems.bind(this);
    this.CartService = new CartService();
    this.ProductService = new ProductService();
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

  renderItems() {
    if (this.props.cartItems.length < 1) return;
    let items = [];
    this.props.cartItems.map(item => {
      this.ProductService.getProduct(item.productId)
        .then(res => res.json())
        .then(res => {
          items.push(
            <div className="item">
              <img
                src={"data:image/jpeg;base64," + res.imageBase64}
                alt={res.name}
                height="64"
                width="64"
              />
              <span className="namespan">
                {item.productAmount} x {res.name}
              </span>
              <span>
                {Math.round(res.pricePln * item.productAmount * 100) / 100}
                zł
              </span>
            </div>
          );
        });
    });

    return items;
  }

  render() {
    const { cartItems } = this.props;
    const popoverClickRootClose =
      cartItems.length > 0 ? (
        <Popover
          id="popover-trigger-click-root-close"
          title="Koszyk"
          positionTop="100"
          positionLeft="100"
          arrowOffsetTop="100"
          arrowOffsetLeft="100"
        >
          {this.renderItems()}
          <hr />
          <div className="cartsummary">
            <Link to="/order">
              <Button bsStyle="buyButton" bsSize="large">
                <Glyphicon glyph="ok" /> Złóż zamówienie
              </Button>
            </Link>
            <div className="sumatext">Suma: </div>
            <div>{this.state.totalPrice}zł</div>
          </div>
        </Popover>
      ) : (
        <Popover
          id="popover-trigger-click-root-close"
          title="Koszyk"
          arrowOffsetTop="80"
        >
          <span style={{ color: "gray", margin: "2px", fontStyle: "italic" }}>
            Koszyk jest pusty
          </span>
        </Popover>
      );
    return (
      <div className="cart">
        <OverlayTrigger
          trigger="click"
          rootClose
          placement="bottom"
          overlay={popoverClickRootClose}
        >
          <button style={{ background: "none", border: "none", padding: 0 }}>
            <i className="fa fa-shopping-cart cart" style={{ fontSize: 22 }} />
            {cartItems.length > 0 && (
              <div className="basketitems">{cartItems.length}</div>
            )}
          </button>
        </OverlayTrigger>
      </div>
    );
  }
}

export default Cart;
