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
        { id: 1, name: "Odzież męska", link: "", superiorCatId: null },
        { id: 2, name: "Spodnie", link: "", superiorCatId: 1 },
        { id: 3, name: "Buty", link: "", superiorCatId: 1 },
        { id: 4, name: "Buty sportowe", link: "", superiorCatId: 3 },
        { id: 5, name: "Pantofle", link: "", superiorCatId: 3 },
        { id: 6, name: "Odzież damska", link: "", superiorCatId: null }
      ],
      Products: [
        {
          imgpath: "nie wiem jak to działa",
          discount: true,
          name: "Koszulka",
          price: 10.5
        },
        {
          imgpath: "nie wiem jak to działa",
          discount: true,
          name: "Koszulka",
          price: 20.5
        },
        {
          imgpath: "nie wiem jak to działa",
          discount: true,
          name: "Koszulka",
          price: 30.5
        },
        {
          imgpath: "nie wiem jak to działa",
          discount: true,
          name: "Koszulka",
          price: 40.5
        },
        {
          imgpath: "nie wiem jak to działa",
          discount: true,
          name: "Koszulka",
          price: 50.5
        }
      ]
    };
  }

  displayCategoriesTree = parentID => {
    const categories = this.state.categories.map(category => {
      if (category.superiorCatId === parentID) {
        return (
          <Nav key={category.id}>
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
