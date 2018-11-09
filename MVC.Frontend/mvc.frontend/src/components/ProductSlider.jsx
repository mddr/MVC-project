import React, { Component } from "react";
import Product from "./Product";

import "./ProductSlider.css";

class ProductSlider extends Component {
  state = {
    productsPerSlider: 3,
    sliderPosition: 0,
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

  handleLeftButtonClick = () => {
    let sliderPosition = this.state.sliderPosition;
    sliderPosition > 0 ? sliderPosition-- : (sliderPosition = 0);
    this.setState({ sliderPosition });
  };

  handleRightButtonClick = () => {
    let sliderPosition = this.state.sliderPosition;
    Math.floor(this.state.Products.length) / this.state.productsPerSlider <
    sliderPosition
      ? sliderPosition++
      : (sliderPosition = Math.floor(
          this.state.Products.length / this.state.productsPerSlider
        ));
    this.setState({ sliderPosition });
  };

  render() {
    return (
      <div className="productslider">
        <button
          className="sliderLeftButton"
          onClick={this.handleLeftButtonClick}
        >
          <span className="glyphicon glyphicon-chevron-left" />
        </button>
        {this.state.Products.filter(
          element =>
            this.state.Products.indexOf(element) >=
              this.state.sliderPosition * this.state.productsPerSlider &&
            this.state.Products.indexOf(element) <
              (this.state.sliderPosition + 1) * this.state.productsPerSlider
        ).map(element => (
          <Product
            imgpath={element.imgpath}
            discount={element.discount}
            name={element.name}
            price={element.price}
          />
        ))}
        <button
          className="sliderRightButton"
          onClick={this.handleRightButtonClick}
        >
          <span className="glyphicon glyphicon-chevron-right" />
        </button>
      </div>
    );
  }
}

export default ProductSlider;
