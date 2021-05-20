using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace my_books.Models.InputModel
{
    public class BookInputModel
    {
        [Required]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Field has to been between 3 and 100 characters")]
        public string Title { get; set; }
        [Required]
        [StringLength(300, MinimumLength = 3, ErrorMessage = "Field has to been between 3 and 300 characters")]
        public string Description { get; set; }
        public string Genre { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Field has to been between 3 and 100 characters")]
        public string Author { get; set; }
        [Required]
        public bool IsRead { get; set; }
        public DateTime? DateRead { get; set; }
        public int? Rate { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Field has to been between 3 and 100 characters")]
        public string Publisher { get; set; }
        
    }
}