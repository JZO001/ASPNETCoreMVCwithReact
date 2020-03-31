const path = require("path");
const webpack = require("webpack");
const Merge = require("webpack-merge");
const SharedConfig = require("./webpack.shared.js");
const MiniCssExtractPlugin = require('mini-css-extract-plugin');

// Images, Fonts Loading: https://webpack.js.org/guides/asset-management/
// LESS Loading: https://webpack.js.org/loaders/less-loader/
// Code splitting: https://webpack.js.org/guides/code-splitting
// Caching: https://webpack.js.org/guides/caching/

module.exports = Merge(SharedConfig, {
    mode: "production",

    devtool: "cheap-source-map",

    output: {
        filename: "[name].[chunkhash].js",
        path: path.resolve(__dirname, "wwwroot"),
        publicPath: "",
        //library: "aspAndWebpack",
        //libraryTarget: "var"
    },

    plugins: [
        new webpack.DefinePlugin({
            "process.env": {
                "NODE_ENV": JSON.stringify("production")
            }
        }),

        // Split out library into seperate bundle and remove from app bundle.
        new webpack.HashedModuleIdsPlugin(),

        // Write out CSS bundle to its own file:
        new MiniCssExtractPlugin({
            // Options similar to the same options in webpackOptions.output
            // all options are optional
            filename: '[name].css',
            chunkFilename: '[id].css',
            ignoreOrder: false // Enable to remove warnings about conflicting order
        }),

        new webpack.LoaderOptionsPlugin({
            minimize: true,
            debug: false
        })
    ]
});
