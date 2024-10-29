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

    [Required(ErrorMessage = "��� ������������ �����������")]
    [StringLength(50, MinimumLength = 6, ErrorMessage = "��� ������ ���� �� ����� 3 � �� ������� 50 ��������")]
    public string Name { get; set; }

    [Required(ErrorMessage = "Email ����������")]
    [EmailAddress(ErrorMessage = "�������� ������ Email")]
    public string Email { get; set; }

    [Required(ErrorMessage = "������ ����������")]
    [StringLength(100, MinimumLength = 6, ErrorMessage = "������ ������ ��������� �� 6 �� 100 ��������")]
    public string Password { get; set; }

    [Required(ErrorMessage = "����� ����������")]
    [StringLength(30, ErrorMessage = "����� ������ ���� �� ������� 30 ��������")]
    [RegularExpression(@"^\w+$", ErrorMessage = "����� ����� ��������� ������ �����, ����� � �������������")]
    [Remote(action: "IsUserNameAvailable", controller: "Account", AdditionalFields = "Id", ErrorMessage = "���� ����� ��� ����� ������ �������������.")]
    public string Login { get; set; }

    public UserState State { get; set; }
}
