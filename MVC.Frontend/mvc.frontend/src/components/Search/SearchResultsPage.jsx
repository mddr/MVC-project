import "./SearchResultsPage.css";
import React, { Component } from "react";
import { Link } from "react-router-dom";
import { Nav } from "react-sidenav";
import Product from "../Products/Product";
import AuthService from "../../services/AuthService";
import Pagination from "react-js-pagination";

class SearchResultsPage extends Component {
  constructor(props) {
    super(props);
    this.state = {
      Products: [],
      categories: [],
      activePage: 1,
      productsPerPage: 3
    };
    this.Auth = new AuthService();
  }
  componentDidMount() {
    this.fetchData();
    this.setState({productsPerPage: this.props.userInfo.productsPerPage})
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

  handlePageChange = activePage => {
    this.setState({activePage});
  } 

  render() {
    const keyword = this.props.searchInput;
    let results = this.state.Products.filter(product =>
      product.name.toLowerCase().includes(keyword.toLowerCase())
    );
    
    let searchResultsLength = results.length;
    
    results = results.filter(product => 
      results.indexOf(product) < this.state.activePage*this.state.productsPerPage 
      && results.indexOf(product) >= (this.state.activePage-1)*this.state.productsPerPage
    )
    results = results.map(product => (
      <Link to={`/product/${product.id}`} key={product.id}>
        <Product
          imageBase64={product.imageBase64}
          discount={product.discount}
          name={product.name}
          price={product.pricePln}
        />
      </Link>
    ));

    console.log(this.props.userInfo);

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
          {searchResultsLength < 1 ? (
            <div className="unfound_product">
              <p>Brak produktu o podanej nazwie</p>
            </div>
          ) : (
            results
          )}            
        </div>
        <div className="paginator-wrapper">
          <Pagination
              totalItemsCount= {searchResultsLength}
              onChange= {this.handlePageChange}
              activePage = {this.state.activePage}
              itemsCountPerPage = { this.state.productsPerPage }
              style = {{margin: "auto"}}
            />
        </div>
      </div>
    );
  }
}

export default SearchResultsPage;
