using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using WritersClub.Data;
using WritersClub.Models;
using WritersClub.ViewModel;

namespace WritersClub.Repository
{
    public class BookRepository : IBook
    {
        private readonly ApplicationContext _context;
        public BookRepository(ApplicationContext context)
        {
            _context = context;
        }
        public async Task<Book> CreateBook(Book book)
        {
            if (book == null) throw new ArgumentNullException(nameof(book), "Genre cannot be null");

            await _context.AddAsync(book);
            await _context.SaveChangesAsync();
            return book;
        }
        public async Task<IEnumerable<Book>> GetAllBooks()
        {
            return await _context.Books
                .Include(b => b.User)
                .Include(b => b.Genre)
                .ToListAsync();
        }
        public async Task<Book> GetBookById(int BookId)
        {
            return await _context.Books
                .Include(b => b.User)
                .Include(b => b.Genre)
                .Include(b => b.Ratings)
                .Include(b => b.Comments)
                .ThenInclude(c => c.User)
                .FirstOrDefaultAsync(b => b.Id == BookId);
        }
        public async Task<Book> DeleteBook(int id)
        {
            var book=await GetBookById(id);
            if(book!=null)
            {
                _context.Books.Remove(book);
                await _context.SaveChangesAsync();
            }
            return book;
        }
        public async Task<Book> UpdateBook(Book book)
        {
            _context.Books.Update(book);
            await _context.SaveChangesAsync();
            return book;
        }
        public async Task<Book> AddRating(int bookId, int rating)
        {
            var book = await GetBookById(bookId);
            if (book != null)
            {
                book.Ratings.Add(new Rating { BookId = bookId, Value = rating });
                await _context.SaveChangesAsync();
            }
            return book;
        }
        public async Task<Page> GetPage(int bookId, int pageNum)
        {
            var book = await GetBookById(bookId);
            if (book == null) return null;

            var page = _context.Pages.FirstOrDefault(p => p.PageNumber == pageNum && p.BookId == bookId);
            return page;
        }
        public async Task<IEnumerable<Book>> GetBooksByUserId(int userId)
        {
            return await _context.Books
                .Include(b => b.User)
                .Include(b => b.Genre)
                .Where(b => b.UserId == userId)
                .ToListAsync();
        }
        public async Task<double> CalculateAverageRating(int bookId)
        {
            var ratings = await _context.Ratings
                .Where(r => r.BookId == bookId)
                .ToListAsync();

            return ratings.Any() ? ratings.Average(r => r.Value) : 0;
        }
        public async Task<List<Book>> SearchBooks(SearchBookViewModel searchModel)
        {
            var query = _context.Books
                .Include(b => b.Genre)
                .Include(b => b.User)
                .AsQueryable();

            if (!string.IsNullOrEmpty(searchModel.Name))
            {
                query = query.Where(b => b.Name.Contains(searchModel.Name));

            }

            if (searchModel.GenreId.HasValue)
            {
                query = query.Where(b => b.GenreId == searchModel.GenreId.Value);
            }

            if (searchModel.AuthorId.HasValue)
            {
                query = query.Where(b => b.User.Id == searchModel.AuthorId.Value);
            }

            return await query.ToListAsync();
        }
    }
}
