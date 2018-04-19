using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LibraryAPI.Models
{
    public class Author
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int? Born { get; set; }
        public int? Died { get; set; }
    }
}