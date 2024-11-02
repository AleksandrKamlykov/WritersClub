using WritersClub.Models;

namespace WritersClub.ViewModel
{
    public class UserDetailViewModel
    {
        public User User { get; set; }
        public IEnumerable<Book> Books { get; set; }
    }
}
