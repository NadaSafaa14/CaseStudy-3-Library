using AutoMapper.Execution;
using CaseStudy_3_Library.DTOs.MemberDTO;
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
    public class MembersController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;

        public MembersController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        [HttpPost]
        public IActionResult AddMember(AddMemberDTO dto)
        {
            var emailexists = unitOfWork.Members
                .GetAll()
                .Any(m => m.Email == dto.Email);

            if (emailexists)
            {
                return BadRequest("Email already exists");
            }

            var member = new Membeer
            {
                FullName = dto.FullName,
                Email = dto.Email,
                Phone = dto.Phone
            };

            unitOfWork.Members.Add(member);

            unitOfWork.Save();

            return Created();
        }

        [HttpGet("top-readers")]
        public IActionResult TopReaders()
        {
            var members = unitOfWork.Members
                .GetTopReaders()
                .Select(m => new
                {
                    m.Id,
                    m.FullName,
                    BorrowingCount = m.Borrowings.Count
                });

            return Ok(members);
        }

        [HttpGet("{id}/statistics")]
        public IActionResult Statistics(int id)
        {
            var member = unitOfWork.Members
                .GetWithStatistics(id);

            if (member == null)
            {
                return NotFound("Member not found");
            }

            var total = member.Borrowings.Count;

            var returned = member.Borrowings
                .Count(b => b.ReturnDate != null);

            var currentlyBorrowed = member.Borrowings
                .Count(b => b.ReturnDate == null);

            var result = new
            {
                MemberName = member.FullName,
                TotalBorrowedBooks = total,
                ReturnedBooks = returned,
                CurrentlyBorrowedBooks = currentlyBorrowed
            };

            return Ok(result);
        }
    }
}
