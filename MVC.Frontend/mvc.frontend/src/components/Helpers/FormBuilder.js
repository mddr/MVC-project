import * as React from "react";

import ProductForm from "../AdminPanel/ProductForm";
import CategoryForm from "../AdminPanel/CategoryForm";
import UserForm from "../AdminPanel/UserForm";
import DeleteForm from "../AdminPanel/DeleteForm";

export default class FormBuilder extends React.Component {
  render() {
    switch (this.props.model) {
      case "product":
        return (
          <ProductForm
            Auth={this.props.Auth}
            apiUrl={this.props.apiUrl}
            apiAction={this.props.apiAction}
            updateData={this.props.updateData}
            categories={this.props.categories}
            title={this.props.title}
            showForm={this.props.showForm}
            hideForm={this.props.hideForm}
            modelProps={this.props.modelProps}
          />
        );
      case "category":
        return (
          <CategoryForm
            Auth={this.props.Auth}
            apiUrl={this.props.apiUrl}
            apiAction={this.props.apiAction}
            updateData={this.props.updateData}
            categories={this.props.categories}
            title={this.props.title}
            showForm={this.props.showForm}
            hideForm={this.props.hideForm}
            modelProps={this.props.modelProps}
          />
				);
			case "user":
				return (
					<UserForm
						Auth={this.props.Auth}
						apiUrl={this.props.apiUrl}
						apiAction={this.props.apiAction}
						updateData={this.props.updateData}
						categories={this.props.categories}
						title={this.props.title}
						showForm={this.props.showForm}
						hideForm={this.props.hideForm}
						modelProps={this.props.modelProps}
					/>
				);
      case "delete":
        return (
          <DeleteForm
            Auth={this.props.Auth}
            apiUrl={this.props.apiUrl}
            apiAction={"delete"}
            updateData={this.props.updateData}
            showForm={this.props.showForm}
            title={"Usuń"}
            hideForm={this.props.hideForm}
            modelProps={{ id: this.props.modelProps.id }}
          />
        );
      default:
        return null;
    }
  }
}
