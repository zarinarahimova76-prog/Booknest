
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

    public GenresController(IGenreRepository genreRepository)
    {
        _genreRepository = genreRepository;
    }

    [HttpGet]
    public async Task<IActionResult> GetGenres()
    {
        var genres = await _genreRepository.GetAll();

        var result = genres.Select(genre => new GenreReadDto
        {
            Id = genre.Id,
            Name = genre.Name
        });

        return Ok(result);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetGenre(int id)
    {
        var genre = await _genreRepository.GetById(id);

        if (genre == null)
        {
            return NotFound("Genre not found");
        }

        var result = new GenreReadDto
        {
            Id = genre.Id,
            Name = genre.Name
        };

        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> AddGenre(GenreCreateDto dto)
    {
        var genre = new Genre
        {
            Name = dto.Name
        };

        await _genreRepository.Add(genre);

        return StatusCode(201, "Genre added successfully");
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateGenre(int id, GenreUpdateDto dto)
    {
        try
        {
            var genre = new Genre
            {
                Id = id,
                Name = dto.Name
            };

            await _genreRepository.Update(genre);

            return Ok("Genre updated successfully");
        }
        catch (Exception ex) when (ex.Message == "Genre not found")
        {
            return NotFound(ex.Message);
        }
    }
}
