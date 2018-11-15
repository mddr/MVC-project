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
import CartService from "../services/CartService";
import ProductService from "../services/ProductService";

class Cart extends Component {
  constructor(props) {
    super(props);

    this.getTotalPrice = this.getTotalPrice.bind(this);
    this.renderItems = this.renderItems.bind(this);
    this.CartService = new CartService();
    this.ProductService = new ProductService();
  }

  getTotalPrice() {
    if (this.props.cartItemsInfo.length < 1) return 0;
    let price = 0;
    for (let i = 0; i < this.props.cartItemsInfo.length; i++) {
      price +=
        this.props.cartItemsInfo[i].pricePln *
        this.props.cartItems[i].productAmount;
    }
    return price;
  }
  addValue = id => {
    let cartElement = this.props.cartItems.find(elem => elem.productId === id);
    this.CartService.updateItem(
      cartElement.productId,
      cartElement.productAmount + 1
    );
  };

  subtractValue = id => {
    let cartElement = this.props.cartItems.find(elem => elem.productId === id);
    this.CartService.updateItem(
      cartElement.productId,
      cartElement.productAmount - 1
    );
  };

  renderItems() {
    if (this.props.cartItems.length < 1) return;
    if (this.props.cartItemsInfo.length < 1) return;
    // eslint-disable-next-line
    if (this.props.cartItemsInfo.length != this.props.cartItems.length) return;
    let items = [];
    // eslint-disable-next-line
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
          <span style={{ width: "50px" }}>
            {Math.round(
              this.props.cartItemsInfo[i].pricePln * item.productAmount * 100
            ) / 100}
            zł
          </span>
          <div className="cart-amount-picker">
            <InputGroup style={{ margin: "auto" }}>
              <InputGroup.Button>
                <Button onClick={this.addValue(item.productId)}>
                  <Glyphicon glyph="chevron-up" />
                </Button>
              </InputGroup.Button>
              <FormControl
                value={item.productAmount}
                style={{ width: "50px", textAlign: "center" }}
                /* onChange={(x: React.FormEvent<FormControl & HTMLInputElement>) => {
                  this.setState({
                    count: isNaN(parseInt(x.currentTarget.value))
                      ? 1
                      : parseInt(x.currentTarget.value)
                  });
                }
              }*/
              />
              <InputGroup.Button>
                <Button onClick={this.subtractValue(item.productId)}>
                  <Glyphicon glyph="chevron-down" />
                </Button>
              </InputGroup.Button>
            </InputGroup>
          </div>
          <a href="">
            <Glyphicon glyph="trash" style={{ color: "red" }} />
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
