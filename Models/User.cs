using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace WritersClub.Models;

public enum UserState
{
    Active,
    Deleted
}

public class User
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Имя пользователя обязательно")]
    [StringLength(50, MinimumLength = 6, ErrorMessage = "Имя должно быть не менее 3 и не длиннее 50 символов")]
    public string Name { get; set; }

    [Required(ErrorMessage = "Email обязателен")]
    [EmailAddress(ErrorMessage = "Неверный формат Email")]
    public string Email { get; set; }

    [Required(ErrorMessage = "Пароль обязателен")]
    [StringLength(100, MinimumLength = 6, ErrorMessage = "Пароль должен содержать от 6 до 100 символов")]
    public string Password { get; set; }

    [Required(ErrorMessage = "Логин обязателен")]
    [StringLength(30, ErrorMessage = "Логин должен быть не длиннее 30 символов")]
    [RegularExpression(@"^\w+$", ErrorMessage = "Логин может содержать только буквы, цифры и подчеркивания")]
    [Remote(action: "IsUserNameAvailable", controller: "Account", AdditionalFields = "Id", ErrorMessage = "Этот логин уже занят другим пользователем.")]
    public string Login { get; set; }

    public UserState State { get; set; }
}
