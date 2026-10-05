using System.ComponentModel.DataAnnotations;

namespace CaseStudy_3_Library.DTOs.BookDTO
{
    public class AddBookDTO
    {
        public string Title { get; set; }

        [Required]
        public string Author { get; set; }

        [Required]
        public string ISBN { get; set; }

        [Required, Range(0.01, double.MaxValue)]
        public decimal Price { get; set; }
        public bool IsAvailable { get; set; }

        public int CategoryId { get; set; }
    }
}
