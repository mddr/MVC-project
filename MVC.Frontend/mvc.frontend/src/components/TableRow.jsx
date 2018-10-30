import React, { Component } from "react";

import { Button, FormControl, FormGroup, Modal, DropdownButton, MenuItem, Checkbox } from 'react-bootstrap';

class TableRow extends Component {
	constructor(props) {
		super(props);
		this.state = {
			isHiddenChecked: false,
		  showRemoveDialog: false,
		  showEditDialog: false
		};
		this.deleteData = this.deleteData.bind(this);
		this.handleSubmit = this.handleSubmit.bind(this);
		this.handleChange = this.handleChange.bind(this);
		this.renderFormContent = this.renderFormContent.bind(this);
		this.handleFile = this.handleFile.bind(this);
		this.renderMenuItems = this.renderMenuItems.bind(this);
	}
	
	componentDidMount() {		
		const { rowData } = this.props;
		const rowKeys = Object.keys(rowData);
		for (let i = 0; i < rowKeys.length; i++) {
			this.setState( { [rowKeys[i]]: rowData[rowKeys[i]]} );
		}
	}
	
	
	
		handleSubmit(event){
		event.preventDefault();
		let body = "";
		switch(this.props.apiUrl.singular){
			case "/product":
				body = JSON.stringify({
					id: this.props.rowData.id,
					isHidden: this.state.isHiddenChecked,
					name: this.state.name,
					pricePln: this.state.pricePln,
					categoryId: this.state.categoryId,
					amountAvailable: this.state.amountAvailable,
					expertEmail: this.state.expertEmail,
					discount: this.state.discount,
					imageBase64: this.state.imageBase64
				});
				break;
			case "/category":
				body = JSON.stringify({
					id: this.props.rowData.id,
					name: this.state.name,
					superiorCategoryId: this.state.superiorCategoryId
				});
				break;
			default: break;
		}
		this.props.Auth.fetch(`${this.props.Auth.domain}${this.props.apiUrl.singular}/update`, {
		method: 'post',
		body
		});
		this.setState({ showCreateModal: false});
		window.location.reload(true);
	}
	handleFile = e => {
		const files = Array.from(e.target.files)

		var reader = new FileReader();
	   reader.readAsDataURL(files[0]);
	   reader.onload = () => {
		this.setState({ imageBase64: reader.result });
		};
	}
		renderFormContent(){
		switch(this.props.apiUrl.singular){
			case "/product":
				return(		
				<div>
					<FormGroup controlId="name">
					  <FormControl
						value={this.state.name}
						onChange={this.handleChange}
						type="name"
						name="name"
						placeholder="Podaj nazwe"
					  />
					</FormGroup>
					<FormGroup controlId="pricePln">
					  <FormControl
					  value={this.state.pricePln}
						onChange={this.handleChange}
						type="pricePln"
						name="pricePln"
						placeholder="Podaj cene w PLN"
					  />
				  </FormGroup>
					  <Checkbox checked={this.state.isHiddenChecked} onClick={() => {this.setState( {isHiddenChecked: !this.state.isHiddenChecked} )}}>
						Ukryty
					</Checkbox>
				<DropdownButton title={`Kategoria nadrzędna: ${this.state.superiorCategoryName}`} id={`dropdown-basic-0`} >
					{this.renderMenuItems("product")}
				</DropdownButton>
					<FormGroup controlId="amountAvailable">
					  <FormControl
					  value={this.state.amountAvailable}
						onChange={this.handleChange}
						type="amountAvailable"
						name="amountAvailable"
						placeholder="Podaj ilość dostępnych"
					  />
				  </FormGroup>
					<FormGroup controlId="expertEmail">
					  <FormControl
					  value={this.state.expertEmail}
						onChange={this.handleChange}
						type="expertEmail"
						name="expertEmail"
						placeholder="Podaj email eksperta"
					  />
				  </FormGroup>					
				  <FormGroup controlId="discount">
					  <FormControl
					  value={this.state.discount}
						onChange={this.handleChange}
						type="discount"
						name="discount"
						placeholder="Podaj zniżke"
					  />
				  </FormGroup>
				  <div className='button'>
					  <label htmlFor='single'>Ikona
					  </label>
					  <input type='file' id='single' onChange={this.handleFile} /> 
					</div>

				  
				  </div>
				);
			case "/category":
				return(		
				<div>
					<FormGroup controlId="name">
					  <FormControl
					  value={this.state.name}
						onChange={this.handleChange}
						type="name"
						name="name"
						placeholder="Podaj nazwe"
					  />
					</FormGroup>
				<DropdownButton title={`Kategoria nadrzędna: ${this.state.superiorCategoryName}`} id={`dropdown-basic-0`} >
					{this.renderMenuItems("category")}
				</DropdownButton>
				  </div>
				);
				default: return;
		}
	}
	
