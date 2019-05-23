import React from 'react';
import {  UserServiceConsumer } from '../UserServiceProvider/UserServiceProvider';

const withUserService = () => (Wrapped) => {

  return (props) => {
    return (
      <UserServiceConsumer>
        {
          (userService) => {
            return (<Wrapped {...props}
                     userService={userService}/>);
          }
        }
      </UserServiceConsumer>
    );
  }
};

export default withUserService;