using Domain.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryLab.ViewModels
{   
    public class UserBooksViewModel
    {        
        public User User { get; set; }     
        
        public IEnumerable<BookCount> Books { get; set; }
    }

    public class BookCount
    {
        public Book Book { get; set; }
        public int Count { get; set; }
    }
}
