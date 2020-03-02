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
        private const string Customers_Collection = "Customers";
        private const string Orders_Collection = "Orders";

        private IMongoDatabase db;

        public Database(string dbName="Customer_List")
        {
            MongoClient client = new MongoClient();
            db = client.GetDatabase(dbName);
        }

        internal List<Customer> GetCustomers()
        {
            var collection = db.GetCollection<Customer>(Customers_Collection);
            return collection.Find(c => true).ToList();
        }

        internal void CreateCustomer(Customer customer)
        {
            var collection = db.GetCollection<Customer>(Customers_Collection);
            collection.InsertOne(customer);
        }

        internal void UpdateCustomer(Customer customer)
        {
            var collection = db.GetCollection<Customer>(Customers_Collection);
            var update = Builders<Customer>.Update
                .Set("Name", customer.Name)
                .Set("TelNumber", customer.TelNumber)
                .Set("Notes", customer.Notes);
            collection.UpdateOne(c => c.Id == customer.Id, update);
        }

        internal void DeleteCustomerById(ObjectId id)
        {
            var collection = db.GetCollection<Customer>(Customers_Collection);
            collection.DeleteOne(c => c.Id == id);
        }

        internal Customer GetCustomerById(ObjectId id)
        {
            var collection = db.GetCollection<Customer>(Customers_Collection);
            return collection.Find(c => c.Id==id).FirstOrDefault();
        }
    }
}
