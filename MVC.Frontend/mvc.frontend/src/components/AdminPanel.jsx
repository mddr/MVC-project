import React, { Component } from "react";
import Table from "./Table";

class AdminPanel extends Component {
  state = {};
  render() {
    return (
      /*whole layouy*/
      <div className="container-fluid">
        <div className="row content">
          <div className="col-sm-3 sidenav">
            {/*left side*/}
            <div class="panel panel-default">
              <div class="panel-body">
                <h4>Tabels</h4>
                <ul className="nav nav-pills nav-stacked">
                  <li className="active">
                    <a href="#section">Users</a>
                  </li>
                </ul>
                <br />
              </div>
            </div>
          </div>
          {/*right side*/}
          <div className="col-sm-9">
            <div class="panel panel-default">
              <div class="panel-body">
                <Table />
              </div>
            </div>
          </div>
        </div>
      </div>
    );
  }
}

export default AdminPanel;
