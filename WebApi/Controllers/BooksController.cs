
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

    public BooksController(IBookRepository bookRepository)
    {
        _bookRepository = bookRepository;
    }

    [HttpGet]
    public async Task<IActionResult> GetBooks()
    {
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

        return Ok(result);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetBook(int id)
    {
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

            return Ok(result);
        }
        catch (Exception ex) when (ex.Message == "Book not found")
        {
            return NotFound(ex.Message);
        }
    }

    [HttpPost]
    public async Task<IActionResult> AddBook(BookCreateDto dto)
    {
        var book = new Book
        {
            Title = dto.Title,
            PublishedYear = dto.PublishedYear,
            TotalCopies = dto.TotalCopies,
            AuthorId = dto.AuthorId,
            GenreId = dto.GenreId
        };

        await _bookRepository.AddBook(book);

        return StatusCode(201, "Book added successfully");
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateBook(int id, BookUpdateDto dto)
    {
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

            return Ok("Book updated successfully");
        }
        catch (Exception ex) when (ex.Message == "Book not found")
        {
            return NotFound(ex.Message);
        }
    }

    [HttpGet("author/{authorId:int}")]
    public async Task<IActionResult> GetBooksByAuthor(int authorId)
    {
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

        return Ok(result);
    }

    [HttpGet("genre/{genreId:int}")]
    public async Task<IActionResult> GetBooksByGenre(int genreId)
    {
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

        return Ok(result);
    }
}
