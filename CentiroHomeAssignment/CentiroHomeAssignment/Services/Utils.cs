using CentiroHomeAssignment.Models;
using CentiroHomeAssignment.Repositories;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace CentiroHomeAssignment.Services
{
    public static class Utils
    {
        public static bool OrderIsAlreadyRegistered(OrderRow order)
        {
            var orders = OrderRepository.GetOrders();

            foreach (OrderRow or in orders)
            {
                if (or.OrderNumber.Equals(order.OrderNumber, StringComparison.InvariantCultureIgnoreCase)
                    && or.OrderLineNumber.Equals(order.OrderLineNumber, StringComparison.InvariantCultureIgnoreCase)
                    && or.ProductNumber.Equals(order.ProductNumber, StringComparison.InvariantCultureIgnoreCase)
                    && or.Quantity.Equals(order.Quantity, StringComparison.InvariantCultureIgnoreCase)
                    && or.Name.Equals(order.Name, StringComparison.InvariantCultureIgnoreCase)
                    && or.Description.Equals(order.Description, StringComparison.InvariantCultureIgnoreCase)
                    && or.Price.Equals(order.Price, StringComparison.InvariantCultureIgnoreCase)
                    && or.ProductGroup.Equals(order.ProductGroup, StringComparison.InvariantCultureIgnoreCase)
                    && or.OrderDate.Equals(order.OrderDate, StringComparison.InvariantCultureIgnoreCase)
                    && or.CustomerName.Equals(order.CustomerName, StringComparison.InvariantCultureIgnoreCase)
                    && or.CustomerNumber.Equals(order.CustomerNumber, StringComparison.InvariantCultureIgnoreCase))
                {
                    return true;
                }
            }

            return false;
        }

    }
}
