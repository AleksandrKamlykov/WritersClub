namespace WritersClub.Models;

public class Book
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int UserId { get; set; }
    public List<Page> Pages { get; set; }
    public User User { get; set; }
    public int GenreId { get; set; }
    public Genre Genre { get; set; }
    public DateOnly ReleaseDate { get; set; }
    public List<Rating> Ratings { get; set; }
}