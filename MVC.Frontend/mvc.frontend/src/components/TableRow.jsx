import React, { Component } from "react";

import { Button, Modal } from 'react-bootstrap';

class TableRow extends Component {
	constructor(props) {
		super(props);
		this.state = {
		  showRemoveDialog: false,
		  showEditDialog: false
		};
		this.deleteData = this.deleteData.bind(this);
	}
	
	deleteData(){
		this.props.Auth.fetch(`${this.props.Auth.domain}${this.props.apiUrl.singular}/delete/${this.props.rowData.id}`, {
		method: 'delete'
  });
		this.setState({ showRemoveDialog: false });
	}

  render() {
    const { rowData } = this.props;
    const rowKeys = Object.keys(rowData);
    const rows = [];
    for (let i = 0; i < rowKeys.length; i++) {
      rows.push(<td key={i}>{rowData[rowKeys[i]]}</td>);
    }
	
    return (
      <tr>
        {rows}
        <td>
          <a href="#" style={{ marginRight: "8px" }} onClick={ ()=> {this.setState({ showEditDialog: true })} }>
            <i className="fa fa-pencil fa-2x" aria-hidden="true" />
          </a>											
										  <Modal show={this.state.showEditDialog}>
											<Modal.Header>
											  <Modal.Title>Usun</Modal.Title>
											</Modal.Header>

											//TODO: add edit form here
											<Modal.Body>Czy usunac?</Modal.Body>

											<Modal.Footer>
											  <Button >Zatwierdź</Button>
											  <Button bsStyle="primary" onClick={ ()=> {this.setState({ showEditDialog: false })} }>Anuluj</Button>
											</Modal.Footer>
										  </Modal>
          <a href="#" onClick={ ()=> {this.setState({ showRemoveDialog: true })} }>
            <i className="fa fa-trash fa-2x" aria-hidden="true" />
          </a>
										  <Modal show={this.state.showRemoveDialog}>
											<Modal.Header>
											  <Modal.Title>Usuń</Modal.Title>
											</Modal.Header>

											<Modal.Body>Czy usunąć?</Modal.Body>

											<Modal.Footer>
											  <Button onClick={this.deleteData}>Usuń</Button>
											  <Button bsStyle="primary" onClick={ ()=> {this.setState({ showRemoveDialog: false })} }>Anuluj</Button>
											</Modal.Footer>
										  </Modal>
        </td>
      </tr>

    );
  }
}

export default TableRow;
