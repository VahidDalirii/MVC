using CentiroHomeAssignment.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CentiroHomeAssignment.Services
{
    public static class CsvOrderMapper
    {
        public static OrderRow MapRow(List<string> row)
        {
            var order = new OrderRow
            {
                OrderNumber = row[1],
                OrderLineNumber = row[2],
                ProductNumber = row[3],
                Quantity = row[4],
                Name = row[5],
                Description = row[6],
                Price = row[7],
                ProductGroup = row[8],
                OrderDate = row[9],
                CustomerName = row[10],
                CustomerNumber = row[11],
            };

            return order;
        }
    }
}
