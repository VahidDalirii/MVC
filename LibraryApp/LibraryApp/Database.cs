using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryApp
{
    class Database
    {
        private const string Members_Collection = "Members";
        private const string Books_Collection = "Books";
        private const string Films_Collection = "Films";
        private const string Rents_Collection = "Rents";

        private IMongoDatabase db;

        public Database(string database = "Library")
        {
            MongoClient client = new MongoClient();
            db = client.GetDatabase(database);
        }

        /// <summary>
        /// Creates a member in db
        /// </summary>
        /// <param name="member"></param>
        public void CreateMember(Member member)
        {
            var collection = db.GetCollection<Member>(Members_Collection);
            collection.InsertOne(member);
        }

        /// <summary>
        /// Creates a book in db
        /// </summary>
        /// <param name="book"></param>
        public void CreateBook(Book book)
        {
            var collection = db.GetCollection<Book>(Books_Collection);
            collection.InsertOne(book);
        }

        /// <summary>
        /// Creates a film in db
        /// </summary>
        /// <param name="film"></param>
        public void CreateFilm(Film film)
        {
            var collection = db.GetCollection<Film>(Films_Collection);
            collection.InsertOne(film);
        }

        /// <summary>
        /// Creates a rent in db
        /// </summary>
        /// <param name="rent"></param>
        public void CreateRent(Rent rent)
        {
            var collection = db.GetCollection<Rent>(Rents_Collection);
            collection.InsertOne(rent);
        }

        /// <summary>
        /// Updates amount copy property in db 
        /// </summary>
        /// <param name="type"></param>
        /// <param name="id"></param>
        /// <param name="copies"></param>
        public void UpdateCopies(Type type, ObjectId id, int copies)
        {
            if (type == typeof(Book))
            {
                var collection = db.GetCollection<Book>(Books_Collection);
                var filter = Builders<Book>.Filter.Eq("Id", id);
                var updateCopies = Builders<Book>.Update.Set("Copies", copies);
                collection.UpdateOne(filter, updateCopies);
            }
            else
            {
                var collection = db.GetCollection<Film>(Films_Collection);
                
                    var filter = Builders<Film>.Filter.Eq("Id", id);
                    var updateCopies = Builders<Film>.Update.Set("Copies", copies);
                    collection.UpdateOne(filter, updateCopies);
            }
        }

        /// <summary>
        /// Gets all members
        /// </summary>
        /// <returns></returns>
        public List<Member> GetMembers()
        {
            var collection = db.GetCollection<Member>(Members_Collection);
            return collection.Find(m => true).ToList();
        }

        /// <summary>
        /// Gets all books
        /// </summary>
        /// <returns></returns>
        public List<Book> GetBooks()
        {
            var collection = db.GetCollection<Book>(Books_Collection);
            return collection.Find(b => true).ToList();
        }

        /// <summary>
        /// Gets all films
        /// </summary>
        /// <returns></returns>
        public List<Film> GetFilms()
        {
            var collection = db.GetCollection<Film>(Films_Collection);
            return collection.Find(f => true).ToList();
        }

        /// <summary>
        /// Gets all rents
        /// </summary>
        /// <returns></returns>
        public List<Rent> GetRents()
        {
            var collection = db.GetCollection<Rent>(Rents_Collection);
            return collection.Find(r => true).ToList();
        }

        /// <summary>
        /// Gets a list of all rents of a book from db 
        /// </summary>
        /// <param name="book"></param>
        /// <returns>a list of all rents of a book from db</returns>
        public List<Rent> GetAllSameBookRented(Book book)
        {
            var collection = db.GetCollection<Rent>(Rents_Collection);
            return collection.Find(r => r.RentedBook.Id == book.Id).ToList();
        }

        /// <summary>
        /// Gets a list of all rents of a film from db 
        /// </summary>
        /// <param name="film"></param>
        /// <returns>a list of all rents of a film from db</returns>
        public List<Rent> GetAllSameFilmRented(Film film)
        {
            var collection = db.GetCollection<Rent>(Rents_Collection);
            return collection.Find(r => r.RentedFilm.Id == film.Id).ToList();
        }

        /// <summary>
        /// Gets a list of member's all rents from db 
        /// </summary>
        /// <param name="id"></param>
        /// <returns>a list of member's all rents from db </returns>
        public List<Rent> GetRentsByMemberId(ObjectId id)
        {
            var collection = db.GetCollection<Rent>(Rents_Collection);
            return collection.Find(fr => fr.RentingMember.Id == id).ToList();
        }

        /// <summary>
        /// Delettes a member from db
        /// </summary>
        /// <param name="id"></param>
        public void DeleteMemberById(ObjectId id)
        {
            var collection = db.GetCollection<Member>(Members_Collection);
            collection.DeleteOne(m => m.Id == id);
        }

        /// <summary>
        /// Delettes a book from db
        /// </summary>
        /// <param name="id"></param>
        public void DeleteBookById(ObjectId id)
        {
            var collection = db.GetCollection<Book>(Books_Collection);
            collection.DeleteOne(b => b.Id == id);
        }

        /// <summary>
        /// Delettes a film from db
        /// </summary>
        /// <param name="id"></param>
        public void DeleteFilmById(ObjectId id)
        {
            var collection = db.GetCollection<Film>(Films_Collection);
            collection.DeleteOne(f => f.Id == id);
        }

        /// <summary>
        /// Delettes a rent from db
        /// </summary>
        /// <param name="id"></param>
        public void DeleteRentById(ObjectId id)
        {
            var collection = db.GetCollection<Rent>(Rents_Collection);
            collection.DeleteOne(dr => dr.Id == id);
        }

    }
}
