using CaseStudy_3_Library.DTOs.ApplicationUserDTOs;
using CaseStudy_3_Library.Models;
using CaseStudy_3_Library.UoW.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CaseStudy_3_Library.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        protected readonly IUnitOfWork _unitOfWork;

        public AuthController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        //[AllowAnonymous]
        [HttpPost("Register")]
        public async Task<IActionResult> Register(RegisterDTO dto)
        {
            if (dto == null || !ModelState.IsValid)
            {
                return BadRequest();
            }

            var result = await _unitOfWork.authRepo.Register(dto);

            if (!result)
            {
                return BadRequest("Username already exists");
            }

            return Created();
        }
    }
}
