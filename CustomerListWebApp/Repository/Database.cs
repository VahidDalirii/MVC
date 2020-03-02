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


        //------------------------------------Customer metods----------------------------------------------

        /// <summary>
        /// Gets all customers from db and returns a list of customers
        /// </summary>
        /// <returns>a list of customers </returns>
        internal List<Customer> GetCustomers()
        {
            var collection = db.GetCollection<Customer>(Customers_Collection);
            return collection.Find(c => true).ToList();
        }

        
        /// <summary>
        /// Gets customer values and cretaes a new customer in db
        /// </summary>
        /// <param name="customer"></param>
        internal void CreateCustomer(Customer customer)
        {
            var collection = db.GetCollection<Customer>(Customers_Collection);
            collection.InsertOne(customer);
        }

        
        /// <summary>
        /// Gets and updated customer and updates it in db
        /// </summary>
        /// <param name="customer"></param>
        internal void UpdateCustomer(Customer customer)
        {
            var collection = db.GetCollection<Customer>(Customers_Collection);
            var update = Builders<Customer>.Update
                .Set("Name", customer.Name)
                .Set("TelNumber", customer.TelNumber)
                .Set("Notes", customer.Notes);
            collection.UpdateOne(c => c.Id == customer.Id, update);
        }

        
        /// <summary>
        /// Gets an customer id and deletes the customer from db
        /// </summary>
        /// <param name="id"></param>
        internal void DeleteCustomerById(ObjectId id)
        {
            var collection = db.GetCollection<Customer>(Customers_Collection);
            collection.DeleteOne(c => c.Id == id);
        }

        /// <summary>
        /// Gets a customer by id from db and returns it
        /// </summary>
        /// <param name="id"></param>
        /// <returns>A customer</returns>
        internal Customer GetCustomerById(ObjectId id)
        {
            var collection = db.GetCollection<Customer>(Customers_Collection);
            return collection.Find(c => c.Id==id).FirstOrDefault();
        }

        //----------------------------------- Order metods-----------------------------------------------


        /// <summary>
        /// Gets a list of all orders from db and returs it
        /// </summary>
        /// <returns>A list of all orders</returns>
        internal List<Order> GetOrders()
        {
            var collection = db.GetCollection<Order>(Orders_Collection);
            return collection.Find(o => true).ToList();
        }

        /// <summary>
        /// Gets an order by id from db and returns it
        /// </summary>
        /// <param name="id"></param>
        /// <returns>an order</returns>
        internal Order GetOrderById(ObjectId id)
        {
            var collection = db.GetCollection<Order>(Orders_Collection);
            return collection.Find(o => o.Id==id).FirstOrDefault();
        }

        /// <summary>
        /// Gets an order object and saves that in db
        /// </summary>
        /// <param name="order"></param>
        internal void CreateOrder(Order order)
        {
            var collection = db.GetCollection<Order>(Orders_Collection);
            collection.InsertOne(order);
        }

        /// <summary>
        /// Gets an updated object and updates that in db
        /// </summary>
        /// <param name="order"></param>
        internal void UpdateOrder(Order order)
        {
            var collection = db.GetCollection<Order>(Orders_Collection);
            var update = Builders<Order>.Update
                .Set("Title", order.Title)
                .Set("Cost", order.Cost);
            collection.UpdateOne(o => o.Id == order.Id, update);
        }

        /// <summary>
        /// Deletes an order by id from db
        /// </summary>
        /// <param name="id"></param>
        internal void DeleteOrderById(ObjectId id)
        {
            var collection = db.GetCollection<Order>(Orders_Collection);
            collection.DeleteOne(o=> o.Id==id);
        }

        /// <summary>
        /// Gets an order by order's customer id and returns it
        /// </summary>
        /// <param name="id"></param>
        /// <returns>an order by order's customer id</returns>
        internal List<Order> GetOrdersByCustomerId(ObjectId id)
        {
            var collection = db.GetCollection<Order>(Orders_Collection);
            return collection.Find(o => o.CustomerId==id).ToList();
        }

        /// <summary>
        /// Gets a customer id and Deletes all orders that orderd by this customer 
        /// </summary>
        /// <param name="id"></param>
        internal void DeleteOrdersByCustomerId(ObjectId id)
        {
            var collection = db.GetCollection<Order>(Orders_Collection);
            collection.DeleteMany(o => o.CustomerId == id);
        }
    }
}
