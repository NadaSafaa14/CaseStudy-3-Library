using System.ComponentModel.DataAnnotations;

namespace CaseStudy_3_Library.DTOs.ApplicationUserDTOs
{
    public class RegisterDTO
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        [MinLength(8)]
        public string Password { get; set; }
    }
}
