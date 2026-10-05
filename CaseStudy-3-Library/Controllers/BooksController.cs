using System.ComponentModel.DataAnnotations;
using CaseStudy_3_Library.DTOs.BookDTO;
using CaseStudy_3_Library.UoW.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CaseStudy_3_Library.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        protected readonly IUnitOfWork _unitOfWork;

        public BooksController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }   

        [HttpPost]
        public IActionResult Add(AddBookDTO dto)
        {
            if (dto == null)
            {
                return BadRequest();
            }

            var category = _unitOfWork.Categories.GetById(dto.CategoryId);

            if (category == null)
            {
                return BadRequest();
            }

            if (dto.Price < 0)
            {
                return BadRequest();
            }

            //Validate that ISBN is unique.

            _unitOfWork.Categories.Add(category);

            _unitOfWork.Save();

            return Created();
        }
    }
}
