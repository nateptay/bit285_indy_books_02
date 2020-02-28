using System;
using System.Linq;
using System.Threading.Tasks;
using IndyBooks.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Bogus;

namespace IndyBooks
{
    public static class SeedData
    {
        public static async Task InitializeAsync(IServiceProvider services)
        {
            await Seed(services.GetRequiredService<IndyBooksDataContext>());
        }

        public static async Task Seed(IndyBooksDataContext context)
        {
            if (context.Books.Any())
            {
                return; //already has data, don't add any more test data
            }

            var titles = new[] { "The Lord of The Rings", "The Flowers of Evil", "Oedipus", "Lolita", "One Hundred Years of Solitude", "The Flowers of Evil", "Emma", "The Magic Mountain", "David Copperfield", "Wuthering Heights", "Paradise Lost", "Pride and Prejudice", "Tristram Shandy", "The Sound and the Fury", "Gulliver's Travels", "Alice Adventures in Wonderland", "David Copperfield", "Great Expectations", "Les Misérables", "Nineteen Eighty Four", "Anna Karenina", "Nineteen Eighty Four", "Great Expectations", "Moby Dick", "Nineteen Eighty Four", "Pride and Prejudice", "The Metamorphosis", "Pride and Prejudice", "Nineteen Eighty Four", "Gargantua and Pantagruel", " Oedipus at Colonus", "Game of Thrones", "The Old Man and the Sea", "The Magic Mountain", "To Kill a Mockingbird", "Hamlet", "The Brothers Karamazov ", "Great Expectations", "The Lord of The Rings", "The Great Gatsby", "The Brothers Karamazov ", "War and Peace", "To the Lighthouse", "One Thousand and One Nights", "The Red and the Black", "Gulliver's Travels", "Robinson Crusoe", "Nineteen Eighty Four", "The Lord of The Rings", "The Odyssey", "The Good Soldier", "Moby Dick", "To the Lighthouse", "Wuthering Heights", "The Flowers of Evil", "Robinson Crusoe", "Robinson Crusoe", "One Hundred Years of Solitude", "Tristram Shandy", "One Hundred Years of Solitude", "Alice Adventures in Wonderland", "Nineteen Eighty Four", "Invisible Man", "The Red and the Black", "The Magic Mountain", "Gone With the Wind", "The Divine Comedy", "The Canterbury Tales", "Hamlet", "The Red and the Black", "Don Quixote", "Les Misérables", "David Copperfield", "Emma", "Les Misérables", "In Search of Lost Time", "The Divine Comedy", "The Red and the Black", "Nineteen Eighty Four", "Robinson Crusoe" };
            var authorsFirstName = new[] { "J. K. ", "Jin ", "Mark ", "Madeleine ", "Franz ", "Carolyn ", "Ernest ", "Brandon ", "Margaret ", "Guillaume ", "Barbara ", "Horatio ", "Rachel ", "Jack ", "T. S. ", "Stephenie ", "Tennessee ", "C.S. ", "Ray ", "Agatha ", "Herman ", "Sylvia ", "Leo ", "G.R.R. ", "Edgar Allen", "F. Scott ", "Ursula Le ", "William ", "Dr. ", "Doris ", "Kurt ", "Harper ", "James ", "L Frank ", "Stephen ", "Alice ", "Maya ", "Virginia ", "Neil ", "Joyce Carol ", "Paulo ", "Eric ", "Ta-nehisi ", "Haruki ", "Toni ", "Emily ", "Jane ", "Flannery " };
            var authorsLastName = new[] { "Rowling", "Yong", "Twain", "L'Engle", "Kafka", "Brown", "Hemingway", "Sanderson", "Atwood", "Musso", "Cartland", "Alger", "Hollis", "Kerouac", "Eliot", "Meyer", "Williams", "Lewis", "Bradbury", "Christie", "Melville", "Plath", "Tolstoy", "Martin", "Poe", "Fitzgerald", "Le Guinn", "Shakespeare", "Seuss", "Lessing", "Vonnegut", "Lee", "Joyce", "Baum", "King", "Walker", "Angelou", "Woolf", "Gaiman", "Oates", "Coelo", "Fromme", "Coates", "Murakami", "Morrison", "Dickinson", "Austen", "O'Connor" };

            
                    //  TODO: (1) Import NuGet Package "Bogus" fake data generator, then
                    //  TODO: (do after each) Use "dotnet ef database drop", and run the application and inspect your data
                    Randomizer.Seed = new Random(8672309);
                    var firstNameIndex = 0;
                    var lastNameIndex = 0;
            //Writers
            var testWriters = new Faker<Writer>()
                .RuleFor(w => w.FirstName, f => authorsFirstName[firstNameIndex++])
                .RuleFor(w => w.LastName, f=> authorsLastName[lastNameIndex++]);
                    var writers = testWriters.Generate(45); // TODO: (2) create a collection of 45 writers
                    //Books
                    var testBooks = new Faker<Book>()
                        .RuleFor(b => b.Title, t => t.PickRandom(titles))
                        .RuleFor(b => b.SKU, n => n.Random.Replace("IB****-##"))
                        .RuleFor(b => b.Price, f => f.Random.Decimal(9.99M, 149.99M))
                        .RuleFor(b => b.Author, f => f.PickRandom(writers));
                    var books = testBooks.Generate(100); // TODO: (3) create a collection of 100 books

                    //Adds the DbContext Collections to the Database 
                    await context.Books.AddRangeAsync(books);
                    await context.Writers.AddRangeAsync(writers);
                    await context.SaveChangesAsync();
                    
        }
    }
}
