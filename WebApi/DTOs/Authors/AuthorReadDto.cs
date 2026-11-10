namespace WebApi.DTOs.Authors;

public class AuthorReadDto
{
    public int Id { get; set; }
    public string? FullName { get; set; }
    public string? Country { get; set; }
    public int BirthYear { get; set; }
}