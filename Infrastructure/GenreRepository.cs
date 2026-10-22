
using Domain.Interfaces;
using Domain.Models;
using Infrastructure.Data;
using Npgsql;

namespace Infrastructure.Repositories;

public class GenreRepository : IGenreRepository
{
    public void Add(Genre genre)
    {
        using var connection = Database.Connection();
        connection.Open();

        var query = "insert into genres(name) values(@name)";

        using var cmd = new NpgsqlCommand(query, connection);
        cmd.Parameters.AddWithValue("@name", genre.Name ?? "");

        cmd.ExecuteNonQuery();

        Console.WriteLine("Added!");
    }

    public List<Genre> GetAll()
    {
        using var connection = Database.Connection();
        connection.Open();

        var query = "select id, name from genres";

        using var cmd = new NpgsqlCommand(query, connection);
        using var data = cmd.ExecuteReader();

        var genres = new List<Genre>();

        while (data.Read())
        {
            var genre = new Genre();
            genre.Id = data.GetInt32(0);
            genre.Name = data.GetString(1);

            genres.Add(genre);
        }

        return genres;
    }

    public Genre? GetById(int id)
    {
        using var connection = Database.Connection();
        connection.Open();

        var query = "select id, name from genres where id = @id";

        using var cmd = new NpgsqlCommand(query, connection);
        cmd.Parameters.AddWithValue("@id", id);

        using var data = cmd.ExecuteReader();

        if (data.Read())
        {
            var genre = new Genre();
            genre.Id = data.GetInt32(0);
            genre.Name = data.GetString(1);

            return genre;
        }

        return null;
    }

    public void Update(Genre genre)
    {
        using var connection = Database.Connection();
        connection.Open();

        var query = "update genres set name = @name where id = @id";

        using var cmd = new NpgsqlCommand(query, connection);
        cmd.Parameters.AddWithValue("@name", genre.Name ?? "");
        cmd.Parameters.AddWithValue("@id", genre.Id);

        cmd.ExecuteNonQuery();

        Console.WriteLine("Updated!");
    }

    public void Delete(int id)
    {
        using var connection = Database.Connection();
        connection.Open();

        var query = "delete from genres where id = @id";

        using var cmd = new NpgsqlCommand(query, connection);
        cmd.Parameters.AddWithValue("@id", id);

        cmd.ExecuteNonQuery();

        Console.WriteLine("Deleted!");
    }
}
