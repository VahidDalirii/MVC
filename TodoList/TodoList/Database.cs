using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoList.Models;

namespace TodoList
{
    public class Database
    {
        private const string TODO_COLLECTION = "Animal";
        private readonly IMongoDatabase db;

        public Database(string dbName="Todo list")
        {
            MongoClient client = new MongoClient();
            db = client.GetDatabase(dbName);
        }

        public void SaveTodo(Todo newTodo)
        {
            var collection = db.GetCollection<Todo>(TODO_COLLECTION);
            collection.InsertOne(newTodo);
        }
  
        public List<Todo> GetTodos()
        {
            var collection = db.GetCollection<Todo>(TODO_COLLECTION);
            return collection.Find(td => true).ToList();
        }

        public void EditTodo(Todo todo)
        {
            var collection = db.GetCollection<Todo>(TODO_COLLECTION);
            var filter = Builders<Todo>.Filter.Eq("Id", todo.Id);

            var updateName = Builders<Todo>.Update.Set("Name", todo.Name);
            collection.UpdateOne(filter, updateName);

            var updateDescription = Builders<Todo>.Update.Set("Description", todo.Description);
            collection.UpdateOne(filter, updateDescription);

            var updatePriority = Builders<Todo>.Update.Set("Priority", todo.Priority);
            collection.UpdateOne(filter, updatePriority);
        }

        public void DeleteTodo(Todo todo)
        {
            var collection = db.GetCollection<Todo>(TODO_COLLECTION);
            collection.DeleteOne(td => td.Id==todo.Id);
        }
    }
}
