using LibraryAPI.Data;
using LibraryAPI.Models;
using LibraryAPI.ViewModels.BookModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LibraryAPI.Services
{
    public class DataChecks
    {
        static public Author CheckAuthor(PostBook book)
        {
            Author author = null;
            var db = new LibraryContext();
            if (!String.IsNullOrEmpty(book.AuthorName))
            {
                var match = db.Authors.FirstOrDefault(x => x.Name == book.AuthorName);

                if (match != null)
                {
                    // Reference match in DB, set it to author.
                    author = match;
                }
                else
                {
                    // Create author.
                    author = new Author
                    {
                        Name = book.AuthorName
                    };
                    db.Authors.Add(author);
                    db.SaveChanges();
                }
            }
            return db.Authors.FirstOrDefault(x => x.Name == book.AuthorName);
        }

        static public Genre CheckGenre(PostBook book)
        {
            Genre genre = null;
            var db = new LibraryContext();
            if (!String.IsNullOrEmpty(book.GenreName))
            {
                var match = db.Genres.FirstOrDefault(x => x.DisplayName == book.GenreName);

                if (match != null)
                {
                    // Reference match in DB, set it to author.
                    genre = match;
                }
                else
                {
                    // Create author.
                    genre = new Genre
                    {
                        DisplayName = book.GenreName
                    };
                    db.Genres.Add(genre);
                    db.SaveChanges();
                }
            }
            return db.Genres.FirstOrDefault(x => x.DisplayName == book.GenreName);
        }
    }
}