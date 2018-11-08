import React, { Component } from "react";
import Product from "./Product";

import "./ProductSlider.css";

class ProductSlider extends Component {
  state = {
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
        price: 10.5
      },
      {
        imgpath: "nie wiem jak to działa",
        discount: true,
        name: "Koszulka",
        price: 10.5
      }
    ]
  };
  render() {
    return (
      <div className="productslider">
        <button>
          <span className="glyphicon glyphicon-chevron-left" />
        </button>
        {this.state.Products.map(element => (
          <Product
            imgpath={element.imgpath}
            discount={element.discount}
            name={element.name}
            price={element.price}
          />
        ))}
        <button>
          <span className="glyphicon glyphicon-chevron-right" />
        </button>
      </div>
    );
  }
}

export default ProductSlider;
