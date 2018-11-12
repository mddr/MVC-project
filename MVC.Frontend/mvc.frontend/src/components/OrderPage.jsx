import React, { Component } from "react";
import { Button, Glyphicon, FormGroup, FormControl } from "react-bootstrap";
import "./OrderPage.css";
import "./Home.css";
import "./ProductPage.css";
import CartService from "../services/CartService";
import ProductService from "../services/ProductService";

class OrderPage extends Component {

    constructor(props) {
        super(props);
        this.state = {
            totalPrice: 0,
        };
        this.getTotalPrice = this.getTotalPrice.bind(this);
        this.placeOrder = this.placeOrder.bind(this);
        this.rendercartItems = this.rendercartItems.bind(this);
        this.CartService = new CartService();
        this.ProductService = new ProductService();
    }

    componentDidUpdate(prevProps) {
        if (this.props.cartItems !== prevProps.cartItems) {
            this.setState({
                totalPrice: this.getTotalPrice(),
            });
        }
    }

    getTotalPrice() {
        let output = 0;
        if (this.props.cartItems.length < 1) return 0;
        for (let i = 0; i < this.props.cartItems.length; i++) {
            this.ProductService.getProduct(this.props.cartItems[i].productId).then(res => res.json()).then(res => {
                output +=
                    Math.round(this.props.cartItems[i].productAmount * res.price * 100) / 100;
            });
        }
        return output;
    }
        
    placeOrder() {
        let body = "";
        const obj = {
            userId: this.state.id,
            cartId: this.state.name,
            totalPrice: this.state.pricePln,
        };

        body = JSON.stringify(obj);

        this.props.Auth.fetch(`${this.props.Auth.domain}/order/add`, {
            method: 'post',
            body
        });
    }

    rendercartItems() {
        if (this.props.cartItems == null) return;
        let items = []
        this.props.cartItems.map(item => {
            this.ProductService.getProduct(item.productId).then(res => res.json()).then(res => {
                items.push(<div className="item">
                    <img src={"data:image/jpeg;base64," + res.imageBase64} alt={res.name} height="64" width="64" />
                    <span className="namespan">
                        {item.productAmount} x {res.name}
                    </span>
                    <span>
                        {Math.round(res.pricePln * item.productAmount * 100) / 100}
                        zł
                      </span>
                    <span />
                </div>)
            })
        });
        return items;
    }

  render() {
    return (
      <div className="orderpage">
        <div className="banner">
          <div className="banner_img" />
        </div>
        <div className="summary">
          <p>Zamówienie:</p>
            <hr style={{ width: "95%" }} />
                {this.rendercartItems()}
          <hr style={{ width: "95%" }} />
          <div className="ordersum">
                    <div className="ordersumtext">Suma: </div>
                    <div>{this.state.totalPrice}zł</div>
          </div>
        </div>
        <div className="shippinginfo">
          <p>Dane wysyłki:</p>
          <hr style={{ width: "95%" }} />
          <FormGroup style={{ margin: "auto", width: "80%" }}>
            <FormControl placeholder="Imię i nazwisko" />
            <FormControl placeholder="Ulica, numer domu i numer mieszkania" />
            <FormControl placeholder="Miasto i kod pocztowy" />
            <Button
              bsStyle="buyButton"
              bsSize="large"
              style={{ marginTop: 18 }}
            >
              <Glyphicon glyph="shopping-cart" /> Kup
            </Button>
          </FormGroup>
        </div>
      </div>
    );
  }
}

export default OrderPage;
