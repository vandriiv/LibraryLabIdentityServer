import React, { Component } from 'react';
import { Form, Button, Col, Row, Alert } from 'react-bootstrap';
import './RegistrationPage.css';
import { compose } from '../../utils/';
import { bindActionCreators } from 'redux';
import { connect } from 'react-redux';
import { registration, resetMessage } from '../../../actions';
import { withUserService } from '../../hoc/';
import extractFormData from '../../../helpers/form-data-extract';

class RegistrationPage extends Component {

  componentDidMount() {
    this.props.resetMessage();
  }

  handleSubmit = (e) => {
    e.preventDefault();
    const formData = extractFormData(e.target);
    this.props.registration(formData);
  };

  mapMessage = (message) => {

    if(typeof message === 'string'){
      return message;
    }

    if ('Password' in message) {
      return (<ul>
        {message.Password.map(item => <li>{item}</li>)}
      </ul>);
    }
    
      return (<ul>
        {message.map(item => <li>{item.description}</li>)}
      </ul>
      );
    

  };

  render() {

    const { message } = this.props;

    return (
      <Row className="d-flex justify-content-center ">
        <Col xs={12}>
          <Col sm={10} md={6} lg={5} className="registration-form">
            <Form onSubmit={this.handleSubmit}>
              <Form.Group controlId="formBasicEmail">
                <Form.Label>Email address</Form.Label>
                <Form.Control type="email" placeholder="Enter email" name="email" required />
                <Form.Text className="text-muted">
                  We'll never share your email with anyone else.
              </Form.Text>
              </Form.Group>
              <Form.Group controlId="formBasicPassword">
                <Form.Label>Password</Form.Label>
                <Form.Control type="password" placeholder="Password" name="password" required
                />
              </Form.Group>
              <Form.Group controlId="formBasicPhoneNumber">
                <Form.Label>Phone number</Form.Label>
                <Form.Control type="text" placeholder="Phone number" name="phoneNumber" required
                  pattern="\d{3}[\-]\d{3}[\-]\d{4}" title="Use 123-456-7899 template" />
                <Form.Text className="text-muted">
                  We'll never share your phone number with anyone else.
              </Form.Text>
              </Form.Group>
              <Button className="registration-form__button" variant="outline-dark" type="submit">
                Registration
            </Button>
            </Form>
          </Col>
        </Col>
        {message ?
          <Col sm={10} md={6} lg={5} className="login-status">
            <Alert variant="primary">
              {this.mapMessage(message)}
            </Alert>
          </Col>
          : null}
      </Row>
    );
  }
}

const mapStateToProps = ({ userStatus }) => {
  return {
    message: userStatus.message
  }
};

const mapDispatchToProps = (dispatch, { userService }) => {
  return bindActionCreators({
    registration: registration(userService),
    resetMessage: resetMessage
  }, dispatch);
};

export default compose(
  withUserService(),
  connect(mapStateToProps, mapDispatchToProps)
)(RegistrationPage);