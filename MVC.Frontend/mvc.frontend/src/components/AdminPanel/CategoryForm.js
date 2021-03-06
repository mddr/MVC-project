﻿import * as React from "react";
import {
	Button,
	Checkbox,
  DropdownButton,
  FormControl,
  FormGroup,
  MenuItem,
  Modal
} from "react-bootstrap";
import CategoryService from "../../services/CategoryService";
import AuthService from "../../services/AuthService";

export default class ProductForm extends React.Component {
  constructor(props) {
    super(props);
    this.state = {
      id: "",
      name: "",
			isHidden: true,
      superiorCategoryId: "",
      categoryName: ""
    };
    this.handleSubmit = this.handleSubmit.bind(this);
    this.handleChange = this.handleChange.bind(this);
    this.validateForm = this.validateForm.bind(this);
		this.getCategoryName = this.getCategoryName.bind(this);
    this.getPdf = this.getPdf.bind(this);
		this.CategoryService = new CategoryService();
    this.AuthService = new AuthService();
  }

  validateForm() {
    if (!(this.state.name.length > 0)) return false;
    return true;
  }

  handleChange = event => {
    this.setState({
      [event.target.id]: event.target.value
    });
  };

  componentDidMount() {
    if (this.props.modelProps)
      this.setState({
        ...this.props.modelProps,
        categoryName: this.getCategoryName(
          this.props.modelProps.superiorCategoryId,
          this.props.categories
        )
      });
  }

  getCategoryName(id, categories) {
    for (let j = 0; j < categories.length; j++) {
      if (categories[j].id === id) return categories[j].name;
    }
    return "";
  }

  handleSubmit(event) {
    event.preventDefault();
    let body = "";
    const obj = {
      id: this.state.id,
      name: this.state.name,
      superiorCategoryId: this.state.categoryId
    };

    body = JSON.stringify(obj);

    this.props.Auth.fetch(
      `${this.props.Auth.domain}/${this.props.apiUrl.singular}/${
        this.props.apiAction
      }`,
      {
        method: "post",
        body
      }
		).then(() => {
			if (this.state.isHidden) this.CategoryService.hideCategory(this.state.id);
			else this.CategoryService.showCategory(this.state.id);
		})
		.then(() => {
      this.props.updateData(obj);
    });
  }

  getPdf() {
    this.CategoryService.getPdfSummary(this.state.id)
      .then(res => {
        //var element = document.createElement('a');
        //element.setAttribute('href', 'data:application/octet-stream;,' + res);
        //element.setAttribute('download', this.state.categoryName+"-summary.pdf");

        //element.style.display = 'none';
        //document.body.appendChild(element);

        //element.click();

        //document.body.removeChild(element);
        window.open(this.AuthService.domain + `/category/${this.state.id}/summary`);
      })
  }

  renderMenuItems() {
    const items = [];
    for (let i = 0; i < this.props.categories.length; i++) {
      items.push(
        <MenuItem
          key={i}
          eventKey={i}
          onClick={() =>
            this.setState({
              categoryId: this.props.categories[i].id,
              categoryName: this.props.categories[i].name
            })
          }
        >
          {this.props.categories[i].name}
        </MenuItem>
      );
    }
    return items;
  }

  render() {
    return (
      <Modal show={this.props.showForm}>
        <Modal.Header>
          <Modal.Title>{this.props.title}</Modal.Title>
        </Modal.Header>
        <Modal.Body>
          <form onSubmit={this.handleSubmit} id="createForm">
            <FormGroup controlId="name">
              <FormControl
                value={this.state.name}
                onChange={this.handleChange}
                type="name"
                name="name"
                placeholder="Podaj nazwe"
              />
            </FormGroup>
            <DropdownButton
              title={`Kategoria nadrzędna: ${this.state.categoryName}`}
              id={`dropdown-basic-0`}
            >
              {this.renderMenuItems()}
						</DropdownButton>
						<Checkbox
							defaultChecked={this.state.isHidden}
							onClick={() => {
								this.setState({ isHidden: !this.state.isHidden });
							}}
						>
							Ukryta
							</Checkbox>
          </form>
          <Button
            bsStyle="primary"
            onClick={this.getPdf}
          >
            Generuj cennik pdf
          </Button>
        </Modal.Body>
        <Modal.Footer>
          <Button
            bsStyle="primary"
            type="submit"
            form="createForm"
            disabled={!this.validateForm()}
            onClick={this.props.hideForm}
          >
            Zatwierdź
          </Button>
          <Button onClick={this.props.hideForm}>Anuluj</Button>
        </Modal.Footer>
      </Modal>
    );
  }
}
