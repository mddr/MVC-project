import * as React from "react";
import { Button, FormControl, FormGroup, Modal, DropdownButton, MenuItem, Checkbox } from 'react-bootstrap';


export default class ProductForm extends React.Component{
  constructor(props) {
    super(props);
	this.state = {
		id: "",
		name: "",
		isHiddenChecked: false,
		pricePln: "",
		categoryId: "",
		amountAvailable: "",
		expertEmail:  "",
		discount: "",
		imageBase64:  "",
		categoryName:  "",
	};
    this.handleSubmit = this.handleSubmit.bind(this);	
	this.handleChange = this.handleChange.bind(this);
	this.validateForm = this.validateForm.bind(this);
	this.handleFile = this.handleFile.bind(this);
	this.getCategoryName = this.getCategoryName.bind(this);
  }

	validateForm() {
		if (!(this.state.name.length > 0 && this.state.pricePln > 0 && this.state.categoryId > 0 
            && this.state.amountAvailable > -1 && this.state.discount >= 0 && this.state.discount <= 100
            && this.state.discount !=="" && this.state.amountAvailable !==""))
		  return false;
		return true;
	}

	handleChange = event => {
		this.setState({
		  [event.target.id]: event.target.value
		});
	};
	
	handleFile = e => {
		const files = Array.from(e.target.files);
		var reader = new FileReader();
	   reader.readAsDataURL(files[0]);
	   reader.onload = () => {
		this.setState({ imageBase64: reader.result });
		};
	}
	  
	componentDidMount(){
			if(this.props.modelProps)
			this.setState( { ...this.props.modelProps, categoryName: this.getCategoryName(this.props.modelProps.categoryId, this.props.categories)});
	}
	
	getCategoryName(id, categories){
		for(let j =0; j<categories.length;j++){
											if(categories[j].id === id)
												return categories[j].name;
										}
		return "";
	}

	handleSubmit(event){
		event.preventDefault();
		let body = "";
		const obj = {
				id: this.state.id,
				name: this.state.name,
				isHidden: this.state.isHiddenChecked,
				pricePln: this.state.pricePln,
				categoryId: this.state.categoryId,
				amountAvailable: this.state.amountAvailable,
				expertEmail: this.state.expertEmail,
				discount: this.state.discount,
				imageBase64: this.state.imageBase64
			};
		
			body = JSON.stringify(obj);
				
        this.props.Auth.fetch(`${this.props.Auth.domain}/${this.props.apiUrl.singular}/${this.props.apiAction}`, {
            method: 'post',
            body
        }).then(() => {
            this.props.updateData(obj);
        });
	}
	
	renderMenuItems(){
		const items = [];
		for (let i = 0; i < this.props.categories.length; i++) {
		  items.push(
				<MenuItem 
					key={i} eventKey={i} 
					onClick={() => this.setState( {categoryId: this.props.categories[i].id, categoryName: this.props.categories[i].name} )}>
						{this.props.categories[i].name}
				</MenuItem>
			);
		}
		  return items;	
	}

	render(){		
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
															<FormGroup controlId="pricePln">
															  <FormControl
																value={this.state.pricePln}															  
																onChange={this.handleChange}
																type="number"
																name="pricePln"
																placeholder="Podaj cene w PLN"
															  />
														  </FormGroup>
															  <Checkbox defaultChecked={this.state.isHiddenChecked} onClick={() => {this.setState( {isHiddenChecked: !this.state.isHiddenChecked} )}}>
																Ukryty
															</Checkbox>
															<FormGroup controlId="categoryId">
														<DropdownButton title={`Kategoria nadrzędna: ${this.state.categoryName}`} id={`dropdown-basic-0`} >
															{this.renderMenuItems()}
														</DropdownButton>
														  </FormGroup>
															<FormGroup controlId="amountAvailable">
															  <FormControl
																value={this.state.amountAvailable}
																onChange={this.handleChange}
																type="number"
																name="amountAvailable"
																placeholder="Podaj ilość dostępnych"
															  />
														  </FormGroup>
															<FormGroup controlId="expertEmail">
															  <FormControl
																value={this.state.expertEmail}
																onChange={this.handleChange}
																type="email"
																name="expertEmail"
																placeholder="Podaj email eksperta"
															  />
														  </FormGroup>					
														  <FormGroup controlId="discount">
															  <FormControl
																value={this.state.discount}
																onChange={this.handleChange}
																type="number"
																name="discount"
																placeholder="Podaj zniżke"
															  />
														  </FormGroup>
														  <div className='button'>
															  <label htmlFor='single'>Ikona
															  </label>
															  <input type='file' id='single' onChange={this.handleFile} /> 
														</div>
										</form>
									</Modal.Body>
								<Modal.Footer>
								  <Button bsStyle="primary" type="submit" form="createForm" disabled={!this.validateForm()} onClick={ this.props.hideForm }>Zatwierdź</Button>
								  <Button onClick={ this.props.hideForm }>Anuluj</Button>
								</Modal.Footer>
							  </Modal>
        )
    }

}