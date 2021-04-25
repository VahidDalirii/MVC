using CentiroHomeAssignment.Models;
using CentiroHomeAssignment.Services;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CentiroHomeAssignment.Repositories
{
    public class OrderRepository: IOrderRepository
    {
        private IUtils _utils;
        public OrderRepository()
        {
            _utils = new Utils();
        }
        public OrderRepository(IUtils utils)
        {
            _utils = utils;
        }

        readonly static OrdersDb _db = new OrdersDb();

        public List<OrderRow> AddOrdersFromFiles(List<OrderRow> orders, string path)
        {
            return _utils.AddOrdersFromFiles(orders, path);
        }

        public bool OrderIsAlreadyRegistered(OrderRow order)
        {
            return _utils.OrderIsAlreadyRegistered(order);
        }

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
