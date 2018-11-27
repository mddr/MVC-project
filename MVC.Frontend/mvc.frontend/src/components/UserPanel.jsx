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
import OrderService from "../services/OrderService";

class UserPanel extends Component {
  //todo dodać get i post state'a
  constructor(props) {
    super(props);
    this.state = {
      currentUserInfo: {
        id: 0,
        emailConfirmed: true,
        firstName: "",
        lastName: "",
        address: {
          id: 0,
          city: "",
          postalCode: "",
          street: "",
          houseNumber: ""
        },
        currency: "",
        prefersNetPrice: null,
        acceptsNewsletters: true,
        productsPerPage: 0
      },
      Orders: []
    };
    this.OrderService = new OrderService();
  }

  componentDidMount() {
    this.setState({ currentUserInfo: this.props.userInfo });
      this.OrderService.getUserOrders()
          .then(res => res.json())
          .then(data => {
              this.setState({
                  Orders: data
              })
          })
    }

    renderHistory() {
        if (this.state.Orders.length < 1) return;
        let items = [];
        this.state.Orders.map(order =>
            items.push(
                <div className="orderitems">
                    <h3>Zamówienie nr {this.state.Orders.indexOf(order) + 1}</h3>
                    {order.shoppingCart.map(item => (
                        <div className="orderitem">
                            <label>ID produktu: </label>
                            {item.productId} <br />
                            <label>Ilość porduktu: </label>
                            {item.productAmount}
                            <br />
                        </div>
                    ))}
                    <div className="totalprice">
                        <label>Całkowita cena: </label>
                        {order.totalPrice}
                        <br />
                    </div>
                    <div className="orderdate">
                        <label>Data dokonania zakupu: </label>
                        {order.createdAt.toString()}
                        <br />
                    </div>
                    <hr />
                </div>)
        );
        return items;
    }

  render() {
    let tempUser = { ...this.state.currentUserInfo };

    const regexPostalCode = () => {
      if (!tempUser.address) return "error";

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
                    defaultValue={this.state.currentUserInfo.firstName}
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
                    defaultValue={this.state.currentUserInfo.lastName}
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
                    defaultValue={
                      this.state.currentUserInfo.address
                        ? this.state.currentUserInfo.address.city
                        : ""
                    }
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
                    defaultValue={
                      this.state.currentUserInfo.address
                        ? this.state.currentUserInfo.address.postalCode
                        : ""
                    }
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
                    defaultValue={
                      this.state.currentUserInfo.address
                        ? this.state.currentUserInfo.address.street
                        : ""
                    }
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
                    defaultValue={
                      this.state.currentUserInfo.address
                        ? this.state.currentUserInfo.address.houseNumber
                        : ""
                    }
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
                    defaultValue={this.state.currentUserInfo.productsPerPage}
                    onChange={e => (tempUser.productsPerPage = e.target.value)}
                  />
                </Col>
              </FormGroup>

              <div
                className="checkboxradiowrapper"
                style={{
                  display: "flex",
                  direction: "row",
                  marginTop: "25em",
                  width: "100%",
                  justifyContent: "space-around"
                }}
              >
                <FormGroup>
                  <ControlLabel>Akceptujesz nasz newsletter</ControlLabel>
                  <Checkbox
                    defaultChecked={
                      this.state.currentUserInfo.acceptsNewsletters
                    }
                    onChange={e =>
                      (tempUser.acceptsNewsletters = e.target.checked)
                    }
                  />
                </FormGroup>
                <FormGroup bsClass="price_radio_group">
                  <ControlLabel>Preferowane wyświetlanie ceny</ControlLabel>
                  <Radio
                    name="radioGroup"
                    defaultChecked={this.state.currentUserInfo.prefersNetPrice}
                    onChange={e => (tempUser.prefersNetPrice = true)}
                  >
                    Netto
                  </Radio>
                  <Radio
                    name="radioGroup"
                    defaultChecked={!this.state.currentUserInfo.prefersNetPrice}
                    onChange={e => (tempUser.prefersNetPrice = false)}
                  >
                    Brutto
                  </Radio>
                </FormGroup>
              </div>
              <Button
                onClick={() => this.setState({ currentUserInfo: tempUser })}
              >
                Zatwierdź
              </Button>
            </Form>
          </Tab>
          <Tab eventKey={2} title="Historia zamówień">
            <p>Historia zamówień:</p>
            <hr />
                    {this.renderHistory()}
          </Tab>
        </Tabs>
      </div>
    );
  }
}

export default UserPanel;
