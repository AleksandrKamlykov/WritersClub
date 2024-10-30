using System.ComponentModel.DataAnnotations;

namespace WritersClub.Models;

public class Genre
{
    public int Id { get; set; }
    [Required(ErrorMessage = "Название жанра обязательно")]
    [StringLength(50, ErrorMessage = "Название должно быть не длиннее 50 символов")]
    public string Name { get; set; }
    [Required(ErrorMessage = "Краткое описание жанра обязательно")]
    public string Description { get; set; }
    public List<Book>? Books { get; set; }
}