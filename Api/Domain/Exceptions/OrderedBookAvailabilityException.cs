using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Exceptions
{
    public class OrderedBookAvailabilityException : Exception
    {
        public OrderedBookAvailabilityException(string message) : base(message)
        {
        }
    }
}
