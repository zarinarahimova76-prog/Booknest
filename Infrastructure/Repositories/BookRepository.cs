using Domain.Interfaces;
using Domain.Models;
using Infrastructure.Data;
using Npgsql;

namespace Infrastructure.Repositories;

public class BookRepository : IBookRepository
{
    public void AddBook(Book book)
    {
        using var connection = Database.Connection();
        connection.Open();
        var query =  $"insert into books(title, publishedyear, totalcopies) values('{book.Title}', {book.PublishedYear}, {book.TotalCopies})";
        var cmd = new NpgsqlCommand(query, connection);
         cmd.ExecuteNonQuery();
        System.Console.WriteLine("Added book successfully!");
    }
     public void DeleteBook(int id)
    {
        using var conn = Database.Connection();
        conn.Open();
        var query = $"delete from books where id={id}";
        var cmd = new NpgsqlCommand(query, conn);
        cmd.ExecuteNonQuery();
        System.Console.WriteLine("Deleted!");
    }

    public Book GetBook(int id)
    {
        using var conn = Database.Connection();
        conn.Open();
        var query = $"select id, title, publishedyear, totalcopies from books where id={id}";
        var cmd = new NpgsqlCommand(query, conn);
        var data = cmd.ExecuteReader();
        var book = new Book();
        while (data.Read())
        {
            book.Id = data.GetInt32(0);
            book.Title = data.GetString(1);
            book.PublishedYear = data.GetInt32(2);
            book.TotalCopies = data.GetInt32(3);
        }
        return book;
    }

    public List<Book> GetBooks()
    {
        using var conn = Database.Connection();
        conn.Open();
        var query = $"select id, title, publishedyear, totalcopies from books";
        var cmd = new NpgsqlCommand(query, conn);
        var data = cmd.ExecuteReader();
        var books = new List<Book>();
        while (data.Read())
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

    public void UpdateBook(Book book)
    {
        using var conn = Database.Connection();
        conn.Open();
        var query = $"update books set title='{book.Title}', publishedyear={book.PublishedYear}, totalcopies={book.TotalCopies} where id={book.Id}";
        var cmd = new NpgsqlCommand(query, conn);
        cmd.ExecuteNonQuery();
        System.Console.WriteLine("Updated!");
    }

}


