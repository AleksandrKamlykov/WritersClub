using System.ComponentModel.DataAnnotations;

namespace WritersClub.Models;

public class Genre
{
    public int Id { get; set; }
    [Required(ErrorMessage = "�������� ����� �����������")]
    [StringLength(50, ErrorMessage = "�������� ������ ���� �� ������� 50 ��������")]
    public string Name { get; set; }
    [Required(ErrorMessage = "������� �������� ����� �����������")]
    public string Description { get; set; }
    public List<Book>? Books { get; set; }
}