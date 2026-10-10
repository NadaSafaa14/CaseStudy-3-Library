using CaseStudy_3_Library.Data;
using CaseStudy_3_Library.Models;
using CaseStudy_3_Library.Repos.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CaseStudy_3_Library.Repos.Implementations
{
    public class UserRepo : GenericRepo<ApplicationUser>, IUserRepo
    {
        protected readonly AppDbContext Context;

        public UserRepo(AppDbContext context) : base(context)
        {
            Context = context;
        }

        public async Task<ApplicationUser> GetByUserName(string userName)
        {
            var user = Context.ApplicationUsers
                .FirstOrDefaultAsync(u => u.UserName == userName);

            return await user;
        }
    }
}
