using CaseStudy_3_Library.Data;
using CaseStudy_3_Library.DTOs.BookDTO;
using CaseStudy_3_Library.Models;
using CaseStudy_3_Library.Repos.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CaseStudy_3_Library.Repos.Implementations
{
    public class BookRepo : GenericRepo<Book>, IBookRepo
    {
        protected readonly AppDbContext _coontext;
        public BookRepo(AppDbContext context) : base(context)
        {
            _coontext = context;
        }

        public List<Book> GetAvailableBooks()
        {
            return _context.Books
                .Where(b => b.IsAvailable)
                .ToList();
        }

        public List<Book> GetBooksByCategory(int categoryId)
        {
            return _context.Books
                .Where(b => b.CategoryId == categoryId)
                .ToList();
        }

        public Book GetMostExpensive()
        {
            return _context.Books
                .OrderByDescending(b => b.Price)
                .FirstOrDefault();
        }

        public List<BookSearchDTO> Search(string keyword)
        {
            return _context.Books
                .Where(b =>
                    b.Title.ToLower().Contains(keyword.ToLower()) || b.Author.ToLower().Contains(keyword.ToLower()))
                .OrderBy(b => b.Title)
                .Select(b => new BookSearchDTO
                {
                    Id = b.Id,
                    Title = b.Title,
                    Author = b.Author,
                    Price = b.Price
                })
                .ToList();
        }
    }
}
