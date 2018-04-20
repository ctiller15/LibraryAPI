using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using LibraryAPI.Data;
using LibraryAPI.Models;
using LibraryAPI.ViewModels.BookModels;


namespace LibraryAPI.Controllers
{
    public class BooksController : ApiController
    {
        static Author CheckAuthor(PostBook book)
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

        static Genre CheckGenre(PostBook book)
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

        // GET: Find a book based on title, author, or genre
        public IEnumerable<Book> Get([FromUri]GetBook book)
        {
            using (var db = new LibraryContext())
            {
                IQueryable<Book> query = db.Books;

                if(!String.IsNullOrEmpty(book.BookTitle))
                {
                    query = query.Where(w => w.Title == book.BookTitle);
                }

                if (!String.IsNullOrEmpty(book.BookAuthor))
                {
                    query = query.Where(w => w.Author.Name == book.BookAuthor);
                }

                if (!String.IsNullOrEmpty(book.BookGenre))
                {
                    query = query.Where(w => w.Genre.DisplayName == book.BookGenre);
                }

                if(book.IsCheckedOut != null)
                {
                    query = query.Where(w => w.IsCheckedOut == book.IsCheckedOut);
                }

                return query.ToList();
            }
        }

        // POST: Add a new Book
        public IHttpActionResult Post(PostBook book)
        {
            Author author = CheckAuthor(book);
            Genre genre = CheckGenre(book);

            var newBook = new Book
            {
                Title = book.BookTitle,
                YearPublished = book.YearPublished,
                Condition = book.Condition,

                // Get Author name.
                AuthorID = author.ID,

                // Get Genre.
                GenreID = genre.ID,

                ISBN = book.ISBN,
                IsCheckedOut = book.IsCheckedOut,
                DueBackDate = book.DueBackDate
            };

            var db = new LibraryContext();
            db.Books.Add(newBook);
            db.SaveChanges();
            newBook.Author = author;
            newBook.Genre = genre;
            return Ok(newBook);
        }

        // PUT: Update an existing book.
        public IHttpActionResult Put(int ID, [FromBody] UpdateBook book)
        {
            var db = new LibraryContext();
            var BookToUpdate = db.Books.First(x => x.ID == ID);
            BookToUpdate.IsCheckedOut = book.IsCheckedOut;
            db.SaveChanges();
            return Ok(BookToUpdate);
        }

    }
}
