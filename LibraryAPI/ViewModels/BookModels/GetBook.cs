using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LibraryAPI.ViewModels.BookModels
{
    public class GetBook
    {
        public string BookTitle { get; set; }
        public string BookAuthor { get; set; }
        public string BookGenre { get; set; }
        public bool? IsCheckedOut { get; set; }
    }
}