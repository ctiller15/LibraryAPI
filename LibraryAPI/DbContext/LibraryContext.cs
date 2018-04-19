using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using LibraryAPI.Models;

namespace LibraryAPI.Data
{
    public class LibraryContext:DbContext
    {
        public LibraryContext() : base("name=DefaultConnection")
        {

        }

        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Checkout> Checkouts { get; set; }
        public DbSet<Genre> Genres { get; set; }

    }
}