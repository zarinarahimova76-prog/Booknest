
using Domain.Models;

namespace Domain.Interfaces;

public interface IAuthorRepository
{
    Task Add(Author author);
    Task<List<Author>> GetAll();
    Task<Author?> GetById(int id);
    Task Update(Author author);
    Task Delete(int id);
}
