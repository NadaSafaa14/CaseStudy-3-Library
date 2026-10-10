using CaseStudy_3_Library.Data;
using CaseStudy_3_Library.Models;
using CaseStudy_3_Library.Repos.Interfaces;
using CaseStudy_3_Library.UoW.Interface;

namespace CaseStudy_3_Library.UoW.Implementation
{
    public class UnitOfWork : IUnitOfWork
    {
        protected readonly AppDbContext _context;

        public IBookRepo Books { get; }

        public IMemberRepo Members { get; }

        public ICategoryRepo Categories { get; }

        public IBorrowingRepo Borrowings { get; }

        public IUserRepo ApplicationUsers { get; }

        public IAuthRepo authRepo { get; }

        public UnitOfWork(AppDbContext context ,IAuthRepo authRepo , IBookRepo books , IMemberRepo members , ICategoryRepo categories , IBorrowingRepo borrowings , IUserRepo users)
        {
            _context = context;
            Books = books;
            Members = members;
            Categories = categories;
            Borrowings = borrowings;
            ApplicationUsers = users;
            this.authRepo = authRepo;
        }

        public int Save()
        {
            return _context.SaveChanges();
        }
    }
}
