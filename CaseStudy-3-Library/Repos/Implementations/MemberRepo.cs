using CaseStudy_3_Library.Data;
using CaseStudy_3_Library.Models;
using CaseStudy_3_Library.Repos.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CaseStudy_3_Library.Repos.Implementations
{
    public class MemberRepo : GenericRepo<Membeer>, IMemberRepo
    {
        protected readonly AppDbContext _context;
        public MemberRepo(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public List<Membeer> GetTopReaders()
        {
            return _context.Members
            .Include(m => m.Borrowings)
            .OrderByDescending(m => m.Borrowings.Count)
            .Take(5)
            .ToList();
        }

        public Membeer GetWithStatistics(int id)
        {
            return _context.Members
            .Include(m => m.Borrowings)
            .FirstOrDefault(m => m.Id == id);
        }
    }
}
