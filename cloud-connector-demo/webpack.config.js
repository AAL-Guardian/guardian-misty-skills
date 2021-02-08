const path = require('path');
const webpack = require('webpack');
const CopyPlugin = require("copy-webpack-plugin");

module.exports = {
  mode: 'production',
  entry: './src/misty-connect.ts',
  // devtool: slsw.lib.webpack.isLocal ? 'source-map' : undefined,
  resolve: {
    extensions: [
      '.js',
      '.json',
      '.ts',
      '.tsx'
    ]
  },
  output: {
    filename: 'misty-connect.js',
    path: path.resolve(__dirname, 'dist'),
  },
  // externals: [nodeExternals({
  //   allowlist: [ 'mqtt' ]
  // })],
  target: [ 'web', 'es5'],
  module: {
    rules: [
      // { test: /\.js$/, loader: 'shebang-loader' },
      {
        test: /\.ts(x?)$/,
        use: [
          {
            loader: 'ts-loader'
          }
        ],
      },
    ]
  },
  plugins: [
    // new webpack.DefinePlugin({
    //   'process.env.NODE_ENV': JSON.stringify('development')
    // }),
    new webpack.ProvidePlugin({    
      console: 'console-polyfill',
      'window.console': 'console-polyfill',
      Buffer: ['buffer', 'Buffer'],
      process: 'process',
    }),
    new CopyPlugin({
      patterns: [
        { from: "misty-connect.json", to: "misty-connect.json" },
      ],
      options: {
        concurrency: 100,
      },
    }),
  ],
};