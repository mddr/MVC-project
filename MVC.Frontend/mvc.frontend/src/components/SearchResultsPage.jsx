import "./SearchResultsPage.css";
import React, { Component } from "react";
import { Link } from "react-router-dom";
import { SideNav, Nav } from "react-sidenav";
import Product from "./Product";
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
  displayCategoriesTree = parentID => {
    // eslint-disable-next-line
    const categories = this.state.categories.map(category => {
      if (category.superiorCategoryId === parentID) {
        return (
          <Nav className="sidenavcategory" key={category.id}>
            <a
              href={category.link}
              onClick={() => this.handleNavClick(category)}
            >
              {category.name}
            </a>
            {this.displayCategoriesTree(category.id)}
          </Nav>
        );
      }
    });

    return categories;
  };
  handleNavClick = category => {
    this.setState({
      category: category
    });
  };
  render() {
    console.log(this.Products);
    const keyword = this.props.searchInput;
    const results = this.state.Products.filter(product =>
      product.name.toLowerCase().includes(keyword.toLowerCase())
    ).map(product => (
      <Link to={`/product/${product.id}`}>
        <Product
          key={product.id}
          imageBase64={product.imageBase64}
          discount={product.discount}
          name={product.name}
          price={product.pricePln}
        />
      </Link>
    ));

    return (
      <div className="searchresults">
        <div className="search_banner">
          <div className="search_banner_img" />
        </div>
        <div className="results">
          {keyword ? (
            <h2 style={{ textAlign: "left", marginLeft: "20px" }}>
              Wyniki wyszukiwania dla <i>"{this.props.searchInput}"</i>:
            </h2>
          ) : (
            ""
          )}
          <hr />
          {results.length < 1 ? (
            <div className="unfound_product">
              <p>Brak produktu o podanej nazwie</p>
            </div>
          ) : (
            results
          )}
        </div>
      </div>
    );
  }
}

export default SearchResultsPage;
