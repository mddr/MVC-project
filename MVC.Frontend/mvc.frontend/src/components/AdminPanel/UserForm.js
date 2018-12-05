import * as React from "react";
import {
	Button,
	DropdownButton,
	FormControl,
	FormGroup,
	MenuItem,
	Modal,
	Checkbox,
} from "react-bootstrap";
import AddressService from "../../services/AddressService";

export default class ProductForm extends React.Component {
	constructor(props) {
		super(props);
		this.state = {
			id: "",
			firstName: "",
			lastName: "",
			email: "",
			currency: "",
			emailConfirmed: false,
			prefersNetPrice: false,
			acceptsNewsletters: false,
			productsPerPage: -1,

			city: "",
			postalCode: "",
			street: "",
			houseNumber: "",
		};
		this.handleUserSubmit = this.handleUserSubmit.bind(this);
		this.handleAddrSubmit = this.handleAddrSubmit.bind(this);
		this.handleChange = this.handleChange.bind(this);
		this.validateUserForm = this.validateUserForm.bind(this);
		this.renderMenuItems = this.renderMenuItems.bind(this);
		this.AddressService = new AddressService();
	}

	validateUserForm() {
		if (!(
			this.state.firstName.length > 0 &&
			this.state.lastName.length > 0 &&
			this.state.currency.length > 0 &&
			this.state.email.length > 0 &&
			this.state.productsPerPage > 0 
			)) return false;
		return true;
	}
	validateAddrForm() {
		if (!(
			this.state.city.length > 0 &&
			this.state.postalCode.length > 0 &&
			this.state.street.length > 0 &&
			this.state.houseNumber.length > 0
		)) return false;
		return true;
	}

	handleChange = event => {
		this.setState({
			[event.target.id]: event.target.value
		});
	};

	componentDidMount() {
		if (this.props.modelProps) {
			if(this.props.modelProps.address)
				this.setState({
					...this.props.modelProps,
					addrId: this.props.modelProps.address.id,
					city: this.props.modelProps.address.city,
					postalCode: this.props.modelProps.address.postalCode,
					street: this.props.modelProps.address.street,
					houseNumber: this.props.modelProps.address.houseNumber,
				});
			else
			this.setState({
				...this.props.modelProps,
			});
		}
	}

	renderMenuItems() {
		const items = [];
		const currencies = [
			"PLN",
			"EUR",
			"USD"
		];
		for (let i = 0; i < this.props.categories.length; i++) {
			items.push(
				<MenuItem
					key={i}
					eventKey={i}
					onClick={() =>
						this.setState({
							currency: currencies[i],
						})
					}
				>
					{currencies[i]}
				</MenuItem>
			);
		}
		return items;
	}

	handleUserSubmit(event) {
		event.preventDefault();
		let body = "";
		const user = {
			id: this.state.id,
			firstName: this.state.firstName,
			lastName: this.state.lastName,
			email: this.state.email,
			currency: this.state.currency,
			emailConfirmed: this.state.emailConfirmed,
			prefersNetPrice: this.state.prefersNetPrice,
			acceptsNewsletters: this.state.acceptsNewsletters,
			productsPerPage: this.state.productsPerPage,
		};

		body = JSON.stringify(user);

		this.props.Auth.fetch(
			`${this.props.Auth.domain}/${this.props.apiUrl.singular}/${
			this.props.apiAction
			}`,
			{
				method: "post",
				body
			}
		).then(() => {
			this.props.updateData(user);
		});
	}

	handleAddrSubmit(event) {
		event.preventDefault();
		const addr = {
			city: this.state.city,
			postalCode: this.state.postalCode,
			street: this.state.street,
			houseNumber: this.state.houseNumber,
		};

		if (this.addrChanged(this.props.modelProps.address, addr)) {
			this.AddressService.setUsersAddres(addr.city, addr.postalCode, addr.street, addr.houseNumber, this.state.id)
				.then(() => {
					this.props.updateData(addr);
				});
		}
	}

	addrChanged(oldAddr, newAddr) {
		if (oldAddr === newAddr) return false;
		if (oldAddr === null && newAddr !== null) return true;
		if (oldAddr.city !== newAddr.city) return true;
		if (oldAddr.postalCode !== newAddr.postalCode) return true;
		if (oldAddr.street !== newAddr.street) return true;
		if (oldAddr.houseNumber !== newAddr.houseNumber) return true;
		return false;
	}

