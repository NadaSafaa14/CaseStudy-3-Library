using CaseStudy_3_Library.UoW.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CaseStudy_3_Library.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApplicationUsersController : ControllerBase
    {
        protected readonly IUnitOfWork _unitOfWork;

        public ApplicationUsersController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet("Get-By-UserName")]
        public async Task<IActionResult> GetByUserName(string userName)
        {
            var user = await _unitOfWork.ApplicationUsers.GetByUserName(userName);

            return Ok(user);
        }
    }
}
