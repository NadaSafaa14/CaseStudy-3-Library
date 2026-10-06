using CaseStudy_3_Library.Models;

namespace CaseStudy_3_Library.Repos.Interfaces
{
    public interface IBorrowingRepo : IGenericRepo<Borrowing>
    {
        List<Borrowing> GetAllWithDetails();
    }
}
