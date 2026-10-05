using CaseStudy_3_Library.Models;
using CaseStudy_3_Library.Repos.Interfaces;

namespace CaseStudy_3_Library.UoW.Interface
{
    public interface IUnitOfWork
    {
        IBookRepo Books {  get;}
        IGenericRepo<Membeer> Members { get;}
        IGenericRepo<Category> Categories { get;}
        IGenericRepo<Borrowing> Borrowings { get;}

        int Save();
    }
}
