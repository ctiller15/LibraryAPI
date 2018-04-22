﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using LibraryAPI.Data;
using LibraryAPI.Models;
using LibraryAPI.ViewModels.BookModels;
using LibraryAPI.ViewModels.CheckoutModels;


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

        //public Book ShallowCopy()
        //{
        //    return (Book)this.MemberwiseClone();
        //}


        // GET: Find a book based on title, author, or genre
        [Route("api/books")]
        [HttpGet]
        public IEnumerable<Book> SearchBooks([FromUri]GetBook book)
        {
            using (var db = new LibraryContext())
            {
                IQueryable<Book> query = db.Books;

                if(book != null)
                {
                    if (!String.IsNullOrEmpty(book.BookTitle))
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
                }


                return query.ToList();
            }
        }

        // POST: Add a new Book
        [Route("api/books")]
        [HttpPost]
        public IHttpActionResult CreateBook(PostBook book)
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
