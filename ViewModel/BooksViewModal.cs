using WritersClub.Models;

namespace WritersClub.ViewModel
{
    public class BooksViewModal
    {
        public IEnumerable<Book> Books { get; set; }
        public IEnumerable<User> Users { get; set; }
        public IEnumerable<Genre> Genres { get; set; }
        public SearchBookViewModel SearchBook { get; set; }
    }
}
