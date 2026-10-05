using System.ComponentModel.DataAnnotations;

namespace CaseStudy_3_Library.Models
{
    public class Borrowing
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public DateTime BorrowDate { get; set; }
        public DateTime? ReturnDate { get; set; }

        public int MemberId { get; set; }
        public Membeer Member { get; set; }

        public int BookId { get; set; }
        public Book Book { get; set; }
    }
}
