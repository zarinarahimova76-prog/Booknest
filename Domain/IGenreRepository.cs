
using Domain.Models;

namespace Domain.Interfaces;

public interface IGenreRepository
{
    void Add(Genre genre);
    List<Genre> GetAll();
    Genre? GetById(int id);
    void Update(Genre genre);
    void Delete(int id);
}

