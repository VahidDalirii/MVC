using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.Models
{
    public class Rent
    {
        public ObjectId Id { get; set; }
        public Member RentingMember { get; set; }
        public Book RentedBook { get; set; }
        public Film RentedFilm { get; set; }
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime StartDate { get; set; }
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime EndDate { get; set; }

        public Rent(Member rentingMember, Book rentedBook, Film rentedfilm, DateTime startDate, DateTime endDate)
        {
            RentingMember = rentingMember;
            RentedBook = rentedBook;
            RentedFilm = rentedfilm;
            StartDate = startDate;
            EndDate = endDate;
        }
    }

    public class SortRentsByMemberName : IComparer<Rent>
    {
        public int Compare(Rent x, Rent y)
        {
            return x.RentingMember.Name.CompareTo(y.RentingMember.Name);
        }
    }
}
