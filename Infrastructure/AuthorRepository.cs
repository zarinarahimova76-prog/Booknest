
using Domain.Interfaces;
using Domain.Models;
using Infrastructure.Data;
using Npgsql;

namespace Infrastructure.Repositories;

public class AuthorRepository : IAuthorRepository
{
    public async Task Add(Author author)
    {
        await using var connection = Database.Connection();
        await connection.OpenAsync();

        var query = "insert into authors(full_name, country, birth_year) values(@fullname, @country, @birthyear)";

        await using var cmd = new NpgsqlCommand(query, connection);
        cmd.Parameters.AddWithValue("fullname", author.FullName);
        cmd.Parameters.AddWithValue("country", author.Country);
        cmd.Parameters.AddWithValue("birthyear", author.BirthYear);

        await cmd.ExecuteNonQueryAsync();

        Console.WriteLine("Added!");
    }

    public async Task<List<Author>> GetAll()
    {
        await using var connection = Database.Connection();
        await connection.OpenAsync();

        var query = "select id, full_name, country, birth_year from authors";

        await using var cmd = new NpgsqlCommand(query, connection);
        await using var data = await cmd.ExecuteReaderAsync();

        var authors = new List<Author>();

        while (await data.ReadAsync())
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

    public async Task<Author?> GetById(int id)
    {
        await using var connection = Database.Connection();
        await connection.OpenAsync();

        var query = "select id, full_name, country, birth_year from authors where id = @id";

        await using var cmd = new NpgsqlCommand(query, connection);
        cmd.Parameters.AddWithValue("id", id);

        await using var data = await cmd.ExecuteReaderAsync();

        if (await data.ReadAsync())
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

    public async Task Update(Author author)
    {
        await using var connection = Database.Connection();
        await connection.OpenAsync();

        var query = "update authors set full_name = @fullname, country = @country, birth_year = @birthyear where id = @id";

        await using var cmd = new NpgsqlCommand(query, connection);

        cmd.Parameters.AddWithValue("fullname", author.FullName);
        cmd.Parameters.AddWithValue("country", author.Country);
        cmd.Parameters.AddWithValue("birthyear", author.BirthYear);
        cmd.Parameters.AddWithValue("id", author.Id);

        await cmd.ExecuteNonQueryAsync();

        Console.WriteLine("Updated!");
    }

    public async Task Delete(int id)
    {
        await using var connection = Database.Connection();
        await connection.OpenAsync();

        var query = "delete from authors where id = @id";

        await using var cmd = new NpgsqlCommand(query, connection);
        cmd.Parameters.AddWithValue("id", id);

        await cmd.ExecuteNonQueryAsync();

        Console.WriteLine("Deleted!");
    }
}
