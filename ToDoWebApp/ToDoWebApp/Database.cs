using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoWebApp.Models;

namespace ToDoWebApp
{
    public class Database
    {
        private const string TODO_COLLECTION = "Todo";
        private readonly IMongoDatabase db;

        public Database(string dbName = "Todo-list")
        {
            MongoClient client = new MongoClient();
            db = client.GetDatabase(dbName);
        }

        /// <summary>
        /// Saves a todo in db
        /// </summary>
        /// <param name="newTodo"></param>
        public void SaveTodo(Todo newTodo)
        {
            var collection = db.GetCollection<Todo>(TODO_COLLECTION);
            collection.InsertOne(newTodo);
        }

        /// <summary>
        /// Gets all todos from db 
        /// </summary>
        /// <returns>A list of all todos</returns>
        public List<Todo> GetTodos()
        {
            var collection = db.GetCollection<Todo>(TODO_COLLECTION);
            return collection.Find(td => true).ToList();
        }

        /// <summary>
        /// Edits a todo with new values as parameters
        /// </summary>
        /// <param name="id"></param>
        /// <param name="title"></param>
        /// <param name="description"></param>
        /// <param name="priority"></param>
        public void EditTodo(ObjectId id, string title, string description, string date, string priority)
        {
            var collection = db.GetCollection<Todo>(TODO_COLLECTION);

            var filter = Builders<Todo>.Filter.Eq("Id", id);

            var updateName = Builders<Todo>.Update
                .Set("Title", title)
                .Set("Description", description)
                .Set("Date", date)
                .Set("Priority", priority);

            collection.UpdateOne(filter, updateName);
        }

        internal List<Todo> GetTodosWithDate(DateTime date)
        {
            var collection = db.GetCollection<Todo>(TODO_COLLECTION);
            return collection.Find(td => td.Date == date).ToList();
        }

        /// <summary>
        /// Filters todos after priority value 
        /// </summary>
        /// <param name="priority"></param>
        /// <returns>A list of todos with same priority</returns>
        internal List<Todo> FilterTodos(string priority)
        {
            var collection = db.GetCollection<Todo>(TODO_COLLECTION);
            return collection.Find(td => td.Priority == priority).ToList();
        }

        /// <summary>
        /// Gets a todo which matchs the id 
        /// </summary>
        /// <param name="id"></param>
        /// <returns>An object as a todo</returns>
        internal Todo GetTodoById(ObjectId id)
        {
            var collection = db.GetCollection<Todo>(TODO_COLLECTION);
            return collection.Find(td => td.Id == id).First();
        }

        /// <summary>
        /// Deletes a todo which matchs the id
        /// </summary>
        /// <param name="id"></param>
        public void DeleteTodo(ObjectId id)
        {
            var collection = db.GetCollection<Todo>(TODO_COLLECTION);
            collection.DeleteOne(td => td.Id == id);
        }
    }
}
