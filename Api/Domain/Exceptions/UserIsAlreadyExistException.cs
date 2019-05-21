using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Exceptions
{
    public class UserIsAlreadyExistException : Exception
    {
        public string Email { get; private set; }
        public UserIsAlreadyExistException(string email)
        {
            Email = email;
        }
    }
}
