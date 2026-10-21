
using Domain.Interfaces;
using Domain.Models;
using Infrastructure.Data;
using Npgsql;

namespace Infrastructure.Repositories;

public class AuthorRepository : IAuthorRepository
{
    public void Add(Author author)
    {
        using var connection = Database.Connection();
        connection.Open();

        var query = "insert into authors(full_name, country, birth_year) values(@fullname, @country, @birthyear)";

        using var cmd = new NpgsqlCommand(query, connection);
        cmd.Parameters.AddWithValue("@fullname", author.FullName);
        cmd.Parameters.AddWithValue("@country", author.Country);
        cmd.Parameters.AddWithValue("@birthyear", author.BirthYear);

        cmd.ExecuteNonQuery();

        Console.WriteLine("Added!");
    }

    public List<Author> GetAll()
    {
        using var connection = Database.Connection();
        connection.Open();

        var query = "select id, full_name, country, birth_year from authors";
        using var cmd = new NpgsqlCommand(query, connection);
        using var data = cmd.ExecuteReader();

        var authors = new List<Author>();

        while (data.Read())
        {
            var author = new Author();
            author.Id = data.GetInt32(0);
            author.FullName = data.GetString(1);
            author.Country = data.GetString(2);
            author.BirthYear = data.GetInt32(3);

            authors.Add(author);
        }

        return authors;
    }

    public Author? GetById(int id)
    {
        using var connection = Database.Connection();
        connection.Open();

        var query = "select id, full_name, country, birth_year from authors where id = @id";
        using var cmd = new NpgsqlCommand(query, connection);
        cmd.Parameters.AddWithValue("@id", id);

        using var data = cmd.ExecuteReader();

        if (data.Read())
        {
            var author = new Author();
            author.Id = data.GetInt32(0);
            author.FullName = data.GetString(1);
            author.Country = data.GetString(2);
            author.BirthYear = data.GetInt32(3);

            return author;
        }

        return null;
    }

    public void Update(Author author)
    {
        using var connection = Database.Connection();
        connection.Open();

        var query = "update authors set full_name = @fullname, country = @country, birth_year = @birthyear where id = @id";

        using var cmd = new NpgsqlCommand(query, connection);
        cmd.Parameters.AddWithValue("@fullname", author.FullName);
        cmd.Parameters.AddWithValue("@country", author.Country);
        cmd.Parameters.AddWithValue("@birthyear", author.BirthYear);
        cmd.Parameters.AddWithValue("@id", author.Id);

        cmd.ExecuteNonQuery();

        Console.WriteLine("Updated!");
    }

    public void Delete(int id)
    {
        using var connection = Database.Connection();
        connection.Open();

        var query = "delete from authors where id = @id";
        using var cmd = new NpgsqlCommand(query, connection);
        cmd.Parameters.AddWithValue("@id", id);

        cmd.ExecuteNonQuery();

        Console.WriteLine("Deleted!");
    }
}
