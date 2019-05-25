using System;
using System.Collections.Generic;

namespace Domain.Models
{
    public class User
    {     

        public long Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        public string Role { get; set; }
     
    }
}
