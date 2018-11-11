import * as React from "react";
import { Button, FormControl, FormGroup, Modal, DropdownButton, MenuItem } from 'react-bootstrap';


export default class ProductForm extends React.Component{
  constructor(props) {
    super(props);
	this.state = {
		id: "",
		name: "",
		superiorCategoryId: "",
		categoryName:  "",
	};
    this.handleSubmit = this.handleSubmit.bind(this);	
	this.handleChange = this.handleChange.bind(this);
	this.validateForm = this.validateForm.bind(this);
	this.getCategoryName = this.getCategoryName.bind(this);
  }

	validateForm() {
		if (!(this.state.name.length > 0))
		  return false;
		return true;
	}

	handleChange = event => {
		this.setState({
		  [event.target.id]: event.target.value
		});
	};
	  
	componentDidMount(){
			if(this.props.modelProps)
			this.setState( { ...this.props.modelProps, categoryName: this.getCategoryName(this.props.modelProps.superiorCategoryId, this.props.categories)});
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
				superiorCategoryId: this.state.categoryId,
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
														<DropdownButton title={`Kategoria nadrzędna: ${this.state.categoryName}`} id={`dropdown-basic-0`} >
															{this.renderMenuItems()}
														</DropdownButton>

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