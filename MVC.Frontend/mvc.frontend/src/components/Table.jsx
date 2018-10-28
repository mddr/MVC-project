import React, { Component } from "react";
import TableRow from "./TableRow";

class Table extends Component {
	constructor(props) {
		super(props);
	this.state = {
		data:  []		
	  };
	  this.renderHeaders = this.renderHeaders.bind(this);
	  this.fetchData = this.fetchData.bind(this);
	}
	
	componentDidMount() {		
		this.fetchData();
	}
	
	componentDidUpdate(prevProps) {
		if (this.props.apiUrl !== prevProps.apiUrl)
			this.fetchData();

	}
	
	fetchData(){
		this.props.Auth.fetch(this.props.Auth.domain + this.props.apiUrl.plural, null
		).then(res=>res.json()).then(data=>{
							this.setState({ 
								data: data
								});
		});
	}
	
	renderHeaders(){
		const heads = [];
  		if (this.state.data.length > 0) {			
			const keys = Object.keys(this.state.data[0]);
			for (let i = 0; i < keys.length; i++) {
				let header = "s";
				switch(keys[i]){
					case "name": header = "Nazwa"; break;
					case "isHidden": header = "Ukryty?"; break;
					case "expertEmail": header = "Mail eksperta"; break;
					case "pricePln": header = "Cena PLN"; break;
					case "taxRate": header = "Stopa podatkowa"; break;
					case "discount": header = "Zniżka"; break;
					case "amountAvailable": header = "Dostępne"; break;
					case "boughtTimes": header = "Kupiono"; break;
					case "imageBase64": header = "Obrazek"; break;
					case "superiorCategoryId": header = "Kategoria nadrzędna"; break;
					case "subCategories": header = "Podkategorie"; break;
					default: continue;
				}
			  heads.push(
				<th key={i} style={{ textAlign: "center" }}>
				  {header}
				</th>
			  );
			}
		}
		  return heads;		
	}
	
  render() {
    return (
      <main>	  
        <table className="table table-striped">
          <thead>
            <tr>{this.renderHeaders()}</tr>
          </thead>
          <tbody>
            {this.state.data.map(data => (
              <TableRow key={data.id} rowData={data} Auth={this.props.Auth} apiUrl={this.props.apiUrl}/>
            ))}
          </tbody>
        </table>
      </main>
    );
  }
}

export default Table;
