using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;
using System.Text.Json;
using WritersClub.Interfaces;
using WritersClub.Models;
using WritersClub.Repository;
using WritersClub.Auth;
using WritersClub.ViewModel;

namespace WritersClub.Controllers
{
    public class BookController : Controller
    {
        private readonly IBook _books;
        private readonly IUser _users;
        private readonly IGenre _genres;
        private readonly TokenService _tokenService;

        public BookController(IBook books, IUser users, IGenre genres, TokenService tokenService)
        {
            _books = books;
            _users = users;
            _genres = genres;
            _tokenService = tokenService;
        }

        public async Task<IActionResult> SelectUser(string searchQuery = "")
        {
            var users = string.IsNullOrWhiteSpace(searchQuery)
                ? await _users.GetAllUsers()
                : await _users.SearchUsersByName(searchQuery);

            return View(users);
        }
        [HttpGet]
        public async Task<IActionResult> Index(SearchBookViewModel searchModel)
        {
            var books = await _books.SearchBooks(searchModel);
            var users = await _users.GetAllUsers();
            var genres = await _genres.GetAllGenres();

            var user = _tokenService.GetUserFromToken(HttpContext.Request.Cookies["AuthToken"]);


            BooksViewModal booksViewModal = new BooksViewModal
            {
                Books = books,
                Users = users,
                Genres = genres,
                SearchBook = searchModel
            };

            ViewBag.userId = user?.Id ?? 0;
            return View(booksViewModal);
        }
        [HttpGet]
        public async Task<IActionResult> CreateBook()
        {

            var userA = _tokenService.GetUserFromToken(HttpContext.Request.Cookies["AuthToken"]);

            var user = await _users.GetUserById(userA.Id);
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
  
        public async Task<IActionResult> Detail(int id)
        {
            var book = await _books.GetBookById(id);
            if (book == null) return NotFound();

            var user = _tokenService.GetUserFromToken(HttpContext.Request.Cookies["AuthToken"]);
            ViewBag.userId = user?.Id ?? 0;
            ViewBag.AverageRating = await _books.CalculateAverageRating(id);

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
        [HttpPost]
        public async Task<IActionResult> AddRating(int bookId, int value)
        {
            if (value < 1 || value > 100)
            {
                return BadRequest("Значение рейтинга должно быть от 1 до 100.");
            }

            var book = await _books.GetBookById(bookId);
            if (book == null)
            {
                return NotFound("Книга не найдена.");
            }
            await _books.AddRating(bookId, value);

            return RedirectToAction(nameof(Detail), new { id = bookId });
        }

    }
}
