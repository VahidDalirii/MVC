using CentiroHomeAssignment.Models;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CentiroHomeAssignment.Repositories
{
    public interface IOrderRepository
    {
        List<OrderRow> GetOrders();
        void CreateOrder(OrderRow order);
        OrderRow GetOrderById(ObjectId id);
        List<OrderRow> GetOrdersByOrderNumber(string orderNumber);
        void DeleteOrderById(ObjectId id);
        void EditOrder(OrderRow order);
    }
}
