const path = require('path');
const ExtractTextPlugin = require("extract-text-webpack-plugin");
const { CleanWebpackPlugin } = require('clean-webpack-plugin');
const HtmlWebpackPlugin = require('html-webpack-plugin');
const CopyWebpackPlugin = require('copy-webpack-plugin');
const MiniCssExtractPlugin = require('mini-css-extract-plugin');
var DashboardPlugin = require("webpack-dashboard/plugin");
require("@babel/polyfill");

const isDev = process.env.NODE_ENV === 'development';
const isProd = !isDev;

const fileName = isDev ? '[name]' : '[name].[contenthash]';

console.error('Is dev mode: ', isDev);


module.exports = {
    context: path.resolve(__dirname, 'src'),
    entry: ["@babel/polyfill", "@root/scripts/app.js"],
    output: {
        path: path.resolve(__dirname, 'dist'),
        filename: `${fileName}.js`,
        publicPath: '/'
    },
    module: {
        rules: [
            {
                test: /\.(css|scss)$/,
                use: [
                    {
                        loader: MiniCssExtractPlugin.loader,
                        options: {
                            hmr: isDev,
                            reloadAll: isDev,
                        }
                    }, 'css-loader', 'sass-loader'
                ]
            },
            {
                test: /\.(png|svg|jpg|gif|ico)$/,
                use: {
                    loader: 'file-loader',
                }
            },
            {
                test: /\.(ttf)$/,
                use: {
                    loader: 'file-loader',
                }
            },
            {
                test: /\.m?js$/,
                exclude: /(node_modules|bower_components)/,
                use: {
                    loader: 'babel-loader',
                    options: {
                        presets: [
                            "@babel/preset-env",
                            "@babel/preset-react"
                        ]

                    }
                }
            },
        ],
    },
    plugins: [
        new DashboardPlugin({ port: process.env.PORT, host: process.env.HOST }),
        new ExtractTextPlugin('style.css'),
        new HtmlWebpackPlugin({
            template: '../index.html',
            inject: 'body',
            minify: {
                collapseWhitespace: isProd,
            }
        }),
        new CleanWebpackPlugin(),
        new CopyWebpackPlugin([
            {
                from: path.resolve(__dirname, 'node_modules/swiper/css/swiper.min.css'),
                to: path.resolve(__dirname, 'dist'),
            }
        ]),
        new MiniCssExtractPlugin({
            filename: `${fileName}.css`
        }),
    ],
    optimization: {
        splitChunks: {
            chunks: 'all'
        }
    },
    resolve: {
        alias: {
            '@root': path.resolve(__dirname, 'src'),
            '@img': path.resolve(__dirname, 'src/img'),
        },
        extensions: ['.ts', '.tsx', '.js']
    },
    devServer: {
        stats: "errors-only",
        contentBase: "./",
        host: process.env.HOST, // Defaults to `localhost`
        port: process.env.PORT, // Defaults to 8080
        open: true,
    },
};