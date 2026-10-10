using CaseStudy_3_Library.Data;
using CaseStudy_3_Library.DTOs.ApplicationUserDTOs;
using CaseStudy_3_Library.Models;
using CaseStudy_3_Library.Repos.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CaseStudy_3_Library.Repos.Implementations
{
    public class AuthRepo : GenericRepo<ApplicationUser>, IAuthRepo
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _config;

        public AuthRepo(AppDbContext context, IConfiguration configuration) : base(context)
        {
            _context = context;
            _config = configuration;
        }

        public async Task<bool> Register(RegisterDTO dto)
        {
            bool usernameExists = await _context.ApplicationUsers
                .AnyAsync(u => u.UserName == dto.UserName);

            if (usernameExists)
            {
                return false;
            }

            var user = new ApplicationUser
            {
                UserName = dto.UserName,
                Role = "Member"
            };

            var passwordHasher = new PasswordHasher<ApplicationUser>();

            user.PasswordHash = passwordHasher.HashPassword(user,dto.Password);

            await _context.ApplicationUsers.AddAsync(user);

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<LoginResponseDTO?> Login(LoginDTO dto)
        {
            var user = await _context.ApplicationUsers
                .FirstOrDefaultAsync(u => u.UserName == dto.UserName);

            if (user == null)
            {
                return null;
            }

            var passwordHasher = new PasswordHasher<ApplicationUser>();

            var result = passwordHasher.VerifyHashedPassword(user, user.PasswordHash, dto.Password);

            if (result == PasswordVerificationResult.Failed)
            {
                return null;
            }

            return GenerateToken(user);
        }

        private LoginResponseDTO GenerateToken(ApplicationUser user)
        {

            var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_config["JWT:SecretKey"]));

            var credentials = new SigningCredentials(key,SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {
                new Claim(
                    ClaimTypes.NameIdentifier,
                    user.Id.ToString()
                ),

                new Claim(
                    ClaimTypes.Name,
                    user.UserName
                ),

                new Claim(
                    ClaimTypes.Role,
                    user.Role
                )
            };

            var tokenObj = new JwtSecurityToken(
                issuer: _config["JWT:Issuer"],
                audience: _config["JWT:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(Convert.ToDouble(_config["JWT:ExpirationDurationInHours"])),
                signingCredentials: credentials
            );

            string tokenString = new JwtSecurityTokenHandler()
                .WriteToken(tokenObj);

            return new LoginResponseDTO
            {
                Token = tokenString,
                Expiration = DateTime.UtcNow.AddHours(Convert.ToDouble(_config["JWT:ExpirationDurationInHours"]))
            };
        }
    }
}
