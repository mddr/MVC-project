import React, { Component } from "react";
import { OverlayTrigger, Popover, Glyphicon, Button } from "react-bootstrap";
import "./Cart.css";
import "./ProductPage.css";

class Cart extends Component {
  render() {
    const { Items } = this.props;
    const priceSum = () => {
      let output = 0;
      for (let i = 0; i < Items.length; i++) {
        output +=
          Math.round(Items[i].count * Items[i].product.price * 100) / 100;
      }
      return output;
    };
    const popoverClickRootClose =
      Items.length > 0 ? (
        <Popover
          id="popover-trigger-click-root-close"
          title="Koszyk"
          arrowOffsetTop="80"
        >
          {Items.map(item => (
            <div className="item">
              <div className="orangebox" />
              <span className="namespan">
                {item.count} x {item.product.name}
              </span>
              <span>
                {Math.round(item.product.price * item.count * 100) / 100}
                zł
              </span>
              <span />
            </div>
          ))}
          <hr />
          <div className="cartsummary">
            <Button bsStyle="buyButton" onClick="" bsSize="medium">
              <Glyphicon glyph="ok" /> Złóż zamówienie
            </Button>
            <div className="sumatext">Suma: </div>
            <div>{priceSum()}zł</div>
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
            <i class="fa fa-shopping-cart cart" style={{ fontSize: 22 }} />
            {Items.length > 0 && (
              <div className="basketitems">{Items.length}</div>
            )}
          </button>
        </OverlayTrigger>
      </div>
    );
  }
}

export default Cart;
