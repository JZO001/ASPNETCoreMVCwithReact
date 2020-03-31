import React, { Component } from "react";
import { NavLink } from 'reactstrap';
import { Link } from 'react-router-dom';

class LandingPage extends Component {
    render() {
        return (
            <div>
                <p>Landing page</p>
                <NavLink tag={Link} to={"/article/1"}>Article 1 - How to ...</NavLink><br />
                <NavLink tag={Link} to={"/article/2"}>Article 2 - Make it yourself</NavLink>
            </div>
        );
    }
}

export default LandingPage;
