using MongoDB.Bson;
using Repository.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository
{
    public class BookRepository
    {
        /// <summary>
        /// Gets a new book as an object and saves in db
        /// </summary>
        /// <param name="newBook"></param>
        public static void CreateBook(Book newBook)
        {
            Database db = new Database();
            db.CreateBook(newBook);
        }

        /// <summary>
        /// Gets all books from db and returns the list
        /// </summary>
        /// <returns>returns a list of all books</returns>
        public static List<Book> GetBooks()
        {
            Database db = new Database();
            List<Book> allBooks = db.GetBooks();
            return allBooks;
        }

        public static Book GetBookById(ObjectId id)
        {
            Database db = new Database();
            return db.GetBookById(id);
        }

        /// <summary>
        /// Updates amount copies of an object in db
        /// </summary>
        /// <param name="type"></param>
        /// <param name="id"></param>
        /// <param name="newCopies"></param>
        public static void UpdateCopies(Type type, ObjectId id, int newCopies)
        {
            Database db = new Database();
            db.UpdateCopies(typeof(Book), id, newCopies);
        }

        /// <summary>
        /// Gets an book id and deletes it from db
        /// </summary>
        /// <param name="id"></param>
        public static void DeleteBookById(ObjectId id)
        {
            Database db = new Database();
            db.DeleteBookById(id);
        }

        public static void UpdateBook(Book book)
        {
            Database db = new Database();
            db.UpdateBook(book);
        }

        /// <summary>
        /// Checks if book if free to rent between entered start and end rent's date 
        /// </summary>
        /// <param name="book"></param>
        /// <returns>True if book if free to rent and false if not</returns>
        public static bool BookIsFreeToRent(Rent rent)
        {
            List<Rent> allSameBookRented = RentRepository.GetAllSameBookRented(rent.RentedBook);
            int rentedCopies = 0;

            for (int i = 0; i < allSameBookRented.Count; i++)
            {
                if (rent.StartDate >= allSameBookRented[i].StartDate && rent.StartDate <= allSameBookRented[i].EndDate)
                {
                    rentedCopies++;
                }
            }

            if (rentedCopies >= rent.RentedBook.Copies)
            {
                return false;
            }
            return true;
        }

    }
}
