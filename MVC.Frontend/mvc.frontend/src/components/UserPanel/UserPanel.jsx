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
import AddressService from "../../services/AddressService";
import UserService from "../../services/UserService";
import AuthService from '../../services/AuthService';

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
        productsPerPage: ""
      },
      tempUser: {
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
        productsPerPage: ""
      },
      Orders: []
    };
    this.AddressService = new AddressService();
    this.OrderService = new OrderService();
		this.UserService = new UserService();
		this.AuthService = new AuthService();
    this.handleChangePasswordSubmit = this.handleChangePasswordSubmit.bind(this)
  }

  componentDidMount() {
    this.UserService.getUserInfo()
      .then(res => res.json())
      .then(data => {
        this.setState({
          currentUserInfo: { ...data },
          tempUser: { ...data }
        });
      });
    this.OrderService.getUserOrders()
      .then(res => res.json())
      .then(data => {
        this.setState({
          Orders: data
        });
      }
    ).catch(error => alert("Przypominamy o potrzebie potwierdzenia adresu email"));
  }

	validateChangePasswordForm() {
    if (!(this.state.password.length > 0)) return false;
    if (this.state.password !== this.state.password2) return false;
    return true;
  }

  validateUpdateDataForm(tempUser) {
    if (tempUser.firstName.length < 0) return false;
    if (tempUser.lastName.length < 0) return false;
    if (tempUser.address.city.length < 0) return false;
    if (tempUser.address.postalCode.length < 0) return false;
    if (tempUser.address.street.length < 0) return false;
    if (tempUser.address.houseNumber < 0) return false;
    if (tempUser.productsPerPage < 1) return false;
    return true;
  }

	handleChangePasswordSubmit() {
		this.AuthService.changepassword(this.state.curr_password, this.state.password);
	}

  handleChangeUserData(tempUser) {
    this.setState({ currentUserInfo: tempUser });
    this.UserService.update(tempUser.id, tempUser.firstName, tempUser.lastName,
      tempUser.email, tempUser.currency, tempUser.emailConfirmed, tempUser.prefersNetPrice,
      tempUser.acceptsNewsletters, tempUser.productsPerPage);
    this.AddressService.setUsersAddres(tempUser.address.city, tempUser.address.postalCode,
      tempUser.address.street, tempUser.address.houseNumber, tempUser.id);
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
              onChange={e => {
                let user = this.state.tempUser;
                user.firstName = e.target.value;
                this.setState({ tempUser: user });
              }}
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
              onChange={e => {
                let user = this.state.tempUser;
                user.lastName = e.target.value;
                this.setState({ tempUser: user });
              }}
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
              onChange={e => {
                let user = this.state.tempUser;
                user.address.city = e.target.value;
                this.setState({ tempUser: user });
              }}
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
                let user = this.state.tempUser;
                user.address.postalCode = e.target.value;
                this.setState({ tempUser: user });
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
              onChange={e => {
                let user = this.state.tempUser;
                user.address.street = e.target.value;
                this.setState({ tempUser: user });
              }}
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
              onChange={e => {
                let user = this.state.tempUser;
                user.address.houseNumber = e.target.value;
                this.setState({ tempUser: user });
              }}
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
              onChange={e => {
                let user = this.state.tempUser;
                user.productsPerPage = e.target.value;
                this.setState({ tempUser: user });
              }}
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
              onChange={e => {
                let user = this.state.tempUser;
                user.acceptsNewsletters = e.target.checked;
                this.setState({ tempUser: user });
              }}
            />
          </FormGroup>
          <FormGroup bsClass="price_radio_group">
            <ControlLabel>Preferowane wyświetlanie ceny</ControlLabel>
            <Radio
              name="radioGroup"
              defaultChecked={this.state.currentUserInfo.prefersNetPrice}
              onChange={e => {
                let user = this.state.tempUser;
                user.prefersNetPrice = true;
                this.setState({ tempUser: user });
              }}
            >
              Netto
            </Radio>
            <Radio
              name="radioGroup"
              defaultChecked={!this.state.currentUserInfo.prefersNetPrice}
              onChange={e => {
                let user = this.state.tempUser;
                user.prefersNetPrice = false;
                this.setState({ tempUser: user });
              }}
            >
              Brutto
            </Radio>
          </FormGroup>
        </div>
        <Button
          disabled={!this.validateUpdateDataForm(this.state.tempUser)}
          onClick={() => this.handleChangeUserData(this.state.tempUser)}>
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
          <form>
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
              disabled={!this.validateChangePasswordForm()}
              onClick={this.handleChangePasswordSubmit}
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
