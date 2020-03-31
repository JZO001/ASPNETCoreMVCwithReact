import React, { Component } from 'react';
import { Route } from 'react-router';
import { Switch } from "react-router-dom";
import LandingPage from "./components/Landingpage";
import Article from "./components/Article/Article";

export default class App extends Component {
    render() {
        return (
            <div>
                <Switch>
                    <Route exact path='/' component={LandingPage} />
                    <Route path='/Article/:id' component={Article} />
                    <Route render={() => <h3 style={{ textAlign: 'center' }}>Requested content not found. Please check your URL. (React/client)</h3>} />
                </Switch>
            </div>
        );
    }
}
