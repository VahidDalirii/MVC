using CentiroHomeAssignment.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CentiroHomeAssignment.Services
{
    public class CsvOrderMapper
    {
        //Add order details from list to an order object 
        public static OrderRow MapRow(List<string> row)
        {
            var order = new OrderRow
            {
                OrderNumber = string.IsNullOrEmpty(row[0]) ? throw new NullReferenceException("OrderNumber can not be null") : row[0],
                OrderLineNumber = string.IsNullOrEmpty(row[1]) ? throw new NullReferenceException("OrderNumber can not be null") : row[1],
                ProductNumber = string.IsNullOrEmpty(row[2]) ? throw new NullReferenceException("OrderNumber can not be null") : row[2],
                Quantity = string.IsNullOrEmpty(row[3]) ? throw new NullReferenceException("OrderNumber can not be null") : row[3],
                Name = string.IsNullOrEmpty(row[4]) ? throw new NullReferenceException("OrderNumber can not be null") : row[4],
                Description = string.IsNullOrEmpty(row[5]) ? "" : row[5],
                Price = string.IsNullOrEmpty(row[6]) ? throw new NullReferenceException("OrderNumber can not be null") : row[6],
                ProductGroup = string.IsNullOrEmpty(row[7]) ? throw new NullReferenceException("OrderNumber can not be null") : row[7],
                OrderDate = string.IsNullOrEmpty(row[8]) ? throw new NullReferenceException("OrderNumber can not be null") : row[8],
                CustomerName = string.IsNullOrEmpty(row[9]) ? throw new NullReferenceException("OrderNumber can not be null") : row[9],
                CustomerNumber = string.IsNullOrEmpty(row[10]) ? throw new NullReferenceException("OrderNumber can not be null") : row[10],
            };

            return order;
        }
    }
}
