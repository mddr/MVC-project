import * as React from "react";
import {
  Button,
  Checkbox,
  DropdownButton,
  FormControl,
  FormGroup,
  Glyphicon,
  MenuItem,
  Modal
} from "react-bootstrap";
import { Editor } from "react-draft-wysiwyg";
import "react-draft-wysiwyg/dist/react-draft-wysiwyg.css";
import { EditorState, convertToRaw } from "draft-js";
import { stateToHTML } from "draft-js-export-html";
import ProductService from "../../services/ProductService";

export default class ProductForm extends React.Component {
  constructor(props) {
    super(props);
    this.state = {
      id: "",
      name: "",
      isHiddenChecked: false,
      pricePln: "",
      categoryId: "",
      amountAvailable: "",
      expertEmail: "",
      description: EditorState.createEmpty(),
      discount: "",
      imageBase64: "",
      categoryName: "",
      taxRate: "",
      fileDesc: "",
      filesList: [],
      files: []
    };
    this.handleSubmit = this.handleSubmit.bind(this);
    this.handleChange = this.handleChange.bind(this);
    this.validateForm = this.validateForm.bind(this);
    this.handleFile = this.handleFile.bind(this);
    this.getCategoryName = this.getCategoryName.bind(this);
    this.ProductService = new ProductService();
  }

  validateForm() {
      if (
        !(
          this.state.name.length > 0 &&
          this.state.pricePln > 0 &&
          this.state.categoryId > 0 &&
          this.state.amountAvailable > -1 &&
                  this.state.taxRate > -1 &&
                  this.state.taxRate <= 100 &&
          this.state.discount >= 0 &&
          this.state.discount <= 100 &&
          this.state.discount !== "" &&
                  this.state.taxRate !== "" &&
          this.state.amountAvailable !== ""
        )
      )
          return false;
      return true;
  }

  handleChange = event => {
    this.setState({
      [event.target.id]: event.target.value
    });
  };

  handleEditorStateChange = description => {
    this.setState({ description });
  };

  handleFile = e => {
    const files = Array.from(e.target.files);
    var reader = new FileReader();
    reader.readAsDataURL(files[0]);
    reader.onload = () => {
      this.setState({ imageBase64: reader.result });
    };
  };
  handleMultipleFiles = e => {
    const recivedFiles = Array.from(e.target.files);
    this.setState({ filesList: recivedFiles });
  };

  componentDidMount() {
    if (this.props.modelProps) {
      var stuff = {
        ...this.props.modelProps,
        categoryName: this.getCategoryName(
          this.props.modelProps.categoryId,
          this.props.categories
        )
      };
      if (!stuff.description)
        stuff.description = EditorState.createEmpty();
      this.setState({
        ...stuff
      });
    }
  }

  getCategoryName(id, categories) {
    for (let j = 0; j < categories.length; j++) {
      if (categories[j].id === id) return categories[j].name;
    }
    return "";
  }

  handleSubmit(event) {
    event.preventDefault();
    this.props.hideForm();
    let body = "";
    const obj = {
      id: this.state.id,
      name: this.state.name,
      isHidden: this.state.isHiddenChecked,
      pricePln: this.state.pricePln,
      categoryId: this.state.categoryId,
      amountAvailable: this.state.amountAvailable,
      expertEmail: this.state.expertEmail,
      description: stateToHTML(this.state.description.getCurrentContent()),
      discount: this.state.discount,
      imageBase64: this.state.imageBase64,
      taxRate: this.state.taxRate
    };

    body = JSON.stringify(obj);

    this.props.Auth.fetch(
      `${this.props.Auth.domain}/${this.props.apiUrl.singular}/${
        this.props.apiAction
      }`,
      {
        method: "post",
        body
      }
    ).then(() => {
      //dodawanie plików
      if (this.state.filesList.length === 0) this.props.updateData(obj);
			let filereader = new FileReader();
			filereader.readAsDataURL(this.state.filesList[0]);
			// eslint-disable-next-line no-loop-func
			filereader.onload = () => {
        this.ProductService.addFile(
          this.state.id,
          this.state.filesList[0].name,
          filereader.result,
          this.state.fileDesc
        ).then(() => {
          this.props.updateData(obj);
        });
			};
    });
  }

