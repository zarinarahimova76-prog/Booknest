
using Domain.Interfaces;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using WebApi.DTOs.Books;

namespace WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BooksController : ControllerBase
{
    private readonly IBookRepository _bookRepository;
    private readonly ILogger<BooksController> _logger;

    public BooksController(
        IBookRepository bookRepository,
        ILogger<BooksController> logger)
    {
        _bookRepository = bookRepository;
        _logger = logger;
    }
   [HttpGet]
    public async Task<IActionResult> GetBooks()
      {
    _logger.LogInformation("Request received: GetBooks");

    var books = await _bookRepository.GetBooks();

    var result = books.Select(book => new BookReadDto
    {
        Id = book.Id,
        Title = book.Title,
        PublishedYear = book.PublishedYear,
        TotalCopies = book.TotalCopies,
        AuthorId = book.AuthorId,
        GenreId = book.GenreId
    });

    _logger.LogInformation("Response sent: GetBooks");

    return Ok(result);
}

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetBook(int id)
   {
    _logger.LogInformation("Request received: GetBook with ID {Id}", id);

    try
    {
        var book = await _bookRepository.GetBook(id);

        var result = new BookReadDto
        {
            Id = book.Id,
            Title = book.Title,
            PublishedYear = book.PublishedYear,
            TotalCopies = book.TotalCopies,
            AuthorId = book.AuthorId,
            GenreId = book.GenreId
        };

        _logger.LogInformation("Response sent: GetBook with ID {Id}", id);

        return Ok(result);
    }
    catch (Exception ex) when (ex.Message == "Book not found")
    {
        _logger.LogWarning("Response sent: Book with ID {Id} not found", id);
        return NotFound(ex.Message);
    }
}

    [HttpPost]
public async Task<IActionResult> AddBook(BookCreateDto dto)
{
    _logger.LogInformation("Request received: AddBook");

    var book = new Book
    {
        Title = dto.Title,
        PublishedYear = dto.PublishedYear,
        TotalCopies = dto.TotalCopies,
        AuthorId = dto.AuthorId,
        GenreId = dto.GenreId
    };

    await _bookRepository.AddBook(book);

    _logger.LogInformation("Response sent: AddBook");

    return StatusCode(201, "Book added successfully");
}

    [HttpPut("{id:int}")]
public async Task<IActionResult> UpdateBook(int id, BookUpdateDto dto)
{
    _logger.LogInformation("Request received: UpdateBook with ID {Id}", id);

    try
    {
        var book = new Book
        {
            Id = id,
            Title = dto.Title,
            PublishedYear = dto.PublishedYear,
            TotalCopies = dto.TotalCopies,
            AuthorId = dto.AuthorId,
            GenreId = dto.GenreId
        };

        await _bookRepository.UpdateBook(book);

        _logger.LogInformation("Response sent: UpdateBook with ID {Id}", id);

        return Ok("Book updated successfully");
    }
    catch (Exception ex) when (ex.Message == "Book not found")
    {
        _logger.LogWarning("Response sent: Book with ID {Id} not found", id);
        return NotFound(ex.Message);
    }
}

   [HttpGet("author/{authorId:int}")]
public async Task<IActionResult> GetBooksByAuthor(int authorId)
{
    _logger.LogInformation("Request received: GetBooksByAuthor with AuthorId {AuthorId}", authorId);

    var books = await _bookRepository.GetBooksByAuthor(authorId);

    var result = books.Select(book => new BookReadDto
    {
        Id = book.Id,
        Title = book.Title,
        PublishedYear = book.PublishedYear,
        TotalCopies = book.TotalCopies,
        AuthorId = book.AuthorId,
        GenreId = book.GenreId
    });

    _logger.LogInformation("Response sent: GetBooksByAuthor with AuthorId {AuthorId}", authorId);

    return Ok(result);
}

    [HttpGet("genre/{genreId:int}")]
public async Task<IActionResult> GetBooksByGenre(int genreId)
{
    _logger.LogInformation("Request received: GetBooksByGenre with GenreId {GenreId}", genreId);

    var books = await _bookRepository.GetBooksByGenre(genreId);

    var result = books.Select(book => new BookReadDto
    {
        Id = book.Id,
        Title = book.Title,
        PublishedYear = book.PublishedYear,
        TotalCopies = book.TotalCopies,
        AuthorId = book.AuthorId,
        GenreId = book.GenreId
    });

    _logger.LogInformation("Response sent: GetBooksByGenre with GenreId {GenreId}", genreId);

    return Ok(result);
}
}
