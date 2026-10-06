using CaseStudy_3_Library.Data;
using CaseStudy_3_Library.Models;
using CaseStudy_3_Library.Repos.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CaseStudy_3_Library.Repos.Implementations
{
    public class BorrowingRepo : GenericRepo<Borrowing>, IBorrowingRepo
    {
        protected readonly AppDbContext _context;
        public BorrowingRepo(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public List<Borrowing> GetAllWithDetails()
        {
            return _context.Borrowings
            .Include(b => b.Member)
            .Include(b => b.Book)
            .OrderByDescending(b => b.BorrowDate)
            .ToList();
        }
    }
}
