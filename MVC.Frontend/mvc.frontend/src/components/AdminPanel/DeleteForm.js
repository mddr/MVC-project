import * as React from "react";
import { Button, Modal } from "react-bootstrap";

export default class DeleteForm extends React.Component {
  constructor(props) {
    super(props);
    this.deleteData = this.deleteData.bind(this);
  }

  deleteData() {
    this.props.Auth.fetch(
      `${this.props.Auth.domain}/${this.props.apiUrl.singular}/${
        this.props.apiAction
      }/${this.props.modelProps.id}`,
      {
        method: "delete"
      }
    ).then(() => {
      this.props.hideForm();
      this.props.updateData(this.props.modelProps.id);
    });
  }

  render() {
    return (
      <Modal show={this.props.showForm}>
        <Modal.Header>
          <Modal.Title>{this.props.title}</Modal.Title>
        </Modal.Header>

        <Modal.Body>Czy usunąć?</Modal.Body>

        <Modal.Footer>
          <Button onClick={this.deleteData}>Usuń</Button>
          <Button bsStyle="primary" onClick={this.props.hideForm}>
            Anuluj
          </Button>
        </Modal.Footer>
      </Modal>
    );
  }
}
