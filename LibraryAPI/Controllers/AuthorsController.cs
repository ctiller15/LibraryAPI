using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using LibraryAPI.Models;
using LibraryAPI.Data;
using LibraryAPI.ViewModels.AuthorModels;

namespace LibraryAPI.Controllers
{
    public class AuthorsController : ApiController
    {
        // POST: Add a new Author
        public IHttpActionResult Post(PostParams param)
        {
            var newAuthor = new Author
            {
                Name = param.AuthorName,
                Born = param.AuthorBorn,
                Died = param.AuthorDied
            };

            var db = new LibraryContext();
            db.Authors.Add(newAuthor);
            db.SaveChanges();
            return Ok(newAuthor);
        }
    }
}