  renderMenuItems() {
    const items = [];
    for (let i = 0; i < this.props.categories.length; i++) {
      items.push(
        <MenuItem
          key={i}
          eventKey={i}
          onClick={() =>
            this.setState({
              categoryId: this.props.categories[i].id,
              categoryName: this.props.categories[i].name
            })
          }
        >
          {this.props.categories[i].name}
        </MenuItem>
      );
    }
    return items;
  }

  render() {
    return (
      <Modal show={this.props.showForm}>
        <Modal.Header>
          <Modal.Title>{this.props.title}</Modal.Title>
        </Modal.Header>
        <Modal.Body>
            <FormGroup controlId="name">
              <FormControl
                value={this.state.name}
                onChange={this.handleChange}
                type="name"
                name="name"
                placeholder="Podaj nazwe"
              />
            </FormGroup>
            <FormGroup controlId="pricePln">
              <FormControl
                value={this.state.pricePln}
                onChange={this.handleChange}
                type="number"
                name="pricePln"
                placeholder="Podaj cene w PLN"
              />
            </FormGroup>
            <Checkbox
              defaultChecked={this.state.isHiddenChecked}
              onClick={() => {
                this.setState({ isHiddenChecked: !this.state.isHiddenChecked });
              }}
            >
              Ukryty
            </Checkbox>
            <FormGroup controlId="categoryId">
              <DropdownButton
                title={`Kategoria nadrzędna: ${this.state.categoryName}`}
                id={`dropdown-basic-0`}
              >
                {this.renderMenuItems()}
              </DropdownButton>
            </FormGroup>
            <FormGroup controlId="amountAvailable">
              <FormControl
                value={this.state.amountAvailable}
                onChange={this.handleChange}
                type="number"
                name="amountAvailable"
                placeholder="Podaj ilość dostępnych"
              />
            </FormGroup>
            <FormGroup controlId="expertEmail">
              <FormControl
                value={this.state.expertEmail}
                onChange={this.handleChange}
                type="email"
                name="expertEmail"
                placeholder="Podaj email eksperta"
              />
            </FormGroup>
            <FormGroup controlId="discount">
              <FormControl
                value={this.state.discount}
                onChange={this.handleChange}
                type="number"
                name="discount"
                placeholder="Podaj zniżke"
              />
            </FormGroup>
            <FormGroup controlId="taxRate">
              <FormControl
                value={this.state.taxRate}
                onChange={this.handleChange}
                type="number"
                name="taxRate"
                placeholder="Podaj wysokość opodatkowania"
              />
            </FormGroup>
            <div className="button">
              <label htmlFor="single">Ikona</label>
              <input type="file" id="single" onChange={this.handleFile} />
            </div>
            <div>
              <label htmlFor="multiplefiles">Dodaj pliki...</label>
              <input
                type="file"
                id="multiplefiles"
                onChange={this.handleMultipleFiles}
                
              />
          </div>
          <FormGroup controlId="fileDesc">
            <FormControl
              value={this.state.fileDesc}
              onChange={this.handleChange}
              type="text"
              name="fileDesc"
              placeholder="Podaj opis pliku"
            />
          </FormGroup>
            {this.renderProdcutFiles()}
            <FormGroup controlId="description">
              <Editor
                editorState={this.state.description}
                onEditorStateChange={this.handleEditorStateChange}
                name="description"
                placeholder="Podaj opis"
              />
            </FormGroup>
        </Modal.Body>
        <Modal.Footer>
          <Button
            bsStyle="primary"
            disabled={!this.validateForm()}
            onClick={this.handleSubmit}
          >
            Zatwierdź
          </Button>
          <Button onClick={this.props.hideForm}>Anuluj</Button>
        </Modal.Footer>
      </Modal>
    );
	}
	renderProdcutFiles() {
		let files = [];
		this.state.files.forEach((file, index) => {
			files.push(<li>
				{file.fileName} 
				<Glyphicon
					glyph="trash"
					style={{ color: "red" }}
					onClick={() => {
						this.ProductService.removeFile(this.state.id, file.id)
						let stateFiles = this.state.files;
						stateFiles = stateFiles.filter(elem => elem.id !== file.id);
						this.setState({ files: stateFiles });
					}}
				/>
			</li>)
		})
		return (<ul>
			{files}
		</ul>)
	}
}
