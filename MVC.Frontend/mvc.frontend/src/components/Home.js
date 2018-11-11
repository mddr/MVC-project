import "./Home.css";

import React, { Component } from "react";
import ProductSlider from "./ProductSlider";
import { SideNav, Nav } from "react-sidenav";

import AuthService from '../services/AuthService';

export default class Home extends Component {
  constructor(props) {
    super(props);

    this.state = {
      isUserLogged: false,
      categories: [],
      Products: []
      };

    this.Auth = new AuthService();
    }

    componentDidMount() {
        this.fetchData();
    }

    fetchData() {
        this.Auth.fetch(`${this.Auth.domain}/products`, null
        ).then(res => res.json()).then(res => {
            this.setState({
                Products: res
            });
        });
        this.Auth.fetch(`${this.Auth.domain}/categories`, null
        ).then(res => res.json()).then(res => {
            this.setState({
                categories: res,
            });
        });
    }

  displayCategoriesTree = parentID => {
    const categories = this.state.categories.map(category => {
      if (category.superiorCategoryId === parentID) {
        return (
          <Nav className="sidenavcategory" key={category.id}>
            <a href={category.link}>{category.name}</a>
            {this.displayCategoriesTree(category.id)}
          </Nav>
        );
      }
    });

    return categories;
  };

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
          Promocje
          <hr />
          {
            <ProductSlider
              Products={this.state.Products}
              productsPerSlider={3}
            />
          }
        </div>
        <div className="news">
          Nowości
          <hr />
          {
            <ProductSlider
              Products={this.state.Products}
              productsPerSlider={3}
            />
          }
        </div>
        <div className="mostpopular">
          Top 10 produktów
          <hr />
          {
            <ProductSlider
              Products={this.state.Products}
              productsPerSlider={4}
            />
          }
        </div>
      </div>
    );
  }
}
