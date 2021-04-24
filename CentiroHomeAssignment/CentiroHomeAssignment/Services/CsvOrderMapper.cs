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
                OrderNumber = row[0] ?? throw new NullReferenceException("OrderNumber can not be null"),
                OrderLineNumber = row[1] ?? throw new NullReferenceException("OrderLineNumber can not be null"),
                ProductNumber = row[2] ?? throw new NullReferenceException("ProductNumber can not be null"),
                Quantity = row[3] ?? throw new NullReferenceException("Quantity can not be null"),
                Name = row[4] ?? throw new NullReferenceException("Name can not be null"),
                Description = row[5],
                Price = row[6] ?? throw new NullReferenceException("Price can not be null"),
                ProductGroup = row[7] ?? throw new NullReferenceException("ProductGroup can not be null"),
                OrderDate = row[8] ?? throw new NullReferenceException("OrderDate can not be null"),
                CustomerName = row[9] ?? throw new NullReferenceException("CustomerName number can not be null"),
                CustomerNumber = row[10] ?? throw new NullReferenceException("CustomerNumber can not be null"),
            };

            return order;
        }
    }
}
