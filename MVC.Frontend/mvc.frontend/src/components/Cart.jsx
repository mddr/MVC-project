import React, { Component } from "react";
import { OverlayTrigger, Popover, Glyphicon, Button } from "react-bootstrap";
import "./Cart.css";
import "./ProductPage.css";

class Cart extends Component {

    constructor(props) {
        super(props);
        this.state = {
            totalPrice: this.getTotalPrice(),
        };
        this.getTotalPrice = this.getTotalPrice.bind(this);
        this.handleSubmit = this.handleSubmit.bind(this);
    }

    componentDidUpdate(prevProps) {
        if (this.props !== prevProps) {
            this.setState({
                totalPrice: this.getTotalPrice(),
            });
        } 
    }

    getTotalPrice(Items) {
        let output = 0;
        for (let i = 0; i < this.props.Items.length; i++) {
            output +=
                Math.round(this.props.Items[i].count * this.props.Items[i].product.price * 100) / 100;
        }
        return output;
    }

    handleSubmit() {
        let body = "";
        const obj = {
            userId: this.state.id,
            //TODO vv
            cartId: this.state.name,
            //TODO ^^
            totalPrice: this.state.totalPrice,
        };

        body = JSON.stringify(obj);

        this.props.Auth.fetch(`${this.props.Auth.domain}/order/add`, {
            method: 'post',
            body
        });
    }

  render() {
    const { Items } = this.props;

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
            <Button bsStyle="buyButton" onClick="{handleSubmit}" bsSize="medium">
              <Glyphicon glyph="ok" /> Złóż zamówienie
            </Button>
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
