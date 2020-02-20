using System;
using Microsoft.EntityFrameworkCore;

namespace IndyBooks.Models
{
    public class IndyBooksDataContext:DbContext
    {
        public IndyBooksDataContext(DbContextOptions<IndyBooksDataContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        //TODO: Define DbSets for Collections representing DB tables
        public DbSet<Book> Books { get; set; }
    }
}
