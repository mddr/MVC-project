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
            <i class="fa fa-pencil fa-2x" aria-hidden="true" />
          </a>
          <a href="#">
            <i class="fa fa-trash fa-2x" aria-hidden="true" />
          </a>
        </td>
      </tr>
    );
  }
}

export default TableRow;
