const path = require("path");
const webpack = require("webpack");
const Merge = require("webpack-merge");
const SharedConfig = require("./webpack.shared.js");

module.exports = Merge(SharedConfig, {
    mode: "development",

    devtool: "cheap-module-eval-source-map",

    watch: true,

    watchOptions: {
        ignored: /node_modules/,
        aggregateTimeout: 1000,
        poll: 1000
    },

    output: {
        filename: "[name].js",
        path: path.resolve(__dirname, "wwwroot"),
        publicPath: "/wwwroot",
        //library: "aspAndWebpack",
        //libraryTarget: "var"
    },

    plugins: [
        new webpack.DefinePlugin({
            "process.env": {
                "NODE_ENV": JSON.stringify("development")
            }
        })
    ]
});
