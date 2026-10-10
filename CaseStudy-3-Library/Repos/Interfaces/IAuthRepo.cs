using CaseStudy_3_Library.DTOs.ApplicationUserDTOs;
using CaseStudy_3_Library.Models;

namespace CaseStudy_3_Library.Repos.Interfaces
{
    public interface IAuthRepo : IGenericRepo<ApplicationUser>
    {
        Task<bool> Register(RegisterDTO dto);

        Task<LoginResponseDTO?> Login(LoginDTO dto);
    }
}