	renderMenuItems(type){		
		const items = [];
		for (let i = 0; i < this.props.categories.length; i++) {
			  switch(type){
				  case "category":
					  items.push(
							<MenuItem key={i} eventKey={i} onClick={() => this.setState( {superiorCategoryId: this.props.categories[i].id, superiorCategoryName: this.props.categories[i].name} )} >{this.props.categories[i].id}. {this.props.categories[i].name}</MenuItem>
						);
						break;
				  case "product":
					  items.push(
							<MenuItem key={i} eventKey={i} onClick={() => this.setState( {categoryId: this.props.categories[i].id, superiorCategoryName: this.props.categories[i].name} )} >{this.props.categories[i].id}. {this.props.categories[i].name}</MenuItem>
						);
						break;
					default: break;
			  }
		}
		  return items;	
	}
	handleChange = event => {
		this.setState({
		[event.target.id]: event.target.value
		});
	};
	
	deleteData(){
		this.props.Auth.fetch(`${this.props.Auth.domain}${this.props.apiUrl.singular}/delete/${this.props.rowData.id}`, {
		method: 'delete'
  });
		this.setState({ showRemoveDialog: false });
		window.location.reload(true);
	}
	

  render() {
    const { rowData } = this.props;
    const rowKeys = Object.keys(rowData);
    const rows = [];
	let content = {};
    for (let i = 0; i < rowKeys.length; i++) {
		switch(rowKeys[i]){
					case "id": content = rowData[rowKeys[i]]; break;
					case "name": content = rowData[rowKeys[i]]; break;
					case "categoryId": content = rowData[rowKeys[i]]; break;
					case "isHidden": rowData[rowKeys[i]] ? content = "tak" : content = "nie" ; break;
					case "expertEmail": content = rowData[rowKeys[i]]; break;
					case "pricePln": content = rowData[rowKeys[i]]; break;
					case "taxRate": content = rowData[rowKeys[i]]; break;
					case "discount": content = rowData[rowKeys[i]]; break;
					case "amountAvailable": content = rowData[rowKeys[i]]; break;
					case "boughtTimes": content = rowData[rowKeys[i]]; break;
					case "imageBase64": content = <img src={"data:image/jpeg;base64," + rowData[rowKeys[i]] } alt="" />; break;
					case "superiorCategoryId": content = rowData[rowKeys[i]]; break;
					case "subCategories": content = "";
											for (let j = 0; j < rowData[rowKeys[i]].length; j++) {
													content += rowData[rowKeys[i]][j].id + ", ";
												}; 
											break;
					default: continue;
				}
      rows.push(<td key={i}>{content}</td>);
    }
	
    return (
      <tr>
        {rows}
        <td>
          <a href="#" style={{ marginRight: "8px" }} onClick={ ()=> {this.setState({ showEditDialog: true })} }>
            <i className="fa fa-pencil fa-2x" aria-hidden="true" />
          </a>											
										  <Modal show={this.state.showEditDialog}>
											<Modal.Header>
											  <Modal.Title>Edytuj</Modal.Title>
											</Modal.Header>

											<Modal.Body>
												<form onSubmit={this.handleSubmit} id="editForm">
													{this.renderFormContent()}
												</form>
												</Modal.Body>
											<Modal.Footer>
											  <Button type="submit" form="editForm" onClick={ ()=> {this.setState({ apiAction: "/update" })} }>Zatwierdź</Button>
											  <Button bsStyle="primary" onClick={ ()=> {this.setState({ showEditDialog: false })} }>Anuluj</Button>
											</Modal.Footer>
										  </Modal>
          <a href="#" onClick={ ()=> {this.setState({ showRemoveDialog: true })} }>
            <i className="fa fa-trash fa-2x" aria-hidden="true" />
          </a>
										  <Modal show={this.state.showRemoveDialog}>
											<Modal.Header>
											  <Modal.Title>Usuń</Modal.Title>
											</Modal.Header>

											<Modal.Body>Czy usunąć?</Modal.Body>

											<Modal.Footer>
											  <Button onClick={this.deleteData}>Usuń</Button>
											  <Button bsStyle="primary" onClick={ ()=> {this.setState({ showRemoveDialog: false })} }>Anuluj</Button>
											</Modal.Footer>
										  </Modal>
        </td>
      </tr>

    );
  }
}

export default TableRow;
