using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryApp
{
    class Film : Item
    {
        public string Director { get; set; }
        public string ReleasedYear { get; set; }

        public Film(string title, string genre, int copies, string director, string releaseYear) : base(title, genre, copies)
        {
            Director = director;
            ReleasedYear = releaseYear;
        }

    }

    class SortFilmsByTitle : IComparer<Film>
    {
        public int Compare(Film x, Film y)
        {
            return x.Title.CompareTo(y.Title);
        }
    }
}
