using System;
using System.Collections.Generic;

namespace DataEFCore.Entities
{
    public partial class User
    {
        public User()
        {
            UserBook = new HashSet<UserBook>();
        }

        public long Id { get; set; }
        public long RoleId { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }

        public virtual ICollection<UserBook> UserBook { get; set; }
    }
}
