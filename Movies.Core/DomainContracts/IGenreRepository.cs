using Movies.Core.Entities;

namespace Movies.Core.DomainContracts
{
    public interface IGenreRepository
    {
        Task<bool> ExistsAsync(string name);
        Task<IEnumerable<Genre>> GetAllAsync();
        Task<Genre?> GetByNameAsync(string name);
    }
}