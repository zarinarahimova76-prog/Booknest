
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

    public AuthorsController(IAuthorRepository authorRepository)
    {
        _authorRepository = authorRepository;
    }

    [HttpGet]
    public async Task<IActionResult> GetAuthors()
    {
        var authors = await _authorRepository.GetAll();

        var result = authors.Select(author => new AuthorReadDto
        {
            Id = author.Id,
            FullName = author.FullName,
            Country = author.Country,
            BirthYear = author.BirthYear
        });

        return Ok(result);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetAuthor(int id)
    {
        var author = await _authorRepository.GetById(id);

        if (author == null)
        {
            return NotFound("Author not found");
        }

        var result = new AuthorReadDto
        {
            Id = author.Id,
            FullName = author.FullName,
            Country = author.Country,
            BirthYear = author.BirthYear
        };

        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> AddAuthor(AuthorCreateDto dto)
    {
        var author = new Author
        {
            FullName = dto.FullName,
            Country = dto.Country,
            BirthYear = dto.BirthYear
        };

        await _authorRepository.Add(author);

        return StatusCode(201, "Author added successfully");
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateAuthor(int id, AuthorUpdateDto dto)
    {
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

            return Ok("Author updated successfully");
        }
        catch (Exception ex) when (ex.Message == "Author not found")
        {
            return NotFound(ex.Message);
        }
    }
}
