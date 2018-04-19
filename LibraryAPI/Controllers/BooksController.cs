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
