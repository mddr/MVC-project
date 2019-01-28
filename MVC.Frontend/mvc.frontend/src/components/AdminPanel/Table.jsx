import "./Table.css";
import React, { Component } from "react";

import TableRow from "./TableRow";

class Table extends Component {
  constructor(props) {
    super(props);
    this.state = {
      sortBy: "name",
      sortDirection: "asc"
    };
    this.renderHeaders = this.renderHeaders.bind(this);
  }
  compareValues = (key, order = "asc") => {
    return (a, b) => {
      if (!a.hasOwnProperty(key) || !b.hasOwnProperty(key)) {
        // property doesn't exist on either object
        return 0;
      }

      const varA = typeof a[key] === "string" ? a[key].toUpperCase() : a[key];
      const varB = typeof b[key] === "string" ? b[key].toUpperCase() : b[key];

      let comparison = 0;

      if (varA > varB) {
        comparison = 1;
      } else if (varA < varB) {
        comparison = -1;
      }
      return this.state.sortDirection === "desc" ? comparison * -1 : comparison;
    };
  }

  sortHandler(key) {
    if (key === this.state.sortBy) {
      var temp = this.state.sortDirection === "asc" ? "desc" : "asc";
      console.log("Wyrażenie: ", temp);
      this.setState({
        sortDirection: temp
      });
    } else {
      this.setState({ sortDirection: "asc" });
    }
    this.setState({ sortBy: key });
  }
  renderHeaders() {
    const heads = [];
    if (this.props.data.length > 0) {
      const keys = Object.keys(this.props.data[0]);
      for (let i = 0; i < keys.length; i++) {
        let header = "s";
        switch (keys[i]) {
          case "name":
            header = "Nazwa";
            break;
          case "categoryId":
            header = "Kategoria";
            break;
          case "isHidden":
            header = "Ukryty";
            break;
          case "expertEmail":
            header = "Mail eksperta";
            break;
          case "pricePln":
            header = "Cena PLN";
            break;
          case "taxRate":
            header = "Stopa podatkowa";
            break;
          case "discount":
            header = "Zniżka";
            break;
          case "amountAvailable":
            header = "Dostępne";
            break;
          case "boughtTimes":
            header = "Kupiono";
            break;
          case "imageBase64":
            header = "Obrazek";
            break;
          case "superiorCategoryId":
            header = "Kategoria nadrzędna";
            break;
          case "subCategories":
            header = "Podkategorie";
            break;
          case "firstName":
            header = "Imie";
            break;
          case "lastName":
            header = "Nazwisko";
            break;
          case "email":
            header = "Email";
            break;
          case "currency":
            header = "Waluta";
            break;
          case "emailConfirmed":
            header = "Email potwierdzony";
            break;
          case "prefersNetPrice":
            header = "Preferuje cene netto";
            break;
          case "acceptsNewsletters":
            header = "Akceptuje newsletter";
            break;
          case "productsPerPage":
            header = "Liczba produktów na strone";
            break;
          case "address":
            header = "Adres";
            break;
          default:
            continue;
        }
        heads.push(
          <th key={i} style={{ textAlign: "center" }}>
            <button
              className="header-btn"
              style={{
                backgroundColor: "transparent",
                border: "none",
                fontWeight: "bold"
              }}
              onClick={() => this.sortHandler(keys[i])}
            >
              {header}
            </button>
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
            {this.props.data
              .sort(this.compareValues(this.state.sortBy))
              .map(data => (
                <TableRow
                  key={data.id}
                  rowData={data}
                  Auth={this.props.Auth}
                  apiUrl={this.props.apiUrl}
                  categories={this.props.categories}
                  updateData={this.props.updateData}
                />
              ))}
          </tbody>
        </table>
      </main>
    );
  }
}

export default Table;
