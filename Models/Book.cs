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

    [Required(ErrorMessage = "����� ����� ����������")]
    public User User { get; set; }

    [Required(ErrorMessage = "�� ������� ������������� �����")]
    public int GenreId { get; set; }

    [Required(ErrorMessage = "���� ����� ����������")]
    public Genre Genre { get; set; }

    [DataType(DataType.Date)]
    [Required(ErrorMessage = "���� ���������� �����������")]
    public DateOnly ReleaseDate { get; set; }

    public List<Rating> Ratings { get; set; } = new();
}
