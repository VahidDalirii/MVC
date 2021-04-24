using CentiroHomeAssignment.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CentiroHomeAssignment.Repositories
{
    class Database
    {
        private const string Orders_Collection = "Orders";

        private IMongoDatabase _db;

        public Database(string database = "Orders")
        {
            MongoClient client = new MongoClient();
            _db = client.GetDatabase(database);
        }

        public List<OrderRow> GetOrders()
        {
            var collection = _db.GetCollection<OrderRow>(Orders_Collection);
            return collection.Find(o => true).ToList();
        }

        public void CreateOrder(OrderRow order)
        {
            var collection = _db.GetCollection<OrderRow>(Orders_Collection);
            collection.InsertOne(order);
        }

        public OrderRow GetOrderById(ObjectId id)
        {
            var collection = _db.GetCollection<OrderRow>(Orders_Collection);
            return collection.Find(o => o.Id == id).FirstOrDefault();
        }

        public void DeleteOrderById(ObjectId id)
        {
            var collection = _db.GetCollection<OrderRow>(Orders_Collection);
            collection.DeleteOne(o => o.Id == id);
        }

        public void EditOrder(OrderRow order)
        {
            var collection = _db.GetCollection<OrderRow>(Orders_Collection);
            var update = Builders<OrderRow>.Update
                .Set("OrderNumber", order.OrderNumber)
                .Set("OrderLineNumber", order.OrderLineNumber)
                .Set("ProductNumber", order.ProductNumber)
                .Set("Quantity", order.Quantity)
                .Set("Name", order.Name)
                .Set("Description", order.Description)
                .Set("Price", order.Price)
                .Set("ProductGroup", order.ProductGroup)
                .Set("OrderDate", order.OrderDate)
                .Set("CustomerName", order.CustomerName)
                .Set("CustomerNumber", order.CustomerNumber);
            collection.UpdateOne(o => o.Id == order.Id, update);
        }

    }
}
