import React, { Component } from "react";
import AuthService from "../services/AuthService";

class SearchResultsPage extends Component {
  constructor(props) {
    super(props);
    this.state = {
      Products: [],
      categories: []
    };
    this.Auth = new AuthService();
  }
  componentDidMount() {
    this.fetchData();
  }

  fetchData() {
    this.Auth.fetch(`${this.Auth.domain}/products`, null)
      .then(res => res.json())
      .then(res => {
        this.setState({
          Products: res
        });
      });
    this.Auth.fetch(`${this.Auth.domain}/categories`, null)
      .then(res => res.json())
      .then(res => {
        this.setState({
          categories: res
        });
      });
  }

  render() {
    return <div className="search_results">TEST</div>;
  }
}

export default SearchResultsPage;
