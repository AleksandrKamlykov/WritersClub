using System.ComponentModel.DataAnnotations;

namespace WritersClub.Models;

public class Genre
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Название жанра обязательно")]
    [StringLength(maximumLength: 50, ErrorMessage = "Название жанра должно быть не длиннее 50 символов")]
    public string Name { get; set; }

    [Required(ErrorMessage = "Описание жанра обязательно")]
    public string Description { get; set; }

    public List<Book>? Books { get; set; }
}
