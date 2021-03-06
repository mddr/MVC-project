import React, { Component } from "react";
import { Link } from "react-router-dom";

import Product from "../Products/Product";

import "./ProductSlider.css";

class ProductSlider extends Component {
  state = {
    sliderPosition: 0
  };

  handlePrevButtonClick = () => {
    let sliderPosition = this.state.sliderPosition;
    sliderPosition > 0 ? sliderPosition-- : (sliderPosition = 0);
    this.setState({ sliderPosition });
  };

  handleNextButtonClick = () => {
    const { Products, productsPerSlider } = this.props;
    let sliderPosition = this.state.sliderPosition;
    Math.floor(Products.length / productsPerSlider) < sliderPosition
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
          onClick={this.handlePrevButtonClick}
        >
          <span className="glyphicon glyphicon-chevron-left" />
        </button>
        {Products.filter(
          element =>
            Products.indexOf(element) >=
              (productsPerSlider * (this.state.sliderPosition + 1) >
              Products.length
                ? productsPerSlider * this.state.sliderPosition -
                  (productsPerSlider - (Products.length % productsPerSlider))
                : productsPerSlider * this.state.sliderPosition) &&
            Products.indexOf(element) <
              (this.state.sliderPosition + 1) * productsPerSlider
				).map(element => (
					<Link key={element.id} to={`/product/${element.id}`}>
            <Product
              key={element.id}
              imageBase64={element.imageBase64}
              discount={element.discount}
              name={element.name}
              price={element.pricePln}
            />
          </Link>
        ))}
        <button
          className="sliderRightButton"
          onClick={this.handleNextButtonClick}
        >
          <span className="glyphicon glyphicon-chevron-right" />
        </button>
      </div>
    );
  }
}

export default ProductSlider;
