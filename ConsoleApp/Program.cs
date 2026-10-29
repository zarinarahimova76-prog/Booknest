
using Domain.Models;
using Infrastructure.Repositories;

class Program
{
    static async Task Main()
    {
        var repo = new BookRepository();
        var authorRepo = new AuthorRepository();
        var genreRepo = new GenreRepository();

        while (true)
        {
            Console.WriteLine("======================================");
            Console.WriteLine(@"1. Show all books.
2. Show book by id.
3. Add new book.
4. Update book.
5. Delete book.
6. Manage authors.
7. Manage genres.
8. Find books by author.
9. Find books by genre.
0. Exit.");

            var a = Console.ReadLine();

            Console.WriteLine("======================================");

            switch (a)
            {
                case "1":
                    var books = await repo.GetBooks();

                    foreach (var b in books)
                    {
                        Console.WriteLine(b.Id + " " + b.Title + " " + b.PublishedYear + " " + b.TotalCopies);
                    }
                    break;

                case "2":
                    Console.Write("Enter id: ");
                    var id = Convert.ToInt32(Console.ReadLine());

                    var bb = await repo.GetBook(id);

                    Console.WriteLine(bb.Id + " " + bb.Title + " " + bb.PublishedYear + " " + bb.TotalCopies);
                    break;

                case "3":
                    Console.Write("Enter title: ");
                    var t = Console.ReadLine()!;

                    Console.Write("Enter published year: ");
                    var py = Convert.ToInt32(Console.ReadLine());

                    Console.Write("Enter total copies: ");
                    var tc = Convert.ToInt32(Console.ReadLine());

                    var bbb = new Book()
                    {
                        Title = t,
                        PublishedYear = py,
                        TotalCopies = tc
                    };

                    await repo.AddBook(bbb);
                    break;

                case "4":
                    Console.Write("Enter id: ");
                    var uid = Convert.ToInt32(Console.ReadLine());

                    Console.Write("Enter title: ");
                    var ut = Console.ReadLine()!;

                    Console.Write("Enter published year: ");
                    var upy = Convert.ToInt32(Console.ReadLine());

                    Console.Write("Enter total copies: ");
                    var utc = Convert.ToInt32(Console.ReadLine());

                    var ubbb = new Book()
                    {
                        Id = uid,
                        Title = ut,
                        PublishedYear = upy,
                        TotalCopies = utc
                    };

                    await repo.UpdateBook(ubbb);
                    break;

                case "5":
                    Console.Write("Enter id: ");
                    var did = Convert.ToInt32(Console.ReadLine());

                    await repo.DeleteBook(did);
                    break;

                case "6":
                    while (true)
                    {
                        Console.WriteLine("===== AUTHORS =====");
                        Console.WriteLine(@"1. Show all authors.
2. Show author by id.
3. Add author.
4. Update author.
5. Delete author.
0. Back.");

                        var choice = Console.ReadLine();

                        switch (choice)
                        {
                            case "1":
                                var authors = await authorRepo.GetAll();

                                foreach (var author in authors)
                                {
                                    Console.WriteLine(
                                        author.Id + " " +
                                        author.FullName + " " +
                                        author.Country + " " +
                                        author.BirthYear);
                                }
                                break;

                            case "2":
                                Console.Write("Enter id: ");
                                var authorId = Convert.ToInt32(Console.ReadLine());

                                var found = await authorRepo.GetById(authorId);

                                if (found != null)
                                {
                                    Console.WriteLine(
                                        found.Id + " " +
                                        found.FullName + " " +
                                        found.Country + " " +
                                        found.BirthYear);
                                }
                                else
                                {
                                    Console.WriteLine("Author not found!");
                                }
                                break;

                            case "3":
                                Console.Write("Enter fullname: ");
                                var fullname = Console.ReadLine()!;

                                Console.Write("Enter country: ");
                                var country = Console.ReadLine()!;

                                Console.Write("Enter birth year: ");
                                var birthyear = Convert.ToInt32(Console.ReadLine());

                                var newAuthor = new Author()
                                {
                                    FullName = fullname,
                                    Country = country,
                                    BirthYear = birthyear
                                };

                                await authorRepo.Add(newAuthor);
                                break;

                            case "4":
                                Console.Write("Enter id: ");
                                var updateId = Convert.ToInt32(Console.ReadLine());

                                var existing = await authorRepo.GetById(updateId);

                                if (existing == null)
                                {
                                    Console.WriteLine("Author not found!");
                                    break;
                                }

                                Console.Write("Enter fullname: ");
                                existing.FullName = Console.ReadLine()!;

                                Console.Write("Enter country: ");
                                existing.Country = Console.ReadLine()!;

                                Console.Write("Enter birth year: ");
                                existing.BirthYear = Convert.ToInt32(Console.ReadLine());

                                await authorRepo.Update(existing);
                                break;

                            case "5":
                                Console.Write("Enter id: ");
                                var deleteId = Convert.ToInt32(Console.ReadLine());

                                await authorRepo.Delete(deleteId);
                                break;

                            case "0":
                                break;

                            default:
                                Console.WriteLine("Wrong choice!");
                                break;
                        }

                        if (choice == "0")
                        {
                            break;
                        }
                    }
                    break;

                case "7":
                    while (true)
                    {
                        Console.WriteLine("===== GENRES =====");
                        Console.WriteLine(@"1. Show all genres.
2. Show genre by id.
3. Add genre.
4. Update genre.
5. Delete genre.
0. Back.");

                        var choice = Console.ReadLine();

                        switch (choice)
                        {
                            case "1":
                                var genres = await genreRepo.GetAll();

                                foreach (var genre in genres)
                                {
                                    Console.WriteLine(genre.Id + " " + genre.Name);
                                }
                                break;

                            case "2":
                                Console.Write("Enter id: ");
                                var genreId = Convert.ToInt32(Console.ReadLine());

                                var foundGenre = await genreRepo.GetById(genreId);

                                if (foundGenre != null)
                                {
                                    Console.WriteLine(foundGenre.Id + " " + foundGenre.Name);
                                }
                                else
                                {
                                    Console.WriteLine("Genre not found!");
                                }
                                break;

                            case "3":
                                Console.Write("Enter genre name: ");
                                var genreName = Console.ReadLine()!;

                                var newGenre = new Genre()
                                {
                                    Name = genreName
                                };

                                await genreRepo.Add(newGenre);
                                break;

                            case "4":
                                Console.Write("Enter id: ");
                                var updateGenreId = Convert.ToInt32(Console.ReadLine());

                                var existingGenre = await genreRepo.GetById(updateGenreId);

                                if (existingGenre == null)
                                {
                                    Console.WriteLine("Genre not found!");
                                    break;
                                }

                                Console.Write("Enter new name: ");
                                existingGenre.Name = Console.ReadLine()!;

                                await genreRepo.Update(existingGenre);
                                break;

                            case "5":
                                Console.Write("Enter id: ");
                                var deleteGenreId = Convert.ToInt32(Console.ReadLine());

                                await genreRepo.Delete(deleteGenreId);
                                break;

                            case "0":
                                break;

                            default:
                                Console.WriteLine("Wrong choice!");
                                break;
                        }

                        if (choice == "0")
                        {
                            break;
                        }
                    }
                    break;

                case "8":
                    Console.Write("Enter author id: ");
                    var searchAuthorId = Convert.ToInt32(Console.ReadLine());

                    var authorBooks = await repo.GetBooksByAuthor(searchAuthorId);

                    foreach (var book in authorBooks)
                    {
                        Console.WriteLine(
                            book.Id + " " +
                            book.Title + " " +
                            book.PublishedYear + " " +
                            book.TotalCopies);
                    }

                    if (authorBooks.Count == 0)
                    {
                        Console.WriteLine("No books found!");
                    }
                    break;

                case "9":
                    Console.Write("Enter genre id: ");
                    var searchGenreId = Convert.ToInt32(Console.ReadLine());

                    var genreBooks = await repo.GetBooksByGenre(searchGenreId);

                    foreach (var book in genreBooks)
                    {
                        Console.WriteLine(
                            book.Id + " " +
                            book.Title + " " +
                            book.PublishedYear + " " +
                            book.TotalCopies);
                    }

                    if (genreBooks.Count == 0)
                    {
                        Console.WriteLine("No books found!");
                    }
                    break;

                case "0":
                    Console.WriteLine("Exiting ...");
                    return;
            }
        }
    }
}
