using CentiroHomeAssignment.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CentiroHomeAssignment.Services
{
    public interface IUtils
    {
        List<OrderRow> AddOrdersFromFiles(List<OrderRow> orders, string path);
        bool OrderIsAlreadyRegistered(OrderRow order);
        void AddOrderToDatabase(OrderRow order);
    }
}
