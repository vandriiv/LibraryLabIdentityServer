import React, { Component } from "react";
import { Navbar, Nav, NavDropdown, Container } from "react-bootstrap";
import { FaUser, FaUserEdit, FaBook, FaShoppingCart, FaSignOutAlt } from "react-icons/fa";
import { LinkContainer} from "react-router-bootstrap";
import "./NavMenu.css";
import { connect } from 'react-redux';
import { logout } from '../../actions';
import { login } from '../../actions';
import { bindActionCreators } from 'redux';
import { compose } from "../utils";
import { withUserService } from '../hoc/';
class NavMenu extends Component {
  checkActive = route => (match, location) => {
    if (!location) return false;
    const { pathname } = location;
    if (route === "/") {
      return pathname === "/";
    }
   
    return pathname ===route;
  };

  render() {
    const { isLoggedIn, logout,login,roleName } = this.props;   
    
    const adminNav = roleName === "admin" ? (<NavDropdown title="Admin panel" id="admin-nav-dropdown">
      <LinkContainer
        to={"/admin/books"}
        isActive={this.checkActive("/admin/books")}
        exact
      >
        <NavDropdown.Item>
          Books edit
        </NavDropdown.Item>
      </LinkContainer>
      <LinkContainer
        to={"/admin/authors"}
        isActive={this.checkActive("/admin/authors")}
        exact
      >
        <NavDropdown.Item>
          Authors edit
        </NavDropdown.Item>
      </LinkContainer>
      <LinkContainer
        to={"/admin/userbooks"}
        isActive={this.checkActive("/admin/userbooks")}
        exact
      >
        <NavDropdown.Item>
          User books edit
        </NavDropdown.Item>
      </LinkContainer>

    </NavDropdown>) : null;

    return (
      <Navbar variant="dark" bg="dark" sticky="top" expand="lg">
        <Container>
          <Navbar.Brand href="/home">
            <FaBook size={20} style={{ marginBottom: 5, marginRight: 10 }} />
            City Library
          </Navbar.Brand>
          <Navbar.Toggle aria-controls="basic-navbar-nav" />
          <Navbar.Collapse id="basic-navbar-nav">
            <Nav className="mr-auto">
              <LinkContainer to={"/"} isActive={this.checkActive("/")} exact>
                <Nav.Link active={false}>Home</Nav.Link>
              </LinkContainer>
              <LinkContainer
                to={"/books/"}
                isActive={this.checkActive("/books/")}
                exact
              >
                <Nav.Link>All books</Nav.Link>
              </LinkContainer>
              <LinkContainer
                to={"/authors/"}
                isActive={this.checkActive("/authors/")}
                exact
              >
                <Nav.Link active={false}>Authors</Nav.Link>
              </LinkContainer>
              <NavDropdown title="Genres" id="basic-nav-dropdown">
                <LinkContainer isActive={this.checkActive("/books/bygenre/thriller")} exact to="/books/bygenre/thriller"><NavDropdown.Item >Thriller</NavDropdown.Item></LinkContainer>
                <LinkContainer isActive={this.checkActive("/books/bygenre/novel")} exact to={"/books/bygenre/novel"}><NavDropdown.Item >Novel</NavDropdown.Item></LinkContainer>
                <LinkContainer isActive={this.checkActive("/books/bygenre/adventure")} exact to={"/books/bygenre/adventure"}><NavDropdown.Item >Adventure</NavDropdown.Item></LinkContainer>
                <LinkContainer isActive={this.checkActive("/books/bygenre/sci-fi")} exact to={"/books/bygenre/sci-fi"}><NavDropdown.Item >Sci-Fi</NavDropdown.Item></LinkContainer>
                <LinkContainer isActive={this.checkActive("/books/bygenre/detective")} exact to={"/books/bygenre/detective"}><NavDropdown.Item >Detective</NavDropdown.Item></LinkContainer>
                <LinkContainer isActive={this.checkActive("/books/bygenre/memoir")} exact to={"/books/bygenre/memoir"}><NavDropdown.Item >Memoir</NavDropdown.Item></LinkContainer>
              </NavDropdown>

              <LinkContainer
                to={"/cart"}
                isActive={this.checkActive("/cart")}
                exact
              >
                <Nav.Link active={false}>
                  <FaShoppingCart size={20} style={{ marginRight: 10 }} />
                  Cart
                </Nav.Link>
              </LinkContainer>
              {isLoggedIn ?                
                  <Nav.Link onClick={logout}>
                    <FaSignOutAlt size={20} style={{ marginRight: 10 }} />
                    Logout
              </Nav.Link>               
                :              
                  <Nav.Link onClick={login} >
                    <FaUser size={20} style={{ marginRight: 10 }} />
                    Login
                </Nav.Link>
                }
              <LinkContainer
                to={"/registration"}
                isActive={this.checkActive("/registration")}
                exact
              >
                <Nav.Link active={false}>
                  <FaUserEdit size={24} style={{ marginRight: 10 }} />
                  Registration
                </Nav.Link>
              </LinkContainer>
              {isLoggedIn ?
                <LinkContainer
                  to={"/orderedBooks"}
                  isActive={this.checkActive("/orderedBooks")}
                  exact                 
                >
                  <Nav.Link active={false}>                   
                    Ordered books
              </Nav.Link>
                </LinkContainer>
                :
                null}
              {adminNav}
            </Nav>
          </Navbar.Collapse>
        </Container>
      </Navbar>
    );
  }
}

const mapDispatchToProps = (dispatch,{userService}) => { 
  return bindActionCreators({
    logout: logout(userService),
    login:login(userService)
  }, dispatch);
};

const mapStateToProps = ({ userStatus }) => {
  return {
    isLoggedIn: userStatus.isLoggedIn,
    roleName: userStatus.roleName
  }
};

export default compose(withUserService(),connect(mapStateToProps, mapDispatchToProps))(NavMenu);