namespace WritersClub.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

public class Page
{
    public int Id { get; set; }
    public int BookId { get; set; }
    [ValidateNever]
    public Book Book { get; set; }
    public int PageNumber { get; set; }
    public string Content { get; set; }
}