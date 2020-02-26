using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.Models
{
    public abstract class Item
    {
        public ObjectId Id { get; set; }
        public string Title { get; set; }
        public string Genre { get; set; }

        public int Copies { get; set; }




        public int CompareTo(Item otherItem)
        {
            return this.Title.CompareTo(otherItem.Title);
        }

        public Item(string title, string genre, int copies)
        {
            Title = title;
            Genre = genre;
            Copies = copies;
        }
    }
}
