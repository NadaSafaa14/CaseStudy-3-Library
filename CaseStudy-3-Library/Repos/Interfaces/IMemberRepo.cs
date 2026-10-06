using AutoMapper.Execution;
using CaseStudy_3_Library.Models;

namespace CaseStudy_3_Library.Repos.Interfaces
{
    public interface IMemberRepo : IGenericRepo<Membeer>
    {
        List<Membeer> GetTopReaders();

        Membeer GetWithStatistics(int id);
    }
}
