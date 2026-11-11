
using Domain.Interfaces;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using WebApi.DTOs.Authors;

namespace WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthorsController : ControllerBase
{
    private readonly IAuthorRepository _authorRepository;
    private readonly ILogger<AuthorsController> _logger;

    public AuthorsController(
    IAuthorRepository authorRepository,
    ILogger<AuthorsController> logger)
   {
    _authorRepository = authorRepository;
    _logger = logger;
    }

    [HttpGet]
     public async Task<IActionResult> GetAuthors()
    {
    _logger.LogInformation("Request received: GetAuthors");

    var authors = await _authorRepository.GetAll();

    var result = authors.Select(author => new AuthorReadDto
    {
        Id = author.Id,
        FullName = author.FullName,
        Country = author.Country,
        BirthYear = author.BirthYear
    });

    _logger.LogInformation("Response sent: GetAuthors");

    return Ok(result);
    }

    [HttpGet("{id:int}")]
   public async Task<IActionResult> GetAuthor(int id)
    {
    _logger.LogInformation("Request received: GetAuthor with ID {Id}", id);

    var author = await _authorRepository.GetById(id);

    if (author == null)
    {
        _logger.LogWarning("Response sent: Author with ID {Id} not found", id);
        return NotFound("Author not found");
    }

    var result = new AuthorReadDto
    {
        Id = author.Id,
        FullName = author.FullName,
        Country = author.Country,
        BirthYear = author.BirthYear
    };

    _logger.LogInformation("Response sent: GetAuthor with ID {Id}", id);

    return Ok(result);
   }

     [HttpPost]
   public async Task<IActionResult> AddAuthor(AuthorCreateDto dto)
   {
    _logger.LogInformation("Request received: AddAuthor");

    var author = new Author
    {
        FullName = dto.FullName,
        Country = dto.Country,
        BirthYear = dto.BirthYear
    };

    await _authorRepository.Add(author);

    _logger.LogInformation("Response sent: AddAuthor");

    return StatusCode(201, "Author added successfully");
   }

    [HttpPut("{id:int}")]
public async Task<IActionResult> UpdateAuthor(int id, AuthorUpdateDto dto)
{
    _logger.LogInformation("Request received: UpdateAuthor with ID {Id}", id);

    try
    {
        var author = new Author
        {
            Id = id,
            FullName = dto.FullName,
            Country = dto.Country,
            BirthYear = dto.BirthYear
        };

        await _authorRepository.Update(author);

        _logger.LogInformation("Response sent: UpdateAuthor with ID {Id}", id);

        return Ok("Author updated successfully");
    }
    catch (Exception ex) when (ex.Message == "Author not found")
    {
        _logger.LogWarning("Response sent: Author with ID {Id} not found", id);
        return NotFound(ex.Message);
    }
}
}
