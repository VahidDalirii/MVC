using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.Models
{
    public class Book : Item

    {
        public string Author { get; set; }

        public string PublishedYear { get; set; }



        public Book(string title, string genre, int copies, string author, string publishedYear) : base(title, genre, copies)
        {
            Author = author;
            PublishedYear = publishedYear;
        }

    }

    public class SortBooksByTitle : IComparer<Book>
    {
        public int Compare(Book x, Book y)
        {
            return x.Title.CompareTo(y.Title);
        }
    }
}
