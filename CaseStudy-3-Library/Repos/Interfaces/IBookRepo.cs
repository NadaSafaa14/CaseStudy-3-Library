using CaseStudy_3_Library.DTOs.BookDTO;
using CaseStudy_3_Library.Models;

namespace CaseStudy_3_Library.Repos.Interfaces
{
    public interface IBookRepo :IGenericRepo<Book>
    {
        List<Book> GetAvailableBooks();

        List<Book> GetBooksByCategory(int CategoryId);

        Book GetMostExpensive();

        List<BookSearchDTO> Search(string keyword);
    }
}
