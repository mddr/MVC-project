import React, { Component } from "react";
import Product from "./Product";

import "./ProductSlider.css";

class ProductSlider extends Component {
  state = {};
  render() {
    return (
      <div className="productslider">
        <button>
          <span className="glyphicon glyphicon-chevron-left" />
        </button>
        <Product />
        <Product />
        <Product />
        <button>
          <span className="glyphicon glyphicon-chevron-right" />
        </button>
      </div>
    );
  }
}

export default ProductSlider;
