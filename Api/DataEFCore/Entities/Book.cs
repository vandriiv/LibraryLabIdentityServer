using System;
using System.Collections.Generic;

namespace DataEFCore.Entities
{
    public partial class Book
    {
        public Book()
        {
            UserBook = new HashSet<UserBook>();
        }

        public long Id { get; set; }
        public int Year { get; set; }
        public string Title { get; set; }
        public long AuthorId { get; set; }
        public int TotalCount { get; set; }
        public int AvailableCount { get; set; }
        public long GenreId { get; set; }

        public virtual Author Author { get; set; }
        public virtual Genre Genre { get; set; }
        public virtual ICollection<UserBook> UserBook { get; set; }
    }
}
