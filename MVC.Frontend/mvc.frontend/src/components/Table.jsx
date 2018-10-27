import React, { Component } from "react";
import TableRow from "./TableRow";

class Table extends Component {
  state = {
    people: [
      {
        id: 1,
        name: "Szymon",
        email: "pizda@nieasperger.com",
        alias: "Człowiek falujący anus"
      },
      {
        id: 2,
        name: "Damian",
        email: "mistrz-zen@gmail.com",
        alias: "Człowiek kurwa"
      },
      {
        id: 3,
        name: "Czarek",
        email: "szmurlo.cezary@gmail.com",
        alias: "Człowiek czy obok pani wolne"
      },
      {
        id: 4,
        name: "Daniel",
        email: "misiek123@gmail.com",
        alias: "Człowiek Tunak Tun"
      }
    ]
  };
  render() {
    const keys = Object.keys(this.state.people[0]);
    const heads = [];
    for (let i = 0; i < keys.length; i++) {
      heads.push(<th>{keys[i].charAt(0).toUpperCase() + keys[i].slice(1)}</th>);
    }
    return (
      <main>
        <table className="table table-dark">
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
