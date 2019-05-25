export default class FetchApiService {
  _apiBase = "http://localhost:5100/api/";

  getRequest = async url => {

    let headers = new Headers();
    headers.append('Content-Type','application/json');
    headers.append('Accept','application/json');

    const authHeader = localStorage.getItem("token")
      ? "Bearer " + localStorage.getItem("token")
      : "";

    if(authHeader!==""){
      headers.append('Authorization',authHeader);
    }
    const res = await fetch(`${this._apiBase}${url}`, {
      method: "GET",
      credentials: "same-origin",
      headers: headers
    });
    if (!res.ok) {
      var errorMsg = await res.json();

      throw errorMsg;
    }
    return await res.json();
  };

  postRequest = async (url, body) => {
    let headers = new Headers();
    headers.append('Content-Type','application/json');
    headers.append('Accept','application/json');
    
    const authHeader = localStorage.getItem("token")
      ? "Bearer " + localStorage.getItem("token")
      : "";

    if(authHeader!==""){
      headers.append('Authorization',authHeader);
    }
    const res = await fetch(`${this._apiBase}${url}`, {
      method: "POST",
      credentials: "same-origin",
      headers: headers        ,
      body: JSON.stringify(body)
    });
    if (!res.ok) {
      var errorMsg = await res.json();

      throw errorMsg;
    }

    return await res.json();
  };

  putRequest = async (url, body) => {
    let headers = new Headers();
    headers.append('Content-Type','application/json');
    headers.append('Accept','application/json');
    
    const authHeader = localStorage.getItem("token")
      ? "Bearer " + localStorage.getItem("token")
      : "";

    if(authHeader!==""){
      headers.append('Authorization',authHeader);
    }
    const res = await fetch(`${this._apiBase}${url}`, {
      method: "PUT",
      credentials: "same-origin",
      headers: headers,
      body: JSON.stringify(body)
    });
    if (!res.ok) {
      var errorMsg = await res.json();

      throw errorMsg;
    }

    return await res.json();
  };

  checkUserStatusRequest = async url => {
    let headers = new Headers();
    headers.append('Content-Type','application/json');
    headers.append('Accept','application/json');
    
    const authHeader = localStorage.getItem("token")
      ? "Bearer " + localStorage.getItem("token")
      : "";

    if(authHeader!==""){
      headers.append('Authorization',authHeader);
    }
    const res = await fetch(`${this._apiBase}${url}`, {
      method: "GET",
      credentials: "same-origin",
      headers:headers
    });
    if (!res.ok) {
      throw new Error("Not authorized");
    }

    return await res.json();
  };

  loginRequest = async (url, body) => {

    let headers = new Headers();
    headers.append('Content-Type','application/json');
    headers.append('Accept','application/json');
    
    const authHeader = localStorage.getItem("token")
      ? "Bearer " + localStorage.getItem("token")
      : "";

    if(authHeader!==""){
      headers.append('Authorization',authHeader);
    }

    const res = await fetch(`${this._apiBase}${url}`, {
      method: "POST",
      credentials: "same-origin",
      headers: headers,
      body: JSON.stringify(body)
    });

    if (!res.ok) {
      var errorMsg = await res.json();

      throw errorMsg;
    }

      return await res.json();
  };

  getBooksRange = async (limit, offset) => {
    const path = `books?limit=${limit}&offset=${offset}`;

    const result = await this.getRequest(path);

    return result;
  };

  getAuthors = async () => {
    const path = "authors";

    const result = await this.getRequest(path);

    return result;
  };

  getBooksByGenre = async (genre, limit, offset) => {
    const path = `books/${genre}/genres?limit=${limit}&offset=${offset}`;

    const result = await this.getRequest(path);

    return result;
  };

  getBooksByAuthorId = async (authorId, limit, offset) => {
      const path = `books/${authorId}/authors?limit=${limit}&offset=${offset}`;

    const result = await this.getRequest(path);

    return result;
  };

  getBooksByTitle = async (title,limit,offset)=>{
    const path = `books/${title}?limit=${limit}&offset=${offset}`;

    const result = await this.getRequest(path);

    return result;
  };

  login = async userLoginData => {
    const path = "session";

    const result = await this.loginRequest(path, userLoginData);

    return result;
  };

  registration = async userRegistrationData => {
    const path = "users";

    const result = await this.postRequest(path, userRegistrationData);

    return result;
  };

  checkIsLoggedIn = async () => {
    const path = "userStatus";

    const result = await this.checkUserStatusRequest(path);

    return result;
  };

  makeOrder = async orderData => {
    const path = "orders";

    const result = await this.postRequest(path, orderData);

    return result;
  };

  getAvailability = async cartItems => {
    const path = "books/availableCount";

    const result = await this.postRequest(path, cartItems);

    return result;
  };

  addAuthor = async author => {
    const path = "authors";

    const result = await this.postRequest(path, author);

    return result;
  };

  updateAuthor = async author=>{
      const path = "authors";

    const result = await this.putRequest(path,author);
    
    return result;
  };

  addBook = async book => {
    const path = "books";

    const result = await this.postRequest(path, book);

    return result;
  };

  updateBook = async book=>{
    const path = "books";

    const result = await this.putRequest(path,book);
    
    return result;
  };

  getUserBooksByEmail = async email=>{
    const path = `orders/${email}`;

    const result = await this.getRequest(path);

    return result;
  };

  updateUserBook = async userBook=>{
    const path = 'orders';

    const result = await this.putRequest(path,userBook);

    return result;

  };
}
