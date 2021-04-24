using CentiroHomeAssignment.Models;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CentiroHomeAssignment.Repositories
{
    public static class OrderRepository
    {
        readonly static Database _db = new Database();

        public static List<OrderRow> GetOrders()
        {
            return _db.GetOrders();
        }

        public static void CreateOrder(OrderRow order)
        {
            _db.CreateOrder(order);
        }

        public static OrderRow GetOrderById(ObjectId id)
        {
            return _db.GetOrderById(id);
        }

        public static List<OrderRow> GetOrdersByOrderNumber(string orderNumber)
        {
            return _db.GetOrdersByOrderNumber(orderNumber);
        }

        public static void DeleteOrderById(ObjectId id)
        {
            _db.DeleteOrderById(id);
        }

        public static void EditOrder(OrderRow order)
        {
            _db.EditOrder(order);
        }
    }
}
