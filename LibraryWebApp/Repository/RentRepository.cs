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
        /// Gets a rent by rent id from db
        /// </summary>
        /// <param name="id"></param>
        /// <returns>A rent as an object</returns>
        public static Rent GetRentById(ObjectId id)
        {
            Database db = new Database(); ;
            return db.GetRentById(id);
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

        /// <summary>
        /// Gets a date as an argument and checks if date is not befor today's date
        /// </summary>
        /// <param name="startDate"></param>
        /// <returns>True if is correct</returns>
        public static bool IsStartDateCorrect(DateTime startDate)
        {
            int startDateResultat = DateTime.Compare(DateTime.Now.Date, startDate);

            if (startDateResultat <= 0)//checks if date is in right format and start date is not before today's date
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Gets 2 dates and checks if the first date is not before second date
        /// </summary>
        /// <param name="endDate"></param>
        /// <param name="startDate"></param>
        /// <returns></returns>
        public static bool IsEndDateCorrect(DateTime endDate, DateTime startDate)
        {
            int endDateResultat = DateTime.Compare(endDate, startDate);//Checks to not enter an end date before start date

            if (endDateResultat > 0)//checks if date is in right format and end date is not before start date
            {
                return true;
            }
            return false;
        }
    }
}
