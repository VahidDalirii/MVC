using BankingSystemApp.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankingSystemApp
{
    public class Database
    {
        private const string CUSTOMERS_COLLECTION = "Customers";
        private const string ACCOUNTS_COLLECTION = "Accounts";
        private const string TRANSACTIONS_COLLECTION = "Transactions";

        IMongoDatabase db;
        
        public Database(string dbName= "Banking_system")
        {
            MongoClient client = new MongoClient();
            db = client.GetDatabase(dbName);
        }

        public Customer GetCustomerBySsn(string ssn)
        {
            var collection = db.GetCollection<Customer>(CUSTOMERS_COLLECTION);
            Customer customer = collection.Find(c => c.SSN == ssn).FirstOrDefault();
            return customer;
        }

        internal List<Customer> GetCustomers()
        {
            var collection = db.GetCollection<Customer>(CUSTOMERS_COLLECTION);
            return collection.Find(c => true).ToList();
        }

        internal void SaveAccount(Account account)
        {
            var collection = db.GetCollection<Account>(ACCOUNTS_COLLECTION);
            collection.InsertOne(account);
        }

        internal List<Account> GetAccountsByCustomerId(ObjectId id)
        {
            var collection = db.GetCollection<Account>(ACCOUNTS_COLLECTION);
            return collection.Find(c => c.CustomerId==id).ToList();
        }

        internal void SaveCustomer(Customer customer)
        {
            var collection = db.GetCollection<Customer>(CUSTOMERS_COLLECTION);
            collection.InsertOne(customer);
        }

        internal Customer GetCustomewrById(ObjectId id)
        {
            var collection = db.GetCollection<Customer>(CUSTOMERS_COLLECTION);
            return collection.Find(c => c.Id==id).FirstOrDefault();
        }

        internal void DeleteCustomerById(ObjectId id)
        {
            var collection = db.GetCollection<Customer>(CUSTOMERS_COLLECTION);
            collection.DeleteOne(c => c.Id == id);
        }

        internal void EditCustomer(ObjectId id, Customer customer)
        {
            var collection = db.GetCollection<Customer>(CUSTOMERS_COLLECTION);

            var update = Builders<Customer>.Update
                .Set(c => c.Type, customer.Type)
                .Set(c => c.SSN, customer.SSN)
                .Set(c => c.FirstName, customer.FirstName)
                .Set(c => c.LastName, customer.LastName)
                .Set(c => c.Email, customer.Email)
                .Set(c => c.MobileNumber, customer.MobileNumber);

            collection.UpdateOne(c => c.Id == id, update);
        }
    }
}
