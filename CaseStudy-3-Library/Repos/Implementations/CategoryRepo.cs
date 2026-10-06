using CaseStudy_3_Library.Data;
using CaseStudy_3_Library.DTOs.CategoryDTO;
using CaseStudy_3_Library.Models;
using CaseStudy_3_Library.Repos.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CaseStudy_3_Library.Repos.Implementations
{
    public class CategoryRepo : GenericRepo<Category>, ICategoryRepo
    {
        protected readonly AppDbContext _context;
        public CategoryRepo(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public List<GetCategoryWithBooksCountDTO> GetCategoriesWithBookCount()
        {
            return _context.Categories
            .Select(c => new GetCategoryWithBooksCountDTO
            {
                Name = c.Name,
                BooksCount = c.Books.Count
            })
            .ToList();
        }
    }
}
