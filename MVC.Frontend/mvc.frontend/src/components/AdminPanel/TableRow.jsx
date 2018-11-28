import React, { Component } from "react";

import FormBuilder from '../Helpers/FormBuilder';

class TableRow extends Component {
	constructor(props) {
		super(props);
		this.state = {
			isHiddenChecked: false,
			showDeleteForm: false,
			showEditForm: false,
		};
		this.getCategoryName = this.getCategoryName.bind(this);
		this.hideEditForm = this.hideEditForm.bind(this);
		this.hideDeleteForm = this.hideDeleteForm.bind(this);
	}
	
	componentDidMount() {		
		const rowKeys = Object.keys(this.props.rowData);
		for (let i = 0; i < rowKeys.length; i++) {
			this.setState( { [rowKeys[i]]: this.props.rowData[rowKeys[i]]} );
			if(rowKeys[i] ==="categoryId") {	this.setState( { superiorCategoryName: this.getCategoryName(this.props.rowData[rowKeys[i]]) } );}
			if(rowKeys[i] ==="superiorCategoryId") {	this.setState( { superiorCategoryName: this.getCategoryName(this.props.rowData[rowKeys[i]]) } );}
		}
	}
	
	getCategoryName(id){
		for(let j =0; j<this.props.categories.length;j++){
											if(this.props.categories[j].id === id)
												return this.props.categories[j].name;
										} 
	}
	
	hideEditForm(){
		this.setState({showEditForm: false});
	}	
	
	hideDeleteForm(){
		this.setState({showDeleteForm: false});
	}

  render() {
    const { rowData } = this.props;
    const rowKeys = Object.keys(rowData);
    const rows = [];
	let content = {};
    for (let i = 0; i < rowKeys.length; i++) {
		switch(rowKeys[i]){
					case "name": content = rowData[rowKeys[i]]; break;
					case "categoryId": 	content = this.getCategoryName(rowData[rowKeys[i]]); break;
					case "isHidden": rowData[rowKeys[i]] ? content = "tak" : content = "nie" ; break;
					case "expertEmail": content = rowData[rowKeys[i]]; break;
					case "pricePln": content = rowData[rowKeys[i]]; break;
					case "taxRate": content = rowData[rowKeys[i]]; break;
					case "discount": content = rowData[rowKeys[i]]; break;
					case "amountAvailable": content = rowData[rowKeys[i]]; break;
					case "boughtTimes": content = rowData[rowKeys[i]]; break;
                    case "imageBase64": content = <img src={"data:image/jpeg;base64," + rowData[rowKeys[i]]} alt=""  />; break;
					case "superiorCategoryId": content = rowData[rowKeys[i]]; break;
					case "subCategories": content = "";
											for (let j = 0; j < rowData[rowKeys[i]].length; j++) {
													content += rowData[rowKeys[i]][j].name + ", ";
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
          <a href="#" style={{ marginRight: "8px" }} onClick={ ()=> {this.setState({ showEditForm: true })} }>
            <i className="fa fa-pencil fa-2x" aria-hidden="true" />
          </a>											
				<FormBuilder  
					model={this.props.apiUrl.singular}
					Auth={this.props.Auth} apiUrl={this.props.apiUrl}  apiAction={"update"}
					categories={this.props.categories}
					showForm={this.state.showEditForm}
					title={"Edytuj"}
					hideForm={this.hideEditForm}
					modelProps={this.props.rowData}
					updateData={this.props.updateData}
				/>
          <a href="#" onClick={ ()=> {this.setState({ showDeleteForm: true })} }>
            <i className="fa fa-trash fa-2x" aria-hidden="true" />
          </a>
				<FormBuilder 
					model={"delete"}
					Auth={this.props.Auth} apiUrl={this.props.apiUrl}  apiAction={"delete"}
					showForm={this.state.showDeleteForm}
					title={"Edytuj"}
					hideForm={this.hideDeleteForm}
					modelProps={ {id: this.props.rowData.id} }
					updateData={this.props.updateData}
				/>
        </td>
      </tr>

    );
  }
}

export default TableRow;
