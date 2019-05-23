import React from "react";
import ReactDOM from "react-dom";
import { Provider } from "react-redux";
import "bootstrap/dist/css/bootstrap.min.css";
import "./index.css";
import App from "./components/App/App";
import * as serviceWorker from "./serviceWorker";
import store from "./store/store";
import { BookServiceProvider } from "./components/BookServiceProvider/BookServiceProvider";
import FetchApiService from "./services/FetchApiService";
import ErrorBoundry from "./components/error-boundry/error-boundry";
import UserService from "./services/UserService";
import { UserServiceProvider } from "./components/UserServiceProvider/UserServiceProvider";
const userManagerConfig = {
  client_id: 'react_spa',
  redirect_uri: 'http://localhost:3000/callback.html',
  response_type: 'token id_token',
  scope:"openid profile email api.read",
  authority: 'http://localhost:54990/',
  silent_redirect_uri: 'http://localhost:3000/',
  post_logout_redirect_uri:'http://localhost:3000/',
  automaticSilentRenew: true,
  filterProtocolClaims: true,
  loadUserInfo: true,
  monitorSession: true
};
const bookService = new FetchApiService();
const userService = new UserService(userManagerConfig);
ReactDOM.render(
  <ErrorBoundry>
    <Provider store={store}>
      <BookServiceProvider value={bookService}>
        <UserServiceProvider value = {userService}>
        <App />
        </UserServiceProvider>
      </BookServiceProvider>      
    </Provider>
  </ErrorBoundry>,
  document.getElementById("root")
);

serviceWorker.unregister();
