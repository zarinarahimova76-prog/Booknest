using Domain.Models;
namespace Domain.Interfaces;


public interface IBookRepository
{
    void AddBook(Book book);
     void UpdateBook(Book book);
    void DeleteBook(int id);
    Book GetBook(int id);
    List<Book> GetBooks();
}