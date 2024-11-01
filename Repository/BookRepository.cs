using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using WritersClub.Data;
using WritersClub.Models;

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
        public async Task<Book>GetBookById(int id)
        {
            return await _context.Books
                .Include(b => b.User)
                .Include(b => b.Genre)
                .Include(b => b.Ratings)
                .FirstOrDefaultAsync(b => b.Id == id);
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
    }
}
