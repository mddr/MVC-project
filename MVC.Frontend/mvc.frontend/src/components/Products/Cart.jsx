import React, { Component } from "react";
import {
  OverlayTrigger,
  Popover,
  Glyphicon,
  Button,
  InputGroup,
  FormControl
} from "react-bootstrap";
import { Link } from "react-router-dom";

import "./Cart.css";
import "./ProductPage.css";
import CartService from "../../services/CartService";
import ProductService from "../../services/ProductService";

class Cart extends Component {
  constructor(props) {
    super(props);

    this.getTotalPrice = this.getTotalPrice.bind(this);
    this.renderItems = this.renderItems.bind(this);
    this.CartService = new CartService();
    this.ProductService = new ProductService();
  }

  getTotalPrice() {
    if (this.props.cartItems.length < 1) return 0;
    let price = 0;
    for (let i = 0; i < this.props.cartItems.length; i++) {
      price +=
        this.props.cartItems[i].product.pricePln *
          this.props.cartItems[i].productAmount -
        (this.props.cartItems[i].product.discount *
          this.props.cartItems[i].product.pricePln *
          this.props.cartItems[i].productAmount) /
          100;
    }
    return price;
  }

  addValue = id => {
    let cartElement = this.props.cartItems.find(elem => elem.productId === id);
    cartElement.productAmount++;
    this.CartService.updateItem(
      cartElement.productId,
      cartElement.productAmount
    ).then(() => {
      this.props.cartItemChanged(cartElement);
    });
  };

  subtractValue = id => {
    let cartElement = this.props.cartItems.find(elem => elem.productId === id);
    cartElement.productAmount--;
    this.CartService.updateItem(
      cartElement.productId,
      cartElement.productAmount
    ).then(() => {
      this.props.cartItemChanged(cartElement);
    });
  };

  renderItems() {
    if (this.props.cartItems.length < 1) return;
    // eslint-disable-next-line
    let items = [];
    // eslint-disable-next-line
    this.props.cartItems.map((item, i) => {
      items.push(
        <div className="item">
          <img
            src={
              "data:image/jpeg;base64," +
              this.props.cartItems[i].product.imageBase64
            }
            alt={this.props.cartItems[i].product.name}
            height="64"
            width="64"
          />
          <span className="namespan">
            {item.productAmount} x {this.props.cartItems[i].product.name}
          </span>
          <span style={{ width: "50px" }}>
            {Math.round(
              (this.props.cartItems[i].product.pricePln * item.productAmount -
                (this.props.cartItems[i].product.discount *
                  this.props.cartItems[i].product.pricePln *
                  this.props.cartItems[i].productAmount) /
                  100) *
                100
            ) / 100}
            zł
          </span>
          <div className="cart-amount-picker">
            <InputGroup style={{ margin: "auto" }}>
              <InputGroup.Button>
                <Button onClick={() => this.addValue(item.productId)}>
                  <Glyphicon glyph="chevron-up" />
                </Button>
              </InputGroup.Button>
              <FormControl
                value={item.productAmount}
                style={{ width: "50px", textAlign: "center" }}
              />
              <InputGroup.Button>
                <Button
                  onClick={() => this.subtractValue(item.productId)}
                  disabled={item.productAmount < 2}
                >
                  <Glyphicon glyph="chevron-down" />
                </Button>
              </InputGroup.Button>
            </InputGroup>
          </div>
          <a href="">
            <Glyphicon
              glyph="trash"
              style={{ color: "red" }}
              onClick={() => this.CartService.removeItem(item.productId)}
            />
          </a>
        </div>
      );
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
            <div>{this.getTotalPrice()}zł</div>
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
