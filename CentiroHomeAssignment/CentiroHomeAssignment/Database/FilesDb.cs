using CentiroHomeAssignment.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CentiroHomeAssignment.Database
{
    public class FilesDb
    {
        private const string Files_Collection = "RegisteredOrderFiles";

        private IMongoDatabase _db;

        public FilesDb(string database = "RegisteredOrderFiles")
        {
            MongoClient client = new MongoClient();
            _db = client.GetDatabase(database);
        }

        public void AddFileToDb(FileModel file)
        {
            var collection = _db.GetCollection<FileModel>(Files_Collection);
            collection.InsertOne(file);
        }

        public List<FileModel> GetFiles()
        {
            var collection = _db.GetCollection<FileModel>(Files_Collection);
            return collection.Find(f => true).ToList();
        }
    }
}
