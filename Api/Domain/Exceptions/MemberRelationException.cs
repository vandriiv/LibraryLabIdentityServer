using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Exceptions
{
    public class MemberRelationException : Exception
    {
        public MemberRelationException(string message,string column) : base(message)
        {
            Column = column;
        }

        public string Column { get; }
    }
}
