import "./UserPanel.css";
import React, { Component } from "react";
import {
  Col,
  Tabs,
  Tab,
  Form,
  FormGroup,
  FormControl,
  ControlLabel,
  Checkbox,
  Radio,
  Button
} from "react-bootstrap";

class UserPanel extends Component {
  //todo dodać get i post state'a
  state = {
    currentUser: {
      firstName: "Czarek",
      lastName: "Szmurło",
      email: "sznurek05@gmail.com",
      prefersNetPrice: true,
      acceptsNewsletter: false,
      productsPerPage: 10,
      address: {
        city: "Białystok",
        postalCode: "15-333",
        street: "Zwierzyniecka",
        houseNumber: 14
      }
    },
    Orders: [
      {
        shoppingCart: [
          {
            productId: "e1f22019-c191-49df-95f4-e110f986ee39",
            productAmount: 2
          },
          {
            productId: "afcc9a77-9cd2-475e-9b4a-f21d5e5b16a0",
            productAmount: 1
          }
        ],
        totalPrice: 850,
        createdAt: new Date()
      },
      {
        shoppingCart: [
          {
            productId: "e1f22019-c191-49df-95f4-e110f986ee39",
            productAmount: 4
          }
        ],
        totalPrice: 1200,
        createdAt: new Date()
      }
    ]
  };

  render() {
    let tempUser = { ...this.state.currentUser };

    const regexPostalCode = () => {
      if (/\d{2}-\d{3}/.test(tempUser.address.postalCode)) return null;
      else return "error";
    };

    return (
      <div className="userpanel">
        <Tabs defaultActiveKey={1}>
          <Tab eventKey={1} title="Dane użytkownika">
            <Form>
              <FormGroup>
                <Col componentClass={ControlLabel} sm={2}>
                  Imię
                </Col>
                <Col sm={10}>
                  <FormControl
                    type="text"
                    defaultValue={this.state.currentUser.firstName}
                    onChange={e => (tempUser.firstName = e.target.value)}
                  />
                </Col>
              </FormGroup>
              <FormGroup>
                <Col componentClass={ControlLabel} sm={2}>
                  Nazwisko
                </Col>
                <Col sm={10}>
                  <FormControl
                    type="text"
                    defaultValue={this.state.currentUser.lastName}
                    onChange={e => (tempUser.lastName = e.target.value)}
                  />
                </Col>
              </FormGroup>
              <FormGroup>
                <Col componentClass={ControlLabel} sm={2}>
                  Miasto
                </Col>
                <Col sm={10}>
                  <FormControl
                    type="text"
                    defaultValue={this.state.currentUser.address.city}
                    onChange={e => (tempUser.address.city = e.target.value)}
                  />
                </Col>
              </FormGroup>
              <FormGroup validationState={regexPostalCode()}>
                <Col componentClass={ControlLabel} sm={2}>
                  Kod pocztowy
                </Col>
                <Col sm={10}>
                  <FormControl
                    type="text"
                    onChange={e => {
                      tempUser.address.postalCode = e.target.value;
                    }}
                    defaultValue={this.state.currentUser.address.postalCode}
                  />
                </Col>
              </FormGroup>
              <FormGroup>
                <Col componentClass={ControlLabel} sm={2}>
                  Ulica
                </Col>
                <Col sm={10}>
                  <FormControl
                    type="text"
                    defaultValue={this.state.currentUser.address.street}
                    onChange={e => (tempUser.address.street = e.target.value)}
                  />
                </Col>
              </FormGroup>
              <FormGroup>
                <Col componentClass={ControlLabel} sm={2}>
                  Numer domu
                </Col>
                <Col sm={10}>
                  <FormControl
                    type="number"
                    defaultValue={this.state.currentUser.address.houseNumber}
                    onChange={e =>
                      (tempUser.address.houseNumber = e.target.value)
                    }
                  />
                </Col>
              </FormGroup>
              <FormGroup>
                <Col componentClass={ControlLabel} sm={2}>
                  Ilość produktów na stronę
                </Col>
                <Col sm={10}>
                  <FormControl
                    type="number"
                    defaultValue={this.state.currentUser.productsPerPage}
                    onChange={e => (tempUser.productsPerPage = e.target.value)}
                  />
                </Col>
              </FormGroup>
              <FormGroup>
                <Col componentClass={ControlLabel} sm={2}>
                  Akceptujesz nasz newsletter
                </Col>
                <Col sm={10}>
                  <Checkbox
                    defaultChecked={this.state.currentUser.acceptsNewsletter}
                    onChange={e =>
                      (tempUser.acceptsNewsletter = e.target.checked)
                    }
                  />
                </Col>
              </FormGroup>
              <FormGroup>
                <ControlLabel>Preferowane wyświetlanie ceny</ControlLabel>
                <Radio
                  name="radioGroup"
                  defaultChecked={this.state.currentUser.prefersNetPrice}
                  onChange={e => (tempUser.prefersNetPrice = true)}
                >
                  Netto
                </Radio>
                <Radio
                  name="radioGroup"
                  defaultChecked={!this.state.currentUser.prefersNetPrice}
                  onChange={e => (tempUser.prefersNetPrice = false)}
                >
                  Brutto
                </Radio>
              </FormGroup>
              <Button onClick={() => this.setState({ currentUser: tempUser })}>
                Zatwierdź
              </Button>
            </Form>
          </Tab>
          <Tab eventKey={2} title="Historia zamówień">
            <p>Historia zamówień:</p>
            <hr />
            {this.state.Orders.map(order => (
              <div className="orderitems">
                {order.shoppingCart.map(item => (
                  <div className="orderitem">
                    {item.productId} {item.productAmount}
                  </div>
                ))}
                <div className="totalprice">{order.totalPrice}</div>
                <div className="orderdate">{order.createdAt.toString()}</div>
                <hr />
              </div>
            ))}
          </Tab>
        </Tabs>
      </div>
    );
  }
}

export default UserPanel;