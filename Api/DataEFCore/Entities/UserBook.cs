using System;
using System.Collections.Generic;

namespace DataEFCore.Entities
{
    public partial class UserBook
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public long BookId { get; set; }
        public int Count { get; set; }

        public virtual Book Book { get; set; }
        public virtual User User { get; set; }
    }
}
