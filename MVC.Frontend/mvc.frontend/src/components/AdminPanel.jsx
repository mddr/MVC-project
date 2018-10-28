import React, { Component } from "react";
import Table from "./Table";

import AuthService from '../services/AuthService';

class AdminPanel extends Component {
  	constructor(props) {
    super(props);
	this.state = {
		apiUrl: {plural: "/products", singular: "/product"}
		};
	this.Auth = new AuthService();
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
