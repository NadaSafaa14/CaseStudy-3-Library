using CaseStudy_3_Library.Models;

namespace CaseStudy_3_Library.DTOs.BorrowingDTO
{
    public class AddBorrowingDTO
    {
        public DateTime BorrowDate { get; set; }
        public DateTime? ReturnDate { get; set; }

        public int MemberId { get; set; }

        public int BookId { get; set; }
    }
}
