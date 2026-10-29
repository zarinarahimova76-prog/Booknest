
using Domain.Models;

namespace Domain.Interfaces;

public interface IBookRepository
{
    Task AddBook(Book book);
    Task UpdateBook(Book book);
    Task DeleteBook(int id);
    Task<Book> GetBook(int id);
    Task<List<Book>> GetBooks();

    Task<List<Book>> GetBooksByAuthor(int authorId);
    Task<List<Book>> GetBooksByGenre(int genreId);
}
