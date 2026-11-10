namespace WebApi.DTOs.Books;

public class BookUpdateDto
{
    public string? Title { get; set; }
    public int PublishedYear { get; set; }
    public int TotalCopies { get; set; }
    public int AuthorId { get; set; }
    public int GenreId { get; set; }
}