
using Domain.Interfaces;
using Domain.Models;
using Infrastructure.Data;
using Npgsql;

namespace Infrastructure.Repositories;

public class BookRepository : IBookRepository
{
    public async Task AddBook(Book book)
    {
        await using var connection = Database.Connection();
        await connection.OpenAsync();

        var query = "insert into books (title, publishedyear, totalcopies) values (@title, @publishedyear, @totalcopies)";

        await using var cmd = new NpgsqlCommand(query, connection);

        cmd.Parameters.AddWithValue("title", book.Title);
        cmd.Parameters.AddWithValue("publishedyear", book.PublishedYear);
        cmd.Parameters.AddWithValue("totalcopies", book.TotalCopies);

        await cmd.ExecuteNonQueryAsync();

        Console.WriteLine("Added book successfully!");
    }

    public async Task UpdateBook(Book book)
    {
        await using var connection = Database.Connection();
        await connection.OpenAsync();

        var query = "update books set title = @title, publishedyear = @publishedyear, totalcopies = @totalcopies where id = @id";

        await using var cmd = new NpgsqlCommand(query, connection);

        cmd.Parameters.AddWithValue("title", book.Title);
        cmd.Parameters.AddWithValue("publishedyear", book.PublishedYear);
        cmd.Parameters.AddWithValue("totalcopies", book.TotalCopies);
        cmd.Parameters.AddWithValue("id", book.Id);

        await cmd.ExecuteNonQueryAsync();

        Console.WriteLine("Updated!");
    }

    public async Task DeleteBook(int id)
    {
        await using var connection = Database.Connection();
        await connection.OpenAsync();

        var query = "delete from books where id = @id";

        await using var cmd = new NpgsqlCommand(query, connection);

        cmd.Parameters.AddWithValue("id", id);

        await cmd.ExecuteNonQueryAsync();

        Console.WriteLine("Deleted!");
    }

    public async Task<Book> GetBook(int id)
    {
        await using var connection = Database.Connection();
        await connection.OpenAsync();

        var query = "select id, title, publishedyear, totalcopies from books where id = @id";

        await using var cmd = new NpgsqlCommand(query, connection);

        cmd.Parameters.AddWithValue("id", id);

        await using var data = await cmd.ExecuteReaderAsync();

        if (await data.ReadAsync())
        {
            var book = new Book();

            book.Id = data.GetInt32(0);
            book.Title = data.GetString(1);
            book.PublishedYear = data.GetInt32(2);
            book.TotalCopies = data.GetInt32(3);

            return book;
        }

        throw new Exception("Book not found");
    }

    public async Task<List<Book>> GetBooks()
    {
        await using var connection = Database.Connection();
        await connection.OpenAsync();

        var query = "select id, title, publishedyear, totalcopies from books";

        await using var cmd = new NpgsqlCommand(query, connection);
        await using var data = await cmd.ExecuteReaderAsync();

        var books = new List<Book>();

        while (await data.ReadAsync())
        {
            var book = new Book();

            book.Id = data.GetInt32(0);
            book.Title = data.GetString(1);
            book.PublishedYear = data.GetInt32(2);
            book.TotalCopies = data.GetInt32(3);

            books.Add(book);
        }

        return books;
    }

    public async Task<List<Book>> GetBooksByAuthor(int authorId)
    {
        await using var connection = Database.Connection();
        await connection.OpenAsync();

        var query = "select id, title, publishedyear, totalcopies from books where author_id = @authorId";

        await using var cmd = new NpgsqlCommand(query, connection);

        cmd.Parameters.AddWithValue("authorId", authorId);

        await using var data = await cmd.ExecuteReaderAsync();

        var books = new List<Book>();

        while (await data.ReadAsync())
        {
            var book = new Book();

            book.Id = data.GetInt32(0);
            book.Title = data.GetString(1);
            book.PublishedYear = data.GetInt32(2);
            book.TotalCopies = data.GetInt32(3);

            books.Add(book);
        }

        return books;
    }

    public async Task<List<Book>> GetBooksByGenre(int genreId)
    {
        await using var connection = Database.Connection();
        await connection.OpenAsync();

        var query = "select id, title, publishedyear, totalcopies from books where genre_id = @genreId";

        await using var cmd = new NpgsqlCommand(query, connection);

        cmd.Parameters.AddWithValue("genreId", genreId);

        await using var data = await cmd.ExecuteReaderAsync();

        var books = new List<Book>();

        while (await data.ReadAsync())
        {
            var book = new Book();

            book.Id = data.GetInt32(0);
            book.Title = data.GetString(1);
            book.PublishedYear = data.GetInt32(2);
            book.TotalCopies = data.GetInt32(3);

            books.Add(book);
        }

        return books;
    }
}
