using Domain.Models;
using Infrastructure.Repositories;

var repo = new BookRepository();
var authorRepo = new AuthorRepository();

while (true)
{
    System.Console.WriteLine("======================================");
    System.Console.WriteLine(@"1. Show all books.
2. Show book by id.
3. Add new book.
4. Update book.
5. Delete book.
6. Manage authors.
0. Exit.");
    var a = Console.ReadLine();
    System.Console.WriteLine("======================================");

    switch (a)
    {
        case "1":
            var books = repo.GetBooks();
            foreach(var b in books)
            {
                System.Console.WriteLine(b.Id+" "+b.Title+" "+b.PublishedYear+" "+b.TotalCopies);
            }
            break;
        case "2":
            System.Console.Write("Enter id: ");
            var id = Convert.ToInt32(Console.ReadLine());
            var bb = repo.GetBook(id);
            System.Console.WriteLine(bb.Id+" "+bb.Title+" "+bb.PublishedYear+" "+bb.TotalCopies);
            break;
        case "3":
            System.Console.Write("Enter title: ");
            var t = Console.ReadLine()!;
            System.Console.Write("Enter published year: ");
            var py = Convert.ToInt32(Console.ReadLine());
            System.Console.Write("Enter total copies: ");
            var tc = Convert.ToInt32(Console.ReadLine());
            var bbb = new Book()
            {
                Title = t,
                PublishedYear = py,
                TotalCopies = tc
            };
            repo.AddBook(bbb);
            break;
        case "4":
            System.Console.Write("Enter id: ");
            var uid = Convert.ToInt32(Console.ReadLine());
            System.Console.Write("Enter title: ");
            var ut = Console.ReadLine()!;
            System.Console.Write("Enter published year: ");
            var upy = Convert.ToInt32(Console.ReadLine());
            System.Console.Write("Enter total copies: ");
            var utc = Convert.ToInt32(Console.ReadLine());
            var ubbb = new Book()
            {
                Id = uid,
                Title = ut,
                PublishedYear = upy,
                TotalCopies = utc
            };
            repo.UpdateBook(ubbb);
            break;
        case "5":
            System.Console.Write("Enter id: ");
            var did = Convert.ToInt32(Console.ReadLine());
            repo.DeleteBook(did);
            break;

        case "6":
        {
            while (true)
            {
                System.Console.WriteLine("===== AUTHORS =====");
                System.Console.WriteLine(@"1. Show all authors.
2. Show author by id.
3. Add author.
4. Update author.
5. Delete author.
0. Back.");

                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        var authors = authorRepo.GetAll();

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

                     var found = authorRepo.GetById(authorId);

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

                         authorRepo.Add(newAuthor);
                        break;

                    case "4":
                        Console.Write("Enter id: ");
                        var updateId = Convert.ToInt32(Console.ReadLine());

                        var existing = authorRepo.GetById(updateId);

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

                        authorRepo.Update(existing);
                        break;

                    case "5":
                        Console.Write("Enter id: ");
                        var deleteId = Convert.ToInt32(Console.ReadLine());

                        authorRepo.Delete(deleteId);
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
        }

        case "0":
            System.Console.WriteLine("Exiting ...");
            return;
    }
}
