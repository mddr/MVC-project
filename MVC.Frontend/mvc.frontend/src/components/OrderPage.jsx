import React, { Component } from "react";
import { Button, Glyphicon, FormGroup, FormControl } from "react-bootstrap";
import "./OrderPage.css";
import "./Home.css";
import "./ProductPage.css";

class OrderPage extends Component {
  state = {};
  render() {
    const OrderItems = [
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
    ];
    const priceSum = () => {
      let output = 0;
      for (let i = 0; i < OrderItems.length; i++) {
        output +=
          Math.round(OrderItems[i].count * OrderItems[i].product.price * 100) /
          100;
      }
      return output;
    };
    return (
      <div className="orderpage">
        <div className="banner">
          <div className="banner_img" />
        </div>
        <div className="summary">
          <p>Zamówienie:</p>
          <hr style={{ width: "95%" }} />
          {OrderItems.map(item => (
            <div className="item" style={{ marginLeft: "2em", width: "90%" }}>
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
          <hr style={{ width: "95%" }} />
          <div className="ordersum">
            <div className="ordersumtext">Suma: </div>
            <div>{priceSum()}zł</div>
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
