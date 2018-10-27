import React, { Component } from "react";

class TableRow extends Component {
  render() {
    const { rowData } = this.props;
    const rowKeys = Object.keys(rowData);
    const rows = [];
    for (let i = 0; i < rowKeys.length; i++) {
      rows.push(<td>{rowData[rowKeys[i]]}</td>);
    }
    return (
      <tr>
        {rows}
        <td>
          <a href="#">
            <span class="glyphicon glyphicon-edit" />
          </a>
          <a href="#">
            <span class="glyphicon glyphicon-delete" />
          </a>
        </td>
      </tr>
    );
  }
}

export default TableRow;
