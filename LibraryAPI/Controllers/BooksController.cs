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

        // Not DRY at all...
        // PUT: Checkout an existing book.
        [Route("api/books/checkout/{ID?}")]
        [HttpPut]
        public IHttpActionResult CheckBookOut(string ID, PutCheckout userCheckout)
        {
            int bookID = Convert.ToInt32(ID);
            var db = new LibraryContext();
            var BookToUpdate = db.Books.First(x => x.ID == bookID);
            BookToUpdate.IsCheckedOut = true;
            // Create the checkout.
            var checkout = new Checkout
            {
                //BookID = bookID,
                TimeStamp = DateTime.Now,
                Email = userCheckout.Email,
                BookStatus = "Checking out"
            };
            BookToUpdate.Checkout.Add(checkout);
            //db.Checkouts.Add(checkout);
            var returnData = new PutCheckoutBody(BookToUpdate, checkout);

            db.SaveChanges();
            return Ok(returnData);
        }

        [Route("api/books/checkin/{ID?}")]
        [HttpPut]
        public IHttpActionResult CheckBookIn(string ID, PutCheckout userCheckin)
        {
            int bookID = Convert.ToInt32(ID);
            var db = new LibraryContext();
            var BookToUpdate = db.Books.First(x => x.ID == bookID);

            BookToUpdate.IsCheckedOut = false;
            // Create the checkin.
            var checkin = new Checkout
            {
                TimeStamp = DateTime.Now,
                Email = userCheckin.Email,
                BookStatus = "Checking in"
            };
            BookToUpdate.Checkout.Add(checkin);

            //Creating the return data.
            //Turns out I needed another model for this.
            // It may be possible and/or ideal to just make sure your first model functions for both the request and the response.
            // The database model should not be returned.
            var returnData = new PutCheckoutBody(BookToUpdate, checkin);

            db.SaveChanges();
            return Ok(returnData);
        }


    }
}
