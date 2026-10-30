
using Dapper;
using Domain.Interfaces;
using Domain.Models;
using Infrastructure.Data;

namespace Infrastructure.Repositories;

public class BookRepository : IBookRepository
{
    public async Task AddBook(Book book)
    {
        await using var connection = Database.Connection();

        var query = "insert into books (title, publishedyear, totalcopies) values (@Title, @PublishedYear, @TotalCopies)";

        await connection.ExecuteAsync(query, book);

        Console.WriteLine("Added book successfully!");
    }

    public async Task UpdateBook(Book book)
    {
        await using var connection = Database.Connection();

        var query = "update books set title = @Title, publishedyear = @PublishedYear, totalcopies = @TotalCopies where id = @Id";

        await connection.ExecuteAsync(query, book);

        Console.WriteLine("Updated!");
    }

    public async Task DeleteBook(int id)
    {
        await using var connection = Database.Connection();

        var query = "delete from books where id = @Id";

        await connection.ExecuteAsync(query, new { Id = id });

        Console.WriteLine("Deleted!");
    }

    public async Task<Book> GetBook(int id)
    {
        await using var connection = Database.Connection();

        var query = "select id, title, publishedyear, totalcopies from books where id = @Id";

        var book = await connection.QueryFirstOrDefaultAsync<Book>(
            query, new { Id = id });

        if (book == null)
        {
            throw new Exception("Book not found");
        }

        return book;
    }

    public async Task<List<Book>> GetBooks()
    {
        await using var connection = Database.Connection();

        var query = "select id, title, publishedyear, totalcopies from books";

        var books = await connection.QueryAsync<Book>(query);

        return books.ToList();
    }

    public async Task<List<Book>> GetBooksByAuthor(int authorId)
    {
        await using var connection = Database.Connection();

        var query = "select id, title, publishedyear, totalcopies from books where author_id = @AuthorId";

        var books = await connection.QueryAsync<Book>(
            query, new { AuthorId = authorId });

        return books.ToList();
    }

    public async Task<List<Book>> GetBooksByGenre(int genreId)
    {
        await using var connection = Database.Connection();

        var query = "select id, title, publishedyear, totalcopies from books where genre_id = @GenreId";

        var books = await connection.QueryAsync<Book>(
            query, new { GenreId = genreId });

        return books.ToList();
    }
}
