using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace WritersClub.Models;

public class Comment
{
    public int Id { get; set; }
    [Required(ErrorMessage = "Напишите комментарий")]
    public string Text { get; set; }
    [Required(ErrorMessage = "Не передан идентификатор пользователя")]
    public int UserId { get; set; }
    [ValidateNever]
    public User User { get; set; }
    [Required(ErrorMessage = "Не передан идентификатор книги")]
    public int BookId { get; set; }
    [ValidateNever]
    public Book Book { get; set; }
}