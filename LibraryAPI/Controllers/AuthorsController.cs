using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using LibraryAPI.Models;
using LibraryAPI.Data;

namespace LibraryAPI.Controllers
{
    public class AuthorsController : ApiController
    {
        // POST: Add a new Author
        public IHttpActionResult Post([FromBody]Dictionary<string, string> author)
        {
            var newAuthor = new Author
            {
                Name = author["Name"],
                Born = Convert.ToInt32(author["Born"]),
                Died = Convert.ToInt32(author["Died"])
            };

            var db = new LibraryContext();
            db.Authors.Add(newAuthor);
            db.SaveChanges();
            return Ok(newAuthor);
        }
    }
}
