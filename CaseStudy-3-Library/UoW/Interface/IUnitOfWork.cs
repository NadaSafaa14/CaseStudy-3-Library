using CaseStudy_3_Library.Models;
using CaseStudy_3_Library.Repos.Interfaces;

namespace CaseStudy_3_Library.UoW.Interface
{
    public interface IUnitOfWork
    {
        IBookRepo Books {  get;}
        IMemberRepo Members { get;}
        ICategoryRepo Categories { get;}
        IBorrowingRepo Borrowings { get;}

        int Save();
    }
}
