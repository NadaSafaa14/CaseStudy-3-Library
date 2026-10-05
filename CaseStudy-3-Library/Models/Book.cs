using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace CaseStudy_3_Library.Models
{
    [Index(nameof(ISBN),IsUnique = true)]
    public class Book
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Author { get; set; }

        [Required]
        public string ISBN { get; set; }

        [Required , Range(0.01 , double.MaxValue)]
        public decimal Price { get; set; }
        public bool IsAvailable { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; }

        public ICollection<Borrowing> Borrowings { get; set; } = new List<Borrowing>();

    }
}