	render() {
		return (
			<Modal show={this.props.showForm}>
				<Modal.Header>
					<Modal.Title>{this.props.title}</Modal.Title>
				</Modal.Header>
				<form onSubmit={this.handleUserSubmit} id="userForm">
					{this.renderUserFields()}
				</form>
				<Modal.Footer>
					<Button
						bsStyle="primary"
						type="submit"
						form="userForm"
						disabled={!this.validateUserForm()}
						onClick={this.props.hideForm}
					>
						Zatwierdü
          </Button>
					<Button onClick={this.props.hideForm}>Anuluj</Button>
				</Modal.Footer>
				<form onSubmit={this.handleAddrSubmit} id="addrForm">
					{this.renderAddressFields()}
				</form>
				<Modal.Footer>
					<Button
						bsStyle="primary"
						type="submit"
						form="addrForm"
						disabled={!this.validateAddrForm()}
						onClick={this.props.hideForm}
					>
						Zatwierdü
          </Button>
					<Button onClick={this.props.hideForm}>Anuluj</Button>
				</Modal.Footer>
			</Modal>
		);
	}

	renderUserFields() {
		return (<Modal.Body>
			<FormGroup controlId="firstName">
				<FormControl
					value={this.state.firstName}
					onChange={this.handleChange}
					type="name"
					name="name"
					placeholder="Podaj imie"
				/>
			</FormGroup>
			<FormGroup controlId="lastName">
				<FormControl
					value={this.state.lastName}
					onChange={this.handleChange}
					type="name"
					name="name"
					placeholder="Podaj nazwisko"
				/>
			</FormGroup>
			<FormGroup controlId="email">
				<FormControl
					value={this.state.email}
					onChange={this.handleChange}
					type="email"
					name="email"
					placeholder="Podaj email"
				/>
			</FormGroup>
			<DropdownButton
				title={`Preferowana waluta: ${this.state.currency}`}
				id={`dropdown-basic-0`}
			>
				{this.renderMenuItems()}
			</DropdownButton>
			<Checkbox
				defaultChecked={this.state.emailConfirmed}
				onClick={() => {
					this.setState({ emailConfirmed: !this.state.emailConfirmed });
				}}
			>
				Email potwierdzony
							</Checkbox>
			<Checkbox
				defaultChecked={this.state.prefersNetPrice}
				onClick={() => {
					this.setState({ prefersNetPrice: !this.state.prefersNetPrice });
				}}
			>
				Preferuje cene netto
							</Checkbox>
			<Checkbox
				defaultChecked={this.state.acceptsNewsletters}
				onClick={() => {
					this.setState({ acceptsNewsletters: !this.state.acceptsNewsletters });
				}}
			>
				Akceptuje newsletter
							</Checkbox>
			<FormGroup controlId="productsPerPage">
				<FormControl
					value={this.state.productsPerPage}
					onChange={this.handleChange}
					type="number"
					name="productsPerPage"
					placeholder="Liczba produktÛw na strone"
				/>
			</FormGroup>
		</Modal.Body>);
	}

	renderAddressFields() {
		if (this.props.apiAction === "add")
			return;
		return (<Modal.Body>
			<FormGroup controlId="city">
				<FormControl
					value={this.state.city}
					onChange={this.handleChange}
					type="name"
					name="city"
					placeholder="Podaj miasto"
				/>
			</FormGroup>
			<FormGroup controlId="postalCode">
				<FormControl
					value={this.state.postalCode}
					onChange={this.handleChange}
					type="name"
					name="postalCode"
					placeholder="Podaj kod pocztowy"
				/>
			</FormGroup>
			<FormGroup controlId="street">
				<FormControl
					value={this.state.street}
					onChange={this.handleChange}
					type="name"
					name="street"
					placeholder="Podaj ulice"
				/>
			</FormGroup>
			<FormGroup controlId="houseNumber">
				<FormControl
					value={this.state.houseNumber}
					onChange={this.handleChange}
					type="numerical"
					name="houseNumber"
					placeholder="Podaj nr domu"
				/>
			</FormGroup>
		</Modal.Body>);
	}
}
