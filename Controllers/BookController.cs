using Microsoft.AspNetCore.Mvc;
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
        public async Task<IActionResult> CreateBook(int userId)
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

            // Если валидация прошла, сохраняем книгу
            await _books.CreateBook(book);
            return RedirectToAction(nameof(Index));
        }

    }
}
