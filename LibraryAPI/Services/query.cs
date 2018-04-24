using LibraryAPI.Models;
using LibraryAPI.ViewModels.BookModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LibraryAPI.Services
{
    public class Query
    {
        static public IQueryable<Book> ParseBook(GetBook book, IQueryable<Book> query)
        {
            if (book != null)
            {
                if (!String.IsNullOrEmpty(book.BookTitle))
                {
                    query = query.Where(w => w.Title.Contains(book.BookTitle));
                }

                if (!String.IsNullOrEmpty(book.BookAuthor))
                {
                    query = query.Where(w => w.Author.Name.Contains(book.BookAuthor));
                }

                if (!String.IsNullOrEmpty(book.BookGenre))
                {
                    query = query.Where(w => w.Genre.DisplayName.Contains(book.BookGenre));
                }
            }

            return query;
        }
    }
}