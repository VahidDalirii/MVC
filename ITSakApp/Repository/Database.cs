using MongoDB.Driver;
using Repository.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository
{
    class Database
    {
        MongoClient Client = new MongoClient();

        public IMongoCollection<User> UserCollection { get; private set; }

        public Database()
        {
            var database = Client.GetDatabase("it-sak-app");

            UserCollection = database.GetCollection<User>("users");
        }
    }
}
