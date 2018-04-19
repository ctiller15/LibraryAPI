using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LibraryAPI.Models
{
    public class Checkout
    {
        public int ID { get; set; }
        
        public int? BookID { get; set; }
        public Book Book { get; set; }

        public DateTime TimeStamp { get; set; }
        public string Email { get; set; }
    }
}