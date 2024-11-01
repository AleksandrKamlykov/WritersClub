using Microsoft.EntityFrameworkCore;
using WritersClub.Data;
using WritersClub.Models;

namespace WritersClub.Repository
{
    public class CommentRepository:IComment
    {
        private readonly ApplicationContext _context;
        public CommentRepository(ApplicationContext context)
        {
            _context = context; 
        }
        public async Task<IEnumerable<Comment>> GetAllCommentsByBookId(int bookId)
        {
            return await _context.Comments
            .Where(comment => comment.BookId == bookId).ToListAsync();
        }

        public async Task<Comment> AddComment(Comment comment, int userId)
        {
            if (comment == null) throw new ArgumentNullException(nameof(comment), "Comment cannot be null");
            var user = await _context.Users.AnyAsync(u => u.Id == userId);
            if (!user)
            {
                throw new ArgumentException("Пользователь не найден", nameof(userId));
            }
            comment.UserId = userId;
            await _context.Comments.AddAsync(comment);
            await _context.SaveChangesAsync();
            return comment;
        }

    }
}
