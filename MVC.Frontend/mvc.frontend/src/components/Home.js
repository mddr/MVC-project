import "./Home.css";

import React, { Component } from "react";
import { Col, Row } from "react-bootstrap";
import ProductSlider from "./ProductSlider";
import Footer from "./Footer";
import Product from "./Product";

export default class Home extends Component {
  constructor(props) {
    super(props);

    this.state = {
      isUserLogged: false
    };
  }

  render() {
    return (
      <div className="home">
        <div className="ad" />
        <div className="categories">
          Lorem ipsum dolor sit amet consectetur, adipisicing elit. Magni, illo?
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
        <div className="mostpopular">
          {/* <ProductSlider />
          <ProductSlider /> */}
        </div>
      </div>
      // <div className="home">
      //   <div className="container-fluid">
      //     <Row>
      //       <Col md={9} mdPush={3} className="products-container">
      //         <div className="products">
      //           <div className="products-header">Nowości</div>
      //           <ProductSlider />
      //         </div>
      //         <div className="products">
      //           <div className="products-header">Promocje</div>
      //           <ProductSlider />
      //         </div>
      //       </Col>

      //       <Col md={3} mdPull={9}>
      //         <div className="categories-container">
      //           <div className="products-header">Kategorie</div>
      //         </div>
      //       </Col>
      //     </Row>
      //   </div>
      // </div>
    );
  }
}
