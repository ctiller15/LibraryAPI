using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LibraryAPI.ViewModels.AuthorModels
{
    public class PostParams
    {
        public string AuthorName { get; set; }
        public int? AuthorBorn { get; set; }
        public int? AuthorDied { get; set; }
    }
}