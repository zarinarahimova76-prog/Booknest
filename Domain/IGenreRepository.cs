
using Domain.Models;

namespace Domain.Interfaces;

public interface IGenreRepository
{
    Task Add(Genre genre);
    Task<List<Genre>> GetAll();
    Task<Genre?> GetById(int id);
    Task Update(Genre genre);
    Task Delete(int id);
}
