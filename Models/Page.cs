namespace WritersClub.Models;

public class Page
{
    public int Id { get; set; }
    public int BookId { get; set; }
    public Book Book { get; set; }
    public int PageNumber { get; set; }
    public string Content { get; set; }
}