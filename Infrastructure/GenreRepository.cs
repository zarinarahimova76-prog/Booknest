
using Domain.Interfaces;
using Domain.Models;
using Infrastructure.Data;
using Npgsql;

namespace Infrastructure.Repositories;

public class GenreRepository : IGenreRepository
{
    public async Task Add(Genre genre)
    {
        await using var connection = Database.Connection();
        await connection.OpenAsync();

        var query = "insert into genres(name) values(@name)";

        await using var cmd = new NpgsqlCommand(query, connection);
        cmd.Parameters.AddWithValue("@name", genre.Name ?? "");

        await cmd.ExecuteNonQueryAsync();

        Console.WriteLine("Added!");
    }

    public async Task<List<Genre>> GetAll()
    {
        await using var connection = Database.Connection();
        await connection.OpenAsync();

        var query = "select id, name from genres";

        await using var cmd = new NpgsqlCommand(query, connection);
        await using var data = await cmd.ExecuteReaderAsync();

        var genres = new List<Genre>();

        while (await data.ReadAsync())
        {
            var genre = new Genre();
            genre.Id = data.GetInt32(0);
            genre.Name = data.GetString(1);

            genres.Add(genre);
        }

        return genres;
    }

    public async Task<Genre?> GetById(int id)
    {
        await using var connection = Database.Connection();
        await connection.OpenAsync();

        var query = "select id, name from genres where id = @id";

        await using var cmd = new NpgsqlCommand(query, connection);
        cmd.Parameters.AddWithValue("@id", id);

        await using var data = await cmd.ExecuteReaderAsync();

        if (await data.ReadAsync())
        {
            var genre = new Genre();
            genre.Id = data.GetInt32(0);
            genre.Name = data.GetString(1);

            return genre;
        }

        return null;
    }

    public async Task Update(Genre genre)
    {
        await using var connection = Database.Connection();
        await connection.OpenAsync();

        var query = "update genres set name = @name where id = @id";

        await using var cmd = new NpgsqlCommand(query, connection);
        cmd.Parameters.AddWithValue("@name", genre.Name ?? "");
        cmd.Parameters.AddWithValue("@id", genre.Id);

        await cmd.ExecuteNonQueryAsync();

        Console.WriteLine("Updated!");
    }

    public async Task Delete(int id)
    {
        await using var connection = Database.Connection();
        await connection.OpenAsync();

        var query = "delete from genres where id = @id";

        await using var cmd = new NpgsqlCommand(query, connection);
        cmd.Parameters.AddWithValue("@id", id);

        await cmd.ExecuteNonQueryAsync();

        Console.WriteLine("Deleted!");
    }
}
