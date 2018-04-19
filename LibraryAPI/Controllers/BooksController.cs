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

                return query.ToList();
            }
        }

        // POST: Add a new Book
        public IHttpActionResult Post(PostBook book)
        {
            var newBook = new Book
            {
                Title = book.BookTitle,
                YearPublished = book.YearPublished,
                Condition = book.Condition,
                // Don't worry about Author right now...
                // Don't worry about Genre right now...
                ISBN = book.ISBN,
                IsCheckedOut = book.IsCheckedOut,
                DueBackDate = book.DueBackDate
            };

            var db = new LibraryContext();
            db.Books.Add(newBook);
            db.SaveChanges();
            return Ok(newBook);
        }


    }
}
