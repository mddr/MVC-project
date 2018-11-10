import React, { Component } from "react";
import { OverlayTrigger, Popover, Glyphicon, Button } from "react-bootstrap";
import "./Cart.css";
import "./ProductPage.css";

class Cart extends Component {
  state = {
    Items: [
      {
        product: {
          imgpath: "nadal nie wiem jak to działa",
          name: "Nazwa produktu 1",
          price: 49.99
        },
        count: 2
      },

      {
        product: {
          imgpath: "nadal nie wiem jak to działa",
          name: "Nazwa produktu 2",
          price: 24.99
        },
        count: 1
      }
    ]
  };

  render() {
    const priceSum = () => {
      let output = 0;
      for (let i = 0; i < this.state.Items.length; i++) {
        output +=
          Math.round(
            this.state.Items[i].count * this.state.Items[i].product.price * 100
          ) / 100;
      }
      return output;
    };
    const popoverClickRootClose =
      this.state.Items.length > 0 ? (
        <Popover
          id="popover-trigger-click-root-close"
          title="Koszyk"
          arrowOffsetTop="80"
        >
          {this.state.Items.map(item => (
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
            {this.state.Items.length > 0 && (
              <div className="basketitems">{this.state.Items.length}</div>
            )}
          </button>
        </OverlayTrigger>
      </div>
    );
  }
}

export default Cart;
