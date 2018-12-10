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
  Button,
  Panel
} from "react-bootstrap";
import OrderService from "../../services/OrderService";
import UserService from "../../services/UserService";

class UserPanel extends Component {
  constructor(props) {
    super(props);
    this.state = {
      password: "",
      password2: "",
      curr_password: "",
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
    this.UserService = new UserService();
  }

  componentDidMount() {
    this.UserService.getUserInfo()
      .then(res => res.json())
      .then(data => {
        this.setState({ currentUserInfo: { ...data } });
      });
    this.OrderService.getUserOrders()
      .then(res => res.json())
      .then(data => {
        this.setState({
          Orders: data
        });
      });
  }
  validateForm() {
    if (!(this.state.password.length > 0)) return false;
    if (this.state.password !== this.state.password2) return false;
    return true;
  }
  handleChange = event => {
    this.setState({
      [event.target.id]: event.target.value
    });
  };
  render() {
    return (
      <div className="userpanel">
        <Tabs defaultActiveKey={1}>
          <Tab eventKey={1} title="Dane użytkownika">
            {this.renderUserData()}
          </Tab>
          <Tab eventKey={2} title="Historia zamówień">
            <p>Historia zamówień:</p>
            <hr />
            {this.renderHistory()}
          </Tab>
          <Tab eventKey={3} title="Zmiana hasła">
            {this.renderPassChange()}
          </Tab>
        </Tabs>
      </div>
    );
  }

  renderUserData() {
    let tempUser = { ...this.state.currentUserInfo };
    return (
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
        <FormGroup>
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
              onChange={e => (tempUser.address.houseNumber = e.target.value)}
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
              defaultChecked={this.state.currentUserInfo.acceptsNewsletters}
              onChange={e => (tempUser.acceptsNewsletters = e.target.checked)}
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
        <Button onClick={() => this.setState({ currentUserInfo: tempUser })}>
          Zatwierdź
        </Button>
      </Form>
    );
  }

  renderHistory() {
    if (this.state.Orders.length < 1) return;
    let items = [];
    this.state.Orders.map(order => items.push(this.renderHistoryItem(order)));
    return items;
  }

  renderHistoryItem(order) {
    return (
      <div className="orderitems">
        <h3>Zamówienie nr {this.state.Orders.indexOf(order) + 1}</h3>
        {order.shoppingCart.map(item => (
          <div className="orderitem">
            <label>ID produktu: </label>
            {item.productId} <br />
            <label>Ilość produktu: </label>
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
      </div>
    );
  }
  renderPassChange() {
    return (
      <div className="pass_change_box">
        <Panel>
          <Panel.Heading />
          <form onSubmit={this.handleSubmit}>
            <FormGroup controlId="curr_password">
              <FormControl
                value={this.state.curr_password}
                onChange={this.handleChange}
                type="password"
                name="curr_password"
                placeholder="Podaj aktualne hasło"
              />
            </FormGroup>
            <FormGroup controlId="password">
              <FormControl
                value={this.state.password}
                onChange={this.handleChange}
                type="password"
                name="password"
                placeholder="Podaj nowe hasło"
              />
            </FormGroup>
            <FormGroup controlId="password2">
              <FormControl
                value={this.state.password2}
                onChange={this.handleChange}
                type="password"
                name="password2"
                placeholder="Wpisz nowe hasło jeszcze raz"
              />
            </FormGroup>
            <Button
              disabled={!this.validateForm()}
              type="submit"
              className="btn btn-success"
              block
            >
              Zmień
            </Button>
          </form>
        </Panel>
      </div>
    );
  }
}

export default UserPanel;
