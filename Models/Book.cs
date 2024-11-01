using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace WritersClub.Models;

public class Book
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Название книги обязательно")]
    [StringLength(maximumLength:100, ErrorMessage = "Название книги должно быть не длиннее 100 символов")]
    public string Name { get; set; }

    [Required(ErrorMessage = "Не передан идентификатор пользователя")]
    public int UserId { get; set; }

    public List<Page> Pages { get; set; } = new List<Page>();
    public int PageCount { get; set; }
    [ValidateNever]
    public User User { get; set; }

    [Required(ErrorMessage = "Не передан идентификатор жанра")]
    public int GenreId { get; set; }
    [ValidateNever]
    public Genre Genre { get; set; }

    [DataType(DataType.Date)]
    [Required(ErrorMessage = "Дата публикации обязательна")]
    public DateTime ReleaseDate { get; set; }

    public List<Rating> Ratings { get; set; } = new();
}
