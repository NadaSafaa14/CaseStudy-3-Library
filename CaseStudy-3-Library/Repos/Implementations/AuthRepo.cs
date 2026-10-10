using CaseStudy_3_Library.Data;
using CaseStudy_3_Library.DTOs.ApplicationUserDTOs;
using CaseStudy_3_Library.Models;
using CaseStudy_3_Library.Repos.Interfaces;
using CaseStudy_3_Library.UoW.Implementation;
using CaseStudy_3_Library.UoW.Interface;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CaseStudy_3_Library.Repos.Implementations
{
    public class AuthRepo : GenericRepo<ApplicationUser>, IAuthRepo
    {
        protected readonly AppDbContext _context;
        protected readonly IConfiguration config;
        public AuthRepo(AppDbContext context , IConfiguration configuration) : base(context)
        {
            _context = context;
            config = configuration;
        }

        public Task<LoginResponseDTO> Login(LoginDTO dto)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> Register(RegisterDTO dto)
        {
            var existingUser = await _context.ApplicationUsers
            .AnyAsync(u => u.UserName == dto.UserName);

            if (existingUser != null)
            {
                return false;
            }

            var user = new ApplicationUser
            {
                UserName = dto.UserName,
                Role = "Member"
            };

            var passwordHasher = new PasswordHasher<ApplicationUser>();

            user.PasswordHash = passwordHasher.HashPassword(
                user, dto.Password
            );

            _context.ApplicationUsers.Add(user);

            await _context.SaveChangesAsync();

            return true;
        }
    }
}
