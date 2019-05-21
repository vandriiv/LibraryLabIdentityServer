using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryLab.ViewModels
{
    public class UpdateUserBookViewModel
    {
        public long UserId { get; set; }
        public long BookId { get; set; }
        public int NewCount { get; set; }
        public int OldCount { get; set; }
    }
}
