using Microsoft.EntityFrameworkCore;
using WritersClub.Data;
using WritersClub.Interfaces;
using WritersClub.Models;

namespace WritersClub.Repository
{
    public class GenreRepository:IGenre
    {

        private readonly ApplicationContext _context;

        public GenreRepository(ApplicationContext context)
        {
            _context = context;
        }
        public async Task<Genre> AddGenre(Genre genre)
        {
            if (genre == null)
            {
                throw new ArgumentNullException(nameof(genre), "Genre cannot be null");
            }
            await _context.AddAsync(genre);
            await _context.SaveChangesAsync();
            return genre;
        }
        public async Task<Genre> GetGenre(int id)
        {
            return await _context.Genres.FindAsync(id);
        }
        public async Task<Genre> UpdateGenre(Genre genre)
        {
            _context.Genres.Update(genre);
            await _context.SaveChangesAsync();
            return genre;
        }
        public async Task<Genre>DeleteGenre(int id)
        {
            var genre = await GetGenre(id);
            if (genre != null)
            {
                _context.Remove(genre);
                await _context.SaveChangesAsync();
            }
            return genre;
        }
        public async Task<IEnumerable<Genre>> GetAllGenres()
        {
            return await _context.Genres.ToListAsync();
        }
    }
}
