import "./Home.css";

import React, { Component } from "react";
import ProductSlider from "./ProductSlider";
import { SideNav, Nav } from "react-sidenav";

export default class Home extends Component {
  constructor(props) {
    super(props);

    this.state = {
      isUserLogged: false,
      categories: [
        { id: 1, name: "Odzież męska", superiorCatId: null },
        { id: 2, name: "Spodnie", superiorCatId: 1 },
        { id: 3, name: "Buty", superiorCatId: 1 },
        { id: 4, name: "Buty sportowe", superiorCatId: 3 },
        { id: 5, name: "Pantofle", superiorCatId: 3 },
        { id: 6, name: "Odzież damska", superiorCatId: null }
      ]
    };
  }

  displayCategoriesTree = parentID => {
    const categories = this.state.categories.map(category => {
      if (category.superiorCatId === parentID) {
        return (
          <Nav key={category.id}>
            {category.name}
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
        <div className="ad">
          <div className="ad_img" />
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
          {<ProductSlider />}
        </div>
        <div className="news">
          Nowości
          <hr />
          {<ProductSlider />}
        </div>
        <div className="mostpopular">{}</div>
      </div>
    );
  }
}
