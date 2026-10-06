namespace CaseStudy_3_Library.DTOs.BorrowingDTO
{
    public class GetBorrowingDTO
    {
        public int Id { get; set; }
        public DateTime BorrowDate { get; set; }
        public DateTime? ReturnDate { get; set; }

        public string MemberName { get; set; }
        public string BookTitle { get; set; }
    }
}
