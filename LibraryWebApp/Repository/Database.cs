using MongoDB.Bson;
using MongoDB.Driver;
using Repository.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository
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
        /// Gets a rent by id from db
        /// </summary>
        /// <param name="id"></param>
        /// <returns>A rent by rent id</returns>
        internal Rent GetRentById(ObjectId id)
        {
            var collection = db.GetCollection<Rent>(Rents_Collection);
            return collection.Find(r => r.Id == id).FirstOrDefault();
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
        /// Gets a book by id from db
        /// </summary>
        /// <param name="id"></param>
        /// <returns>A book by book id</returns>
        internal Book GetBookById(ObjectId id)
        {
            var collection = db.GetCollection<Book>(Books_Collection);
            return collection.Find(b => b.Id == id).FirstOrDefault();
        }

        /// <summary>
        /// Gets a film by id from db
        /// </summary>
        /// <param name="id"></param>
        /// <returns>A film by film id</returns>
        internal Film GetFilmById(ObjectId id)
        {
            var collection = db.GetCollection<Film>(Films_Collection);
            return collection.Find(f => f.Id == id).FirstOrDefault();
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
        /// Gets a member by id and returns it
        /// </summary>
        /// <param name="id"></param>
        /// <returns>A member</returns>
        internal Member GetMemberById(ObjectId id)
        {
            var collection = db.GetCollection<Member>(Members_Collection);
            return collection.Find(m => m.Id==id).FirstOrDefault();
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

        /// <summary>
        /// Gets an updated member and updates member in db
        /// </summary>
        /// <param name="updatedMember"></param>
        public void UpdateMember(Member updatedMember)
        {
            var collection = db.GetCollection<Member>(Members_Collection);
            //collection.ReplaceOne(m => m.Id == updatedMember.Id, updatedMember); 
            var update = Builders<Member>.Update
                .Set("Name", updatedMember.Name)
                .Set("Address", updatedMember.Address)
                .Set("Password", updatedMember.Password)
                .Set("TelNumber", updatedMember.TelNumber);
            collection.UpdateOne(m=> m.Id==updatedMember.Id, update);
        }

        /// <summary>
        /// Gets an update book and updates book in db
        /// </summary>
        /// <param name="book"></param>
        internal void UpdateBook(Book book)
        {
            var collection = db.GetCollection<Book>(Books_Collection);
            var update = Builders<Book>.Update
                .Set("Title", book.Title)
                .Set("Author", book.Author)
                .Set("Genre", book.Genre)
                .Set("PublishedYear", book.PublishedYear)
                .Set("Copies", book.Copies);
            collection.UpdateOne(b => b.Id == book.Id, update);
        }

        /// <summary>
        /// Gets an update film and updates film in db
        /// </summary>
        /// <param name="film"></param>
        internal void UpdateFilm(Film film)
        {
            var collection = db.GetCollection<Film>(Films_Collection);
            var update = Builders<Film>.Update
                .Set("Title", film.Title)
                .Set("Director", film.Director)
                .Set("Genre", film.Genre)
                .Set("ReleasedYear", film.ReleasedYear)
                .Set("Copies", film.Copies);
            collection.UpdateOne(f=> f.Id==film.Id, update);
        }

        /// <summary>
        /// Gets member and new password and updates member's password in db
        /// </summary>
        /// <param name="member"></param>
        /// <param name="newPassword"></param>
        public void ChangePassword(Member member, string newPassword)
        {
            var collection = db.GetCollection<Member>(Members_Collection);
            var filter = Builders<Member>.Filter.Eq("Id", member.Id);
            var updateCopies = Builders<Member>.Update.Set("Password", newPassword);
            collection.UpdateOne(m=> m.Id==member.Id, updateCopies);
        }

    }
}
