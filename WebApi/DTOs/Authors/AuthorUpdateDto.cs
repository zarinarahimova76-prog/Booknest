namespace WebApi.DTOs.Authors;

public class AuthorUpdateDto
{
   public string FullName { get; set; } = null!;
    public string Country { get; set; } = null!;
    public int BirthYear { get; set; }
}