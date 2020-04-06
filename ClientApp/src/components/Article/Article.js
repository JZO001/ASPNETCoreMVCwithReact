import React, { Component } from "react";
import { NavLink } from 'reactstrap';
import { Link } from 'react-router-dom';
import axios from "axios";

import Auxilary from "../Auxilary/Auxilary";

class Article extends Component {

    state = {
        isLoading: true,
        isError: false,
        article: null
    }

    componentDidMount() {
        axios.get('/ArticleData/' + this.props.match.params.id)
            .then((response) => {
                if (response.status === 200) {
                    this.setState({ isLoading: false, article: response.data });
                } else {
                    this.setState({ isLoading: false, isError: true });
                }
            },
                (error) => {
                    this.setState({ isLoading: false, isError: true });
                    console.log(error);
                })
    }

    static renderArticleContent(article) {
        return (
            <Auxilary>
                <section>
                    <header>
                        <h1>{article.header}</h1>
                    </header>
                </section>
                <section>
                    <main dangerouslySetInnerHTML={{ __html: article.content }}></main>
                </section>
            </Auxilary>
        );
    }

    render() {
        var content = "Loading article ...";
        if (this.state.isError) {
            content = "Ooops... something went wrong. Please visit back a little bit later.";
        } else if (!this.state.isLoading) {
            content = Article.renderArticleContent(this.state.article);
        }

        return (
            <Auxilary>
                {content}
                <br />
                <NavLink tag={Link} to={"/"}>Back to Home</NavLink>
            </Auxilary>
        );
    }

}

export default Article;
