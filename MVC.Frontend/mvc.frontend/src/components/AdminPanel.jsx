import React, { Component } from "react";
import Table from "./Table";
import { Button, FormControl, FormGroup, Modal, DropdownButton, MenuItem, Checkbox } from 'react-bootstrap';

import AuthService from '../services/AuthService';

class AdminPanel extends Component {
  	constructor(props) {
    super(props);
	this.state = {
		isHiddenChecked: false,
		data: [],
		categories: [],
		apiUrl: {plural: "/categories", singular: "/category"},
		apiAction: "",
		showCreateModal: false,
		reloadData: false,
		};
	this.Auth = new AuthService();
	this.renderFormContent = this.renderFormContent.bind(this);
	this.renderModal = this.renderModal.bind(this);
	this.handleSubmit = this.handleSubmit.bind(this);
	this.handleChange = this.handleChange.bind(this);
	this.renderMenuItems = this.renderMenuItems.bind(this);
	this.fetchData = this.fetchData.bind(this);
	this.handleFile = this.handleFile.bind(this);
	this.needsReload = this.needsReload.bind(this);
	}
	
	componentDidMount() {		
		this.fetchData();
		this.Auth.fetch(this.Auth.domain + "/categories", null
		).then(res=>res.json()).then(res=>{
									this.setState({ 
											categories: res
											});
								});	
	}
	
	componentDidUpdate(prevProps, prevState) {
		if (this.state.apiUrl !== prevState.apiUrl || this.state.reloadData){
			this.fetchData();
			this.setState( {superiorCategoryName: "", categoryId: "", reloadData: false})
		}

	}
	
	needsReload(){
		this.setState({reloadData: true});
	}
	
	fetchData(){
		this.Auth.fetch(this.Auth.domain + this.state.apiUrl.plural, null
		).then(res=>res.json()).then(res=>{
									this.setState({ 
											data: res
											});
								});
	}
	
	
	renderMenuItems(type){	

		const items = [];
		for (let i = 0; i < this.state.categories.length; i++) {
			  switch(type){
				  case "category":
					  items.push(
							<MenuItem key={i} eventKey={i} onClick={() => this.setState( {superiorCategoryId: this.state.categories[i].id, superiorCategoryName: this.state.categories[i].name} )} >{this.state.categories[i].id} {this.state.categories[i].name}</MenuItem>
						);
						break;
				  case "product":
					  items.push(
							<MenuItem key={i} eventKey={i} onClick={() => this.setState( {categoryId: this.state.categories[i].id, superiorCategoryName: this.state.categories[i].name} )} >{this.state.categories[i].id} {this.state.categories[i].name}</MenuItem>
						);
						break;
					default: break;
			  }
		}
		  return items;	
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
		switch(this.state.apiUrl.singular){
			case "/product":
				return(		
				<div>
					<FormGroup controlId="productName">
					  <FormControl
						onChange={this.handleChange}
						type="productName"
						name="productName"
						placeholder="Podaj nazwe"
					  />
					</FormGroup>
					<FormGroup controlId="pricePln">
					  <FormControl
						onChange={this.handleChange}
						type="pricePln"
						name="pricePln"
						placeholder="Podaj cene w PLN"
					  />
				  </FormGroup>
					  <Checkbox checked={this.state.isHiddenChecked} onClick={() => {this.setState( {isHiddenChecked: !this.state.isHiddenChecked} )}}>
						Ukryty
					</Checkbox>
					<FormGroup controlId="categoryId">
				<DropdownButton title={`Kategoria nadrzędna: ${this.state.superiorCategoryName}`} id={`dropdown-basic-0`} >
					{this.renderMenuItems("product")}
				</DropdownButton>
				  </FormGroup>
					<FormGroup controlId="amountAvailable">
					  <FormControl
						onChange={this.handleChange}
						type="amountAvailable"
						name="amountAvailable"
						placeholder="Podaj ilość dostępnych"
					  />
				  </FormGroup>
					<FormGroup controlId="expertEmail">
					  <FormControl
						onChange={this.handleChange}
						type="expertEmail"
						name="expertEmail"
						placeholder="Podaj email eksperta"
					  />
				  </FormGroup>					
				  <FormGroup controlId="discount">
					  <FormControl
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
					<FormGroup controlId="categoryName">
					  <FormControl
						onChange={this.handleChange}
						type="categoryName"
						name="categoryName"
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
	
	handleSubmit(event){
		event.preventDefault();
		let body = "";
		console.log(this.state.isHiddenChecked);
		switch(this.state.apiUrl.singular){
			case "/product":
				body = JSON.stringify({
					name: this.state.productName,
					isHidden: this.state.isHiddenChecked,
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
					name: this.state.categoryName,
					superiorCategoryId: this.state.superiorCategoryId
				});
				break;
			default: break;
		}
		this.Auth.fetch(`${this.Auth.domain}${this.state.apiUrl.singular}${this.state.apiAction}`, {
		method: 'post',
		body
		});
		this.setState({ showCreateModal: false});
		window.location.reload(true);
	}
	
	handleChange = event => {
		this.setState({
		[event.target.id]: event.target.value
		});
	};
	
	renderModal(){
		return(<Modal show={this.state.showCreateModal}>
											<Modal.Header>
											  <Modal.Title>Utwórz</Modal.Title>
											</Modal.Header>											
												<Modal.Body>
												<form onSubmit={this.handleSubmit} id="createForm">
													{this.renderFormContent()}
												</form>
												</Modal.Body>
											<Modal.Footer>
											  <Button type="submit" form="createForm" onClick={ ()=> {this.setState({ apiAction: "/add" })} }>Zatwierdź</Button>
											  <Button bsStyle="primary" onClick={ ()=> {this.setState({ showCreateModal: false })} }>Anuluj</Button>
											</Modal.Footer>
										  </Modal>
			  );
	}
  
  render() {	 
    return (
      /*whole layouy*/
      <div className="container-fluid">
        <div className="row content">
          <div className="col-sm-3 sidenav">
            {/*left side*/}
            <div className="panel panel-default">
              <div className="panel-body">
                <h4>Tabels</h4>
                <ul className="nav nav-pills nav-stacked">
				<li className="active">
                    <a href="#section" onClick={ ()=> {this.setState({ apiUrl: {plural: "/categories", singular: "/category"} })} }>Kategorie</a>
                  </li>
				<li className="active">
                    <a href="#section2" onClick={ ()=> {this.setState({ apiUrl: {plural: "/products", singular: "/product"} })} }>Produkty</a>
                  </li>
                </ul>
                <br />
              </div>
            </div>
          </div>
          {/*right side*/}
          <div className="col-sm-9">
            <div className="panel panel-default">
              <div className="panel-body">
				<Button onClick={ ()=> {this.setState({ showCreateModal: true })} }>Utwórz</Button>
					{this.renderModal()}
					<Table apiUrl={this.state.apiUrl} Auth={this.Auth} data={this.state.data} categories={this.state.categories} needsReload={this.needsReload}
						handleSubmit={this.handleSubmit} handleChange={this.handleChange}
						renderFormContent={this.renderFormContent}
					/>
              </div>
            </div>
          </div>
        </div>
      </div>
    );
  }
}

export default AdminPanel;
