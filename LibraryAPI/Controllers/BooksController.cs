using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using LibraryAPI.Data;
using LibraryAPI.Models;
using LibraryAPI.ViewModels.BookModels;
using LibraryAPI.ViewModels.CheckoutModels;
using LibraryAPI.Services;


namespace LibraryAPI.Controllers
{
    public class BooksController : ApiController
    {
        // GET: Find a book based on title, author, or genre
        [Route("api/books")]
        [HttpGet]
        public IEnumerable<Book> SearchBooks([FromUri]GetBook book)
        {
            using (var db = new LibraryContext())
            {
                var query = db.Books
                            .Include(i => i.Author)
                            .Include(i => i.Genre);

                var parsedQuery = Query.ParseBook(book, query);

                return parsedQuery.ToList();
            }
        }

        // POST: Add a new Book
        [Route("api/books")]
        [HttpPost]
        public IHttpActionResult CreateBook(PostBook book)
        {
            Author author = DataChecks.CheckAuthor(book);
            Genre genre = DataChecks.CheckGenre(book);

            // The null value of DateTime is 0001, 01, 01.
            if (book.DueBackDate == new DateTime(0001,01,01))
            {
                book.DueBackDate = new DateTime(1990, 01, 01);
            }

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
            // Tack properties on to the newBook and then return.
            newBook.Author = author;
            newBook.Genre = genre;
            return Ok(newBook);
        }

        //PUT: Check out or check in a book.
        [Route("api/books/checkoutin/{bookID}")]
        [HttpPut]
        public IHttpActionResult CheckBook([FromUri]int bookID, [FromBody]PutCheckout userCheckout) {
            var db = new LibraryContext();
            var BookToUpdate = db.Books.First(x => x.ID == bookID);

            var checkout = new Checkout
            {
                TimeStamp = DateTime.Now,
                Email = userCheckout.Email,
            };

            if(userCheckout.Mode == "checkout")
            {
                BookToUpdate.IsCheckedOut = true;
                checkout.BookStatus = "checking out";
            } else if(userCheckout.Mode == "checkin")
            {
                BookToUpdate.IsCheckedOut = false;
                checkout.BookStatus = "checking in";
            }

            BookToUpdate.Checkout.Add(checkout);
            var returnData = new PutCheckoutBody(BookToUpdate, checkout);

            db.SaveChanges();
            return Ok(returnData);
        }

    }
}
