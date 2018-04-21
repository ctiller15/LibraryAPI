using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LibraryAPI.Models
{
    public class Book
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public int? YearPublished { get; set; }
        public string Condition { get; set; }

        public int? AuthorID { get; set; }
        public Author Author { get; set; }

        public int? GenreID { get; set; }
        public Genre Genre { get; set; }

        public string ISBN { get; set; }
        public bool IsCheckedOut { get; set; } = true;
        public DateTime DueBackDate { get; set; }

        public ICollection<Checkout> Checkout { get; set; } = new HashSet<Checkout>();
    }
}