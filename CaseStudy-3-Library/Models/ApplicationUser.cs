using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace CaseStudy_3_Library.Models
{
    [Index(nameof(UserName), IsUnique = true)]
    public class ApplicationUser
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        public string PasswordHash { get; set; }

        [Required]
        public string Role { get; set; } = "Member";
    }
}
