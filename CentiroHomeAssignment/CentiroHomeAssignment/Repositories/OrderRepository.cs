using CentiroHomeAssignment.Models;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CentiroHomeAssignment.Repositories
{
    public class OrderRepository: IOrderRepository
    {
        readonly static Database _db = new Database();

        public List<OrderRow> GetOrders()
        {
            return _db.GetOrders();
        }

        public void CreateOrder(OrderRow order)
        {
            _db.CreateOrder(order);
        }

        public OrderRow GetOrderById(ObjectId id)
        {
            return _db.GetOrderById(id);
        }

        public List<OrderRow> GetOrdersByOrderNumber(string orderNumber)
        {
            return _db.GetOrdersByOrderNumber(orderNumber);
        }

        public void DeleteOrderById(ObjectId id)
        {
            _db.DeleteOrderById(id);
        }

        public void EditOrder(OrderRow order)
        {
            _db.EditOrder(order);
        }
    }
}
