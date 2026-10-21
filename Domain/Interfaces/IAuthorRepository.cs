
using Domain.Models;

namespace Domain.Interfaces;

public interface IAuthorRepository
{
    void Add(Author author);
    List<Author> GetAll();
    Author? GetById(int id);
    void Update(Author author);
    void Delete(int id);
}
