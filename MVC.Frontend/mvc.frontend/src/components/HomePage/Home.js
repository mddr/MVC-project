import "./Home.css";

import React, { Component } from "react";
import ProductSlider from "./ProductSlider";
import { SideNav, Nav } from "react-sidenav";

import AuthService from "../../services/AuthService";
import ProductService from "../../services/ProductService";
import CategoryService from "../../services/CategoryService";

export default class Home extends Component {
  constructor(props) {
    super(props);

    this.state = {
      isUserLogged: false,
      categories: [],
      Products: [],
      TopProducts: [],
      NewProducts: [],
      category: -1
    };

    this.Auth = new AuthService();
    this.ProductService = new ProductService();
		this.CategoryService = new CategoryService();
  }

  componentDidMount() {
    this.fetchData();
  }

  fetchData() {
		this.ProductService.getVisibleProducts()
      .then(res => res.json())
      .then(res => {
        this.setState({
          Products: res
        });
      });
    this.ProductService.getTop(10)
      .then(res => res.json())
      .then(res => {
        this.setState({
          TopProducts: res
        });
      });
    this.ProductService.getNewest(4)
      .then(res => res.json())
      .then(res => {
        this.setState({
          NewProducts: res
        });
			});
		this.CategoryService.getVisibleCategories()
      .then(res => res.json())
      .then(res => {
        this.setState({
          categories: res
        });
      });
  }
  displayCategoriesTree = parentID => {
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

  getFilteredProducts() {
    let products = this.state.Products;
    if (this.state.category === -1 || !this.state.category.superiorCategoryId)
      return products;
    return products.filter(product => {
      return product.categoryId === this.state.category.id;
    });
  }

  render() {
    return (
      <div className="home">
        <div className="banner">
          <div className="banner_img" />
        </div>
        <div className="categories">
          <SideNav className="sidemenu">
            {/* wywołanie z nullem bo "najwyższe" kategorie mają null w superiorCatId */}
            {this.displayCategoriesTree(null)}
          </SideNav>
        </div>
        <div className="sale">
          Produkty
          <hr />
          {
            <ProductSlider
              Products={this.getFilteredProducts()}
              productsPerSlider={3}
            />
          }
        </div>
        <div className="news">
          Nowości
          <hr />
          {
            <ProductSlider
              Products={this.state.NewProducts}
              productsPerSlider={3}
            />
          }
        </div>
        <div className="mostpopular">
          Top 10 produktów
          <hr />
          {
            <ProductSlider
              Products={this.state.TopProducts}
              productsPerSlider={4}
            />
          }
        </div>
      </div>
    );
  }
}
