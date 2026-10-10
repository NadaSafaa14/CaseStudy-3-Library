using CaseStudy_3_Library.Models;

namespace CaseStudy_3_Library.Repos.Interfaces
{
    public interface IUserRepo : IGenericRepo<ApplicationUser>
    {
        Task<ApplicationUser> GetByUserName(string userName);
    }
}
