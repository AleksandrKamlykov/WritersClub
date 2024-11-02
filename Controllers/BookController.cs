using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;
using System.Text.Json;
using WritersClub.Interfaces;
using WritersClub.Models;
using WritersClub.Repository;

namespace WritersClub.Controllers
{
    public class BookController : Controller
    {
        private readonly IBook _books;
        private readonly IUser _users;
        private readonly IGenre _genres;

        public BookController(IBook books, IUser users, IGenre genres)
        {
            _books = books;
            _users = users;
            _genres = genres;
        }

        public async Task<IActionResult> SelectUser(string searchQuery = "")
        {
            var users = string.IsNullOrWhiteSpace(searchQuery)
                ? await _users.GetAllUsers()
                : await _users.SearchUsersByName(searchQuery);

            return View(users);
        }

        public async Task<IActionResult> Index()
        {
            var books = await _books.GetAllBooks();
            ViewBag.Genres = await _genres.GetAllGenres();
            return View(books);
        }
        [HttpGet]
        public async Task<IActionResult> CreateBook(int userId=2)
        {
            var user = await _users.GetUserById(userId);
            if (user == null) return NotFound();

            var book = new Book { UserId = user.Id, User = user };
            ViewBag.Genres = await _genres.GetAllGenres();
            return View(book);
        }

        [HttpPost]
        public async Task<IActionResult> CreateBook(Book book)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors);
                foreach (var error in errors)
                {
                    Console.WriteLine(error.ErrorMessage);
                }
                ViewBag.Genres = await _genres.GetAllGenres();
                return View(book);
            }

            // Ensure Pages are correctly associated with the Book
            if (book.Pages != null)
            {
                for (int i = 0; i < book.Pages.Count; i++)
                {
                    book.Pages[i].BookId = book.Id;
                }
                book.PageCount = book.Pages.Count;
            }

            // Если валидация прошла, сохраняем книгу
            await _books.CreateBook(book);
            return RedirectToAction(nameof(Index));
        }
        [HttpGet("Book/Details/{bookId}")]
        public async Task<IActionResult> Details(int bookId)
        {
            var book = await _books.GetBookById(bookId);

        public async Task<IActionResult> Detail(int id)
        {
            var book = await _books.GetBookById(id);
            if (book == null) return NotFound();

            return View(book);
        }
        [HttpGet]
        public async Task<IActionResult> GetPage(int bookId, int pageNumber)
        {
            var book = await _books.GetBookById(bookId);
            if (book == null) return NotFound();

            var page = await _books.GetPage(bookId, pageNumber -1);
            if (page == null) return NotFound();

            var options = new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.Preserve,
                WriteIndented = true
            };

            return new JsonResult(page, options);
        }

    }
}
