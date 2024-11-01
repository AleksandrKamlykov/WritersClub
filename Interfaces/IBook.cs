using WritersClub.Models;
using WritersClub.ViewModel;

namespace WritersClub.Repository;

public interface IBook
{
    public Task<IEnumerable<Book>> GetAllBooks();
    public Task<Book> GetBookById(int id);
    public Task<Book> UpdateBook(Book book);
    public Task<Book> CreateBook(Book book);
    public Task<Book> DeleteBook(int id);
    public Task<Book> AddRating(int bookId, int rating);
    public Task<Page> GetPage(int bookId, int rating);
    //public Task<List<Book>> SearchBooks(SearchBookViewModel searchBook);
}