using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace WritersClub.Models;

public class Book
{
    public int Id { get; set; }

    [Required(ErrorMessage = "�������� ����� �����������")]
    [StringLength(100, ErrorMessage = "�������� ����� ������ ���� �� ������� 100 ��������")]
    public string Name { get; set; }

    [Required(ErrorMessage = "�� ������� ������������� ������������")]
    public int UserId { get; set; }

    public List<Page> Pages { get; set; } = new();
    [ValidateNever]
    public User User { get; set; }

    [Required(ErrorMessage = "�� ������� ������������� �����")]
    public int GenreId { get; set; }
    [ValidateNever]
    public Genre Genre { get; set; }

    [DataType(DataType.Date)]
    [Required(ErrorMessage = "���� ���������� �����������")]
    public DateTime ReleaseDate { get; set; }

    public List<Rating> Ratings { get; set; } = new();
    public ICollection<Comment> Comments { get; set; } = new List<Comment>();
}
