
using Dapper;
using Domain.Interfaces;
using Domain.Models;
using Infrastructure.Data;

namespace Infrastructure.Repositories;

public class GenreRepository : IGenreRepository
{
    public async Task Add(Genre genre)
    {
        await using var connection = Database.Connection();

        var query = "insert into genres(name) values(@Name)";

        await connection.ExecuteAsync(query, new { Name = genre.Name ?? "" });

        Console.WriteLine("Added!");
    }

    public async Task<List<Genre>> GetAll()
    {
        await using var connection = Database.Connection();

        var query = "select id, name from genres";

        var genres = await connection.QueryAsync<Genre>(query);

        return genres.ToList();
    }

    public async Task<Genre?> GetById(int id)
    {
        await using var connection = Database.Connection();

        var query = "select id, name from genres where id = @Id";

        return await connection.QueryFirstOrDefaultAsync<Genre>(
            query, new { Id = id });
    }

    public async Task Update(Genre genre)
    {
        await using var connection = Database.Connection();

        var query = "update genres set name = @Name where id = @Id";

        await connection.ExecuteAsync(query, new
        {
            Name = genre.Name ?? "",
            Id = genre.Id
        });

        Console.WriteLine("Updated!");
    }

    public async Task Delete(int id)
    {
        await using var connection = Database.Connection();

        var query = "delete from genres where id = @Id";

        await connection.ExecuteAsync(query, new { Id = id });

        Console.WriteLine("Deleted!");
    }
}
