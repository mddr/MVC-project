import React, { Component } from "react";
import TableRow from "./TableRow";

class Table extends Component {
  //Todo get data from api
  state = {
    people: [
      {
        id: 1,
        name: "Damian",
        surname: "Jaźwiński",
        email: "damian@mail.com"
      },
      {
        id: 2,
        name: "Cezary",
        surname: "Szmurło",
        email: "cezary@mail.com"
      },
      {
        id: 3,
        name: "Adam",
        surname: "Sienkiewicz",
        email: "adam@mail.com"
      },
      {
        id: 4,
        name: "Paweł",
        surname: "Jacewicz",
        email: "pawel@mail.com"
      }
    ]
  };
  render() {
    const keys = Object.keys(this.state.people[0]);
    const heads = [];
    for (let i = 0; i < keys.length; i++) {
      heads.push(
        <th style={{ textAlign: "center" }}>
          {keys[i].charAt(0).toUpperCase() + keys[i].slice(1)}
        </th>
      );
    }
    return (
      <main>
        <table className="table table-striped">
          <thead>
            <tr>{heads}</tr>
          </thead>
          <tbody>
            {this.state.people.map(person => (
              <TableRow key={person.id} rowData={person} />
            ))}
          </tbody>
        </table>
      </main>
    );
  }
}

export default Table;
