using LibraryLab.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryLab.ViewModels
{
    public class NewBookViewModel
    {
        public long Id { get; set; }
        [Required]
        [MaxLength(40)]
        public string Title { get; set; }
        public int? Year { get; set; }
        [Required]
        [Range(0,int.MaxValue)]
        public long AuthorId { get; set; }
        [Required]
        [Range(0, int.MaxValue)]
        [BookCount("AvailableCount", ErrorMessage = "Available count should be less or equal than total count")]
        public int TotalCount { get; set; }
        [Required]
        [Range(0, int.MaxValue)]       
        public int AvailableCount { get; set; }
        [Required]
        [Range(0, int.MaxValue)]
        public long GenreId { get; set; }
    }
}
