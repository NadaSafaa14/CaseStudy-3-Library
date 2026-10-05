using CaseStudy_3_Library.Data;
using CaseStudy_3_Library.Models;
using CaseStudy_3_Library.Repos.Interfaces;
using CaseStudy_3_Library.UoW.Interface;

namespace CaseStudy_3_Library.UoW.Implementation
{
    public class UnitOfWork : IUnitOfWork
    {
        protected readonly AppDbContext _context;

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
        }

        public IBookRepo Books { get; }

        public IGenericRepo<Membeer> Members { get; }

        public IGenericRepo<Category> Categories { get; }

        public IGenericRepo<Borrowing> Borrowings { get; }

        public int Save()
        {
            return _context.SaveChanges();
        }
    }
}
