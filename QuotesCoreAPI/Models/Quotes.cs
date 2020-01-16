using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuotesCoreAPI.Models
{
    public class Quotes
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Author { get; set; }
        [Required]
        public string Quote { get; set; }
        [Required]
        public string Category { get; set; }
    }
}
