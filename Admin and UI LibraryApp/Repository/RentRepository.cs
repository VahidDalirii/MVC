using MongoDB.Bson;
using Repository.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository
{
    public class RentRepository
    {
        /// <summary>
        /// Creates a new rent in db
        /// </summary>
        /// <param name="newRent"></param>
        public static void CreateRent(Rent newRent)
        {
            Database db = new Database();
            db.CreateRent(newRent);
        }
        
        /// <summary>
        /// Gets rent's id and Deletes it from db
        /// </summary>
        /// <param name="id"></param>
        public static void DeleteRentById(ObjectId id)
        {
            Database db = new Database();
            db.DeleteRentById(id);
        }

        /// <summary>
        /// Gets att rents from db
        /// </summary>
        /// <returns>returns a list of all rents</returns>
        public static List<Rent> GetRents()
        {
            Database db = new Database();
            List<Rent> allRents = db.GetRents();
            return allRents;
        }

        /// <summary>
        /// Gets all rented book with same properties values from db and returns them
        /// </summary>
        /// <param name="book"></param>
        /// <returns>returnes all rented book with same properties values</returns>
        public static List<Rent> GetAllSameBookRented(Book book)
        {
            Database db = new Database();
            List<Rent> bookRentedList = db.GetAllSameBookRented(book);
            return bookRentedList;
        }

        /// <summary>
        /// Gets all rented film with same properties values from db and returns them
        /// </summary>
        /// <param name="film"></param>
        /// <returns>returnes all rented film with same properties values</returns>
        public static List<Rent> GetAllSameFilmRented(Film film)
        {
            Database db = new Database();
            List<Rent> filmRentedList = db.GetAllSameFilmRented(film);
            return filmRentedList;
        }

        /// <summary>
        /// Gets all rents of a member from db and returns them
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static List<Rent> GetRentsByMemberId(ObjectId id)
        {
            Database db = new Database();
            List<Rent> memberRents = db.GetRentsByMemberId(id);
            return memberRents;
        }

        /// <summary>
        /// Gets a member's rents from db and returns them
        /// </summary>
        /// <param name="member"></param>
        /// <returns></returns>
        public static List<Rent> GetMyRents(Member member)
        {
            Database db = new Database();
            List<Rent> myRents = db.GetRentsByMemberId(member.Id);
            return myRents;
        }

    }
}
