using CaseStudy_3_Library.DTOs.BorrowingDTO;
using CaseStudy_3_Library.Models;
using CaseStudy_3_Library.UoW.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CaseStudy_3_Library.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BorrowingsController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;

        public BorrowingsController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        [HttpPost]
        public IActionResult AddBorrowing(AddBorrowingDTO dto)
        {
            var member = unitOfWork.Members
                .GetById(dto.MemberId);

            if (member == null)
            {
                return NotFound("Member not found");
            }

            var book = unitOfWork.Books
                .GetById(dto.BookId);

            if (book == null)
            {
                return NotFound("Book not found");
            }

            if (!book.IsAvailable)
            {
                return BadRequest("Book is not available");
            }

            var borrowing = new Borrowing
            {
                MemberId = dto.MemberId,
                BookId = dto.BookId,
                BorrowDate = DateTime.Now
            };

            book.IsAvailable = false;

            unitOfWork.Borrowings.Add(borrowing);

            unitOfWork.Books.Update(book);

            unitOfWork.Save();

            return Created();
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var borrowings = unitOfWork.Borrowings
                .GetAllWithDetails()
                .Select(b => new GetBorrowingDTO
                {
                    Id = b.Id,
                    BorrowDate = b.BorrowDate,
                    ReturnDate = b.ReturnDate,
                    MemberName = b.Member.FullName,
                    BookTitle = b.Book.Title
                });

            return Ok(borrowings);
        }

        [HttpPut("{id}/return")]
        public IActionResult ReturnBook(int id)
        {
            var borrowing = unitOfWork.Borrowings
                .GetById(id);

            if (borrowing == null)
            { 
                return NotFound("Borrowing not found"); 
            }

            if (borrowing.ReturnDate != null)
            {
                return BadRequest("Book is already returned");
            }

            borrowing.ReturnDate = DateTime.Now;

            var book = unitOfWork.Books.GetById(borrowing.BookId);

            book.IsAvailable = true;

            unitOfWork.Borrowings.Update(borrowing);

            unitOfWork.Books.Update(book);

            unitOfWork.Save();

            return NoContent();
        }
    }
}
