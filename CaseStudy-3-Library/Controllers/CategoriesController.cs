using CaseStudy_3_Library.DTOs.CategoryDTO;
using CaseStudy_3_Library.Models;
using CaseStudy_3_Library.UoW.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CaseStudy_3_Library.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CategoriesController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;

        public CategoriesController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        [HttpPost]
        public IActionResult AddCategory(AddCategoryDTO dto)
        {
            var nameexists = unitOfWork.Categories
                .GetAll()
                .Any(c => c.Name == dto.Name);

            if (nameexists)
            {
                return BadRequest("Category name already exists");
            }

            var category = new Category
            {
                Name = dto.Name,
                Description = dto.Description
            };

            unitOfWork.Categories.Add(category);

            unitOfWork.Save();

            return Created();
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var categories = unitOfWork.Categories
               .GetCategoriesWithBookCount();

            return Ok(categories);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var category = unitOfWork.Categories.GetById(id);

            if (category == null)
            {
                return NotFound("Category not found");
            }

            var books = unitOfWork.Books.GetBooksByCategory(id);

            if (books.Any())
            {
                return BadRequest("Cannot delete category because it contains books");
            }

            unitOfWork.Categories.Delete(category);

            unitOfWork.Save();

            return NoContent();
        }
    }
}
