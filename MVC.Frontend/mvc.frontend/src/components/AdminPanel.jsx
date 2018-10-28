import React, { Component } from "react";
import Table from "./Table";
import { Button, FormControl, FormGroup, Modal } from 'react-bootstrap';

import AuthService from '../services/AuthService';

class AdminPanel extends Component {
  	constructor(props) {
    super(props);
	this.state = {
		apiUrl: {plural: "/products", singular: "/product"},
		showCreateModal: false
		};
	this.Auth = new AuthService();
	this.renderCreateFormContent = this.renderCreateFormContent.bind(this);
	this.renderModal = this.renderModal.bind(this);
	this.createData = this.createData.bind(this);
	this.handleChange = this.handleChange.bind(this);
	}
	
	renderCreateFormContent(){
		switch(this.state.apiUrl.singular){
			case "/product":
				return(		
				<div>
					<FormGroup controlId="name">
					  <FormControl
						onChange={this.handleChange}
						type="name"
						name="name"
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
					<FormGroup controlId="categoryId">
					  <FormControl
						onChange={this.handleChange}
						type="categoryId"
						name="categoryId"
						placeholder="Podaj id kategorii"
					  />
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
				  
				  </div>
				);
			case "/category":
				return(		
				<div>
					<FormGroup controlId="name">
					  <FormControl
						onChange={this.handleChange}
						type="name"
						name="name"
						placeholder="Podaj nazwe"
					  />
					</FormGroup>
					<FormGroup controlId="superiorCategoryId">
					  <FormControl
						onChange={this.handleChange}
						type="superiorCategoryId"
						name="superiorCategoryId"
						placeholder="Podaj id kategorii nadrzędnej"
					  />
				  </FormGroup>
				  </div>
				);
				default: return;
		}
	}
	
	createData(event){
		event.preventDefault();
		let body = "";
		switch(this.state.apiUrl.singular){
			case "/product":
				body = JSON.stringify({
					name: this.state.name,
					pricePln: this.state.pricePln,
					categoryId: this.state.categoryId,
					amountAvailable: this.state.amountAvailable,
					expertEmail: this.state.expertEmail
				});
				break;
			case "/category":
				body = JSON.stringify({
					name: this.state.name,
					superiorCategoryId: this.state.superiorCategoryId
				});
				break;
			default: break;
		}
		this.Auth.fetch(`${this.Auth.domain}${this.state.apiUrl.singular}/add`, {
		method: 'post',
		body
		});
		this.setState({ showCreateModal: false });
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
												<form onSubmit={this.createData} id="createForm">
													{this.renderCreateFormContent()}
												</form>
												</Modal.Body>
											<Modal.Footer>
											  <Button type="submit" form="createForm">Zatwierdź</Button>
											  <Button bsStyle="primary" onClick={ ()=> {this.setState({ showCreateModal: false })} }>Anuluj</Button>
											</Modal.Footer>
										  </Modal>
			  );
	}
  
  render() {	 
	console.log(this.Auth.getProfile());  
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
                    <a href="#section1" onClick={ ()=> {this.setState({ apiUrl: "/asdasdd" })} }>Users</a>
                  </li>
				<li className="active">
                    <a href="#section2" onClick={ ()=> {this.setState({ apiUrl: {plural: "/products", singular: "/product"} })} }>Produkty</a>
                  </li>
				<li className="active">
                    <a href="#section3" onClick={ ()=> {this.setState({ apiUrl: {plural: "/categories", singular: "/category"} })} }>Kategorie</a>
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
                <Table apiUrl={this.state.apiUrl} Auth={this.Auth}/>
              </div>
            </div>
          </div>
        </div>
      </div>
    );
  }
}

export default AdminPanel;
