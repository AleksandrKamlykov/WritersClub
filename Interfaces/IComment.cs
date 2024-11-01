using WritersClub.Models;

namespace WritersClub.Repository;

public interface IComment
{
    public Task<IEnumerable<Comment>> GetAllCommentsByBookId(int bookId);
    public Task<Comment> AddComment(Comment comment,int id);
}