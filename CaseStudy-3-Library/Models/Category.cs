using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace CaseStudy_3_Library.Models
{
    [Index(nameof(Name) , IsUnique = true)]
    public class Category
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [MaxLength(200)]
        public string Description { get; set; }

        public ICollection<Book> Books { get; set; } = new List<Book>();
    }
}
    