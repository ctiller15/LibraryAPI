using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace LibraryAPI.Data
{
    public class LibraryContext:DbContext
    {
        public MoviesContext() : base("name=DefaultConnection")
        {

        }

    }
}