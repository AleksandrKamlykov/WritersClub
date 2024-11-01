using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WritersClub.Models;
using WritersClub.Repository;

namespace WritersClub.Controllers
{
    public class CommentController : Controller
    {
        private readonly IComment _comments;
        private readonly IUser _users;
        private readonly IBook _books;
        public CommentController(IComment comments, IUser users, IBook books)
        {
            _comments = comments;
            _users = users;
            _books = books;
        }

        public async Task<IActionResult> CreateComment(int bookId, int userId=2)
        {
            var user = await _users.GetUserById(userId);
           
            if (user == null) return NotFound();

            var comment = new Comment { UserId = user.Id, BookId = bookId };
            ViewBag.UserName = user.Login;
            return View(comment);
        }
        [HttpPost]
        public async Task<IActionResult> CreateComment(Comment comment, int userId)
        {
            if (!ModelState.IsValid)
            {
                return View(comment);
            }
            var result = await _comments.AddComment(comment, userId);
            return RedirectToAction("Details", "Book", new { id = comment.BookId });
        }
        
    }
}
