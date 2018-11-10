import React, { Component } from "react";
import Product from "./Product";

import "./ProductSlider.css";

class ProductSlider extends Component {
  state = {
    sliderPosition: 0
  };

  handleLeftButtonClick = () => {
    let sliderPosition = this.state.sliderPosition;
    sliderPosition > 0 ? sliderPosition-- : (sliderPosition = 0);
    this.setState({ sliderPosition });
  };

  handleRightButtonClick = () => {
    const { Products, productsPerSlider } = this.props;
    let sliderPosition = this.state.sliderPosition;
    Math.floor(Products.length) / productsPerSlider < sliderPosition
      ? sliderPosition++
      : (sliderPosition = Math.floor(Products.length / productsPerSlider));
    this.setState({ sliderPosition });
  };

  render() {
    const { Products, productsPerSlider } = this.props;
    return (
      <div className="productslider">
        <button
          className="sliderLeftButton"
          onClick={this.handleLeftButtonClick}
        >
          <span className="glyphicon glyphicon-chevron-left" />
        </button>
        {Products.filter(
          element =>
            Products.indexOf(element) >=
              this.state.sliderPosition * Products.length &&
            Products.indexOf(element) <
              (this.state.sliderPosition + 1) * productsPerSlider
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
