using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using LibraryAPI.Models;

namespace LibraryAPI.ViewModels.CheckoutModels
{
    public class PutCheckoutBody
    {
        public int BookID { get; set; }
        public string Title { get; set; }
        public int? YearPublished { get; set; }
        public string Condition { get; set; }
        
        public Author Author { get; set; }
        
        public Genre Genre { get; set; }

        public string ISBN { get; set; }
        public bool IsCheckedOut { get; set; }
        public DateTime DueBackDate { get; set; }

        //public int CheckoutID { get; set; }
        public DateTime Timestamp { get; set; }
        public string Email { get; set; }
        public string BookStatus { get; set; }

        // A duration of two weeks.
        private TimeSpan Duration { get; set; } = new TimeSpan(14, 0, 0, 0);

        public PutCheckoutBody(Book book, Checkout checkout)
        {
            this.BookID = book.ID;
            this.Title = book.Title;
            this.YearPublished = book.YearPublished;
            this.Condition = book.Condition;
            this.Author = book.Author;
            this.Genre = book.Genre;
            this.ISBN = book.ISBN;
            this.IsCheckedOut = book.IsCheckedOut;
            this.DueBackDate = DateTime.Now.Add(Duration);
            //this.CheckoutID = checkout.ID;
            this.Timestamp = checkout.TimeStamp;
            this.Email = checkout.Email;
            this.BookStatus = checkout.BookStatus;
        }
    }
}