namespace Domain.Models;

public class Book
{
    public int Id {get; set;}
    public string? Title{get; set;}
    public int PublishedYear {get; set;}
     public int TotalCopies {get; set;}=1;
}