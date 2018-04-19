using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LibraryAPI.ViewModels.BookModels
{
    public class PostBook
    {
        public string BookTitle { get; set; }
        public int? YearPublished { get; set; }
        public string Condition { get; set; }
        public string AuthorName { get; set; }
        public string GenreName { get; set; }
        public string ISBN { get; set;}
        public bool IsCheckedOut { get; set; }
        public DateTime DueBackDate { get; set; }
    }
}