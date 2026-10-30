
using Dapper;
using Domain.Interfaces;
using Domain.Models;
using Infrastructure.Data;

namespace Infrastructure.Repositories;

public class AuthorRepository : IAuthorRepository
{
    public async Task Add(Author author)
    {
        await using var connection = Database.Connection();

        var query = "insert into authors(full_name, country, birth_year) values(@FullName, @Country, @BirthYear)";

        await connection.ExecuteAsync(query, author);

        Console.WriteLine("Added!");
    }

    public async Task<List<Author>> GetAll()
    {
        await using var connection = Database.Connection();

        var query = "select id, full_name, country, birth_year from authors";

        var authors = await connection.QueryAsync<Author>(query);

        return authors.ToList();
    }

    public async Task<Author?> GetById(int id)
    {
        await using var connection = Database.Connection();

        var query = "select id, full_name, country, birth_year from authors where id = @Id";

        return await connection.QueryFirstOrDefaultAsync<Author>(query, new { Id = id });
    }

    public async Task Update(Author author)
    {
        await using var connection = Database.Connection();

        var query = "update authors set full_name = @FullName, country = @Country, birth_year = @BirthYear where id = @Id";

        await connection.ExecuteAsync(query, author);

        Console.WriteLine("Updated!");
    }

    public async Task Delete(int id)
    {
        await using var connection = Database.Connection();

        var query = "delete from authors where id = @Id";

        await connection.ExecuteAsync(query, new { Id = id });

        Console.WriteLine("Deleted!");
    }
}
