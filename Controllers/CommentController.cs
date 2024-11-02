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

        [HttpPost]
        public async Task<IActionResult> CreateComment(Comment comment, int userId)
        {
            if (!ModelState.IsValid)
            {
                return View(comment);
            }
            var result = await _comments.AddComment(comment, userId);
            return RedirectToAction("Detail", "Book", new { id = comment.BookId });
        }
        
    }
}
