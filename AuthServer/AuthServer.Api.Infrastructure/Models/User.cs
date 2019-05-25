using System;
using System.Collections.Generic;

namespace AuthServer.Api.Infrastructure.Models
{
    public partial class User
    {       

        public long Id { get; set; }
        public string Role { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }       
    }
}
