using CaseStudy_3_Library.DTOs.CategoryDTO;
using CaseStudy_3_Library.Models;

namespace CaseStudy_3_Library.Repos.Interfaces
{
    public interface ICategoryRepo : IGenericRepo<Category>
    {
        List<GetCategoryWithBooksCountDTO> GetCategoriesWithBookCount();
    }
}
