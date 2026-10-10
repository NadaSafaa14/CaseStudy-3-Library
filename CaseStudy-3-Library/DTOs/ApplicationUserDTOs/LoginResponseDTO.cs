namespace CaseStudy_3_Library.DTOs.ApplicationUserDTOs
{
    public class LoginResponseDTO
    {
        public string Token { get; set; } 

        public DateTime Expiration { get; set; }
    }
}
