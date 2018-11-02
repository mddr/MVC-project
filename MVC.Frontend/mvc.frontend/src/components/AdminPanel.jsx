import React, { Component } from "react";
import Table from "./Table";
import { Button } from 'react-bootstrap';

import AuthService from '../services/AuthService';
import FormBuilder from './FormBuilder';

class AdminPanel extends Component {
  	constructor(props) {
    super(props);
	this.state = {
		data: [],
		categories: [],
		apiUrl: {plural: "products", singular: "product"},
		showCreateForm: false,
		};
	this.Auth = new AuthService();
	this.handleChange = this.handleChange.bind(this);
	this.fetchData = this.fetchData.bind(this);
	this.hideForm = this.hideForm.bind(this);
	this.updateData = this.updateData.bind(this);
	}
	
	componentDidMount() {		
		this.fetchData();
	}
	
	componentDidUpdate(prevProps, prevState) {
		if (this.state.apiUrl !== prevState.apiUrl){
			this.fetchData();
		}
	}
	
	fetchData(){
		this.Auth.fetch(`${this.Auth.domain}/${this.state.apiUrl.plural}`, null
		).then(res=>res.json()).then(res=>{
									this.setState({ 
											data: res
											});
								});
		this.Auth.fetch(`${this.Auth.domain}/categories`, null
		).then(res=>res.json()).then(res=>{
									this.setState({ 
											categories: res,
											});
								});
	}
	
	handleChange = event => {
		this.setState({
		  ["form-"+event.target.id]: event.target.value
		});
	  };
  
	hideForm(){
		this.setState({showCreateForm: false});
	}
	
	updateData(model){
		window.location.reload(true);
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
                    <a href="#section" onClick={ ()=> {this.setState({ apiUrl: {plural: "categories", singular: "category"} })} }>Kategorie</a>
                  </li>
				<li className="active">
                    <a href="#section2" onClick={ ()=> {this.setState({ apiUrl: {plural: "products", singular: "product"} })} }>Produkty</a>
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
				<Button onClick={ ()=> {this.setState({ showCreateForm: true })} }>Utwórz</Button>
						<FormBuilder  
							model={this.state.apiUrl.singular}
							Auth={this.Auth} apiUrl={this.state.apiUrl}  apiAction={"add"}
							categories={this.state.categories}
							showForm={this.state.showCreateForm}
							title={"Utwórz"}
							hideForm={this.hideForm}
							updateData={this.updateData}
						/>
					<Table apiUrl={this.state.apiUrl} Auth={this.Auth} data={this.state.data} categories={this.state.categories} updateData={this.updateData}
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
