using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using LibraryAPI.Data;
using LibraryAPI.Models;


namespace LibraryAPI.Controllers
{
    public class BooksController : ApiController
    {
        // POST: Add a new Book
        public IHttpActionResult Post([FromBody]Dictionary<string, string> book)
        {
            var newBook = new Book
            {
                Title = book["Title"],
                YearPublished = Convert.ToInt32(book["YearPublished"]),
                Condition = book["Condition"],
                // Don't worry about Author right now...
                // Don't worry about Genre right now...
                ISBN = book["ISBN"],
                IsCheckedOut = Convert.ToBoolean(book["IsCheckedOut"]),
                DueBackDate = Convert.ToDateTime(book["DueBackDate"])
            };

            var db = new LibraryContext();
            db.Books.Add(newBook);
            db.SaveChanges();
            return Ok(newBook);
        }


    }
}
