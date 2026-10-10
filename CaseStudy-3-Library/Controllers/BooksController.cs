using CaseStudy_3_Library.DTOs.BookDTO;
using CaseStudy_3_Library.Models;
using CaseStudy_3_Library.UoW.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace CaseStudy_3_Library.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class BooksController : ControllerBase
    {
        protected readonly IUnitOfWork unitOfWork;

        public BooksController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        [HttpPost]
        public IActionResult AddBook(AddBookDTO dto)
        {
            if (dto.Price <= 0)
            {
                return BadRequest("Price must be greater than zero");
            }

            var isbnExists = unitOfWork.Books
                .GetAll()
                .Any(b => b.ISBN == dto.ISBN);

            if (isbnExists)
            {
                return BadRequest("ISBN already exists");
            }

            var category = unitOfWork.Categories
                .GetById(dto.CategoryId);

            if (category == null)
            {
                return NotFound("Category not found");
            }

            var book = new Book
            {
                Title = dto.Title,
                Author = dto.Author,
                ISBN = dto.ISBN,
                Price = dto.Price,
                CategoryId = dto.CategoryId,
                IsAvailable = true
            };

            unitOfWork.Books.Add(book);

            unitOfWork.Save();

            return Created();
        }

        [HttpGet("search")]
        public IActionResult Search(string keyword)
        {
            var books = unitOfWork.Books.Search(keyword);

            return Ok(books);
        }

        [HttpGet("most-expensive")]
        public IActionResult MostExpensive()
        {
            var book = unitOfWork.Books.GetMostExpensive();

            if (book == null)
            {
                return NotFound("No books found");
            }

            return Ok(book);
        }
    }
}
