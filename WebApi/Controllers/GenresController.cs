
using Domain.Interfaces;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using WebApi.DTOs.Genres;

namespace WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class GenresController : ControllerBase
{
    private readonly IGenreRepository _genreRepository;
    private readonly ILogger<GenresController> _logger;

    public GenresController(
    IGenreRepository genreRepository,
    ILogger<GenresController> logger)
{
    _genreRepository = genreRepository;
    _logger = logger;
}

   [HttpGet]
    public async Task<IActionResult> GetGenres()
  {
    _logger.LogInformation("Request received: GetGenres");

    var genres = await _genreRepository.GetAll();

    var result = genres.Select(genre => new GenreReadDto
    {
        Id = genre.Id,
        Name = genre.Name
    });

    _logger.LogInformation("Response sent: GetGenres");

    return Ok(result);
   }

    [HttpGet("{id:int}")]
   public async Task<IActionResult> GetGenre(int id)
   {
    _logger.LogInformation("Request received: GetGenre with ID {Id}", id);

    var genre = await _genreRepository.GetById(id);

    if (genre == null)
    {
        _logger.LogWarning("Response sent: Genre with ID {Id} not found", id);
        return NotFound("Genre not found");
    }

    var result = new GenreReadDto
    {
        Id = genre.Id,
        Name = genre.Name
    };

    _logger.LogInformation("Response sent: GetGenre with ID {Id}", id);

    return Ok(result);
  }

    [HttpPost]
  public async Task<IActionResult> AddGenre(GenreCreateDto dto)
  {
    _logger.LogInformation("Request received: AddGenre");

    var genre = new Genre
    {
        Name = dto.Name
    };

    await _genreRepository.Add(genre);

    _logger.LogInformation("Response sent: AddGenre");

    return StatusCode(201, "Genre added successfully");
   }

    [HttpPut("{id:int}")]
  public async Task<IActionResult> UpdateGenre(int id, GenreUpdateDto dto)
   {
    _logger.LogInformation("Request received: UpdateGenre with ID {Id}", id);

    try
    {
        var genre = new Genre
        {
            Id = id,
            Name = dto.Name
        };

        await _genreRepository.Update(genre);

        _logger.LogInformation("Response sent: UpdateGenre with ID {Id}", id);

        return Ok("Genre updated successfully");
    }
    catch (Exception ex) when (ex.Message == "Genre not found")
    {
        _logger.LogWarning("Response sent: Genre with ID {Id} not found", id);
        return NotFound(ex.Message);
    }
   }
}
