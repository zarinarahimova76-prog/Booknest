using Domain.Interfaces;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;

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
        return Ok(books);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetBook(int id)
    {
        try
        {
            var book = await _bookRepository.GetBook(id);
            return Ok(book);
        }
        catch (Exception ex) when (ex.Message == "Book not found")
        {
            return NotFound(ex.Message);
        }
    }

    [HttpPost]
    public async Task<IActionResult> AddBook(Book book)
    {
        await _bookRepository.AddBook(book);
       return StatusCode(201, "Book added successfully");
    }

   [HttpPut]
public async Task<IActionResult> UpdateBook(Book book)
{
    try
    {
        await _bookRepository.UpdateBook(book);
        return Ok("Book updated successfully");
    }
    catch (Exception ex) when (ex.Message == "Book not found")
    {
        return NotFound(ex.Message);
    }
}

    [HttpDelete("{id:int}")]
public async Task<IActionResult> DeleteBook(int id)
{
    try
    {
        await _bookRepository.DeleteBook(id);
        return Ok("Book deleted successfully");
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
        return Ok(books);
    }

    [HttpGet("genre/{genreId:int}")]
    public async Task<IActionResult> GetBooksByGenre(int genreId)
    {
        var books = await _bookRepository.GetBooksByGenre(genreId);
        return Ok(books);
    }
}