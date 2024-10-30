using WritersClub.Models;

namespace WritersClub.Interfaces
{
    public interface IGenre
    {
        public Task<IEnumerable<Genre>> GetAllGenres();
        public Task<Genre> AddGenre(Genre genre);
        public Task<Genre> GetGenre(int id);
        public Task<Genre> UpdateGenre(Genre genre);
        public Task<Genre> DeleteGenre(int id);
    }
}
