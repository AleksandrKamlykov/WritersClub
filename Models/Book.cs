using System.ComponentModel.DataAnnotations;

namespace WritersClub.Models;

public class Book
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Название книги обязательно")]
    [StringLength(100, ErrorMessage = "Название книги должно быть не длиннее 100 символов")]
    public string Name { get; set; }

    [Required(ErrorMessage = "Не передан идентификатор пользователя")]
    public int UserId { get; set; }

    public List<Page> Pages { get; set; } = new();

    [Required(ErrorMessage = "Автор книги обязателен")]
    public User User { get; set; }

    [Required(ErrorMessage = "Не передан идентификатор жанра")]
    public int GenreId { get; set; }

    [Required(ErrorMessage = "Жанр книги обязателен")]
    public Genre Genre { get; set; }

    [DataType(DataType.Date)]
    [Required(ErrorMessage = "Дата публикации обязательна")]
    public DateOnly ReleaseDate { get; set; }

    public List<Rating> Ratings { get; set; } = new();
}
