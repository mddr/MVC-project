import './Home.css';

import React, { Component } from 'react';
import { Col, Row } from 'react-bootstrap';

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
        <div className="container-fluid">
          <Row>
            <Col md={9} mdPush={3} className="products-container">
              <div className="products">
                <div className="products-header">Nowości</div>
              </div>
              <div className="products">
                <div className="products-header">Promocje</div>
              </div>
            </Col>

            <Col md={3} mdPull={9}>
              <div className="categories-container">
                <div className="products-header">Kategorie</div>
              </div>
            </Col>
          </Row>
        </div>
      </div>
    );
  }
}
