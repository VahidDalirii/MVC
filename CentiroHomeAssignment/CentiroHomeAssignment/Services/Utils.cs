using CentiroHomeAssignment.Models;
using CentiroHomeAssignment.Repositories;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CentiroHomeAssignment.Services
{
    public static class Utils
    {
        public static List<OrderRow> AddOrdersFromFiles(List<OrderRow> orders, string path)
        {
            try
            {
                if(!Directory.Exists(path))
                    throw new Exception("Path does not exists");

                var files = FileServices.GetFiles(path);

                foreach (var file in files)
                {
                    if (Path.GetExtension(file).Equals(".txt", StringComparison.InvariantCultureIgnoreCase))
                    {
                        var parser = new FileParser<OrderRow>('|', true, false, "txt");
                        var rows = parser.GetRows(file, Encoding.UTF8);
                        foreach (var row in rows)
                        {
                            var splitedRow = parser.GetSplitedRow(row);
                            var order = CsvOrderMapper.MapRow(splitedRow);
                            if (!Utils.OrderIsAlreadyRegistered(order))
                            {
                                OrderRepository.CreateOrder(order);
                                orders.Add(order);
                            }
                        }
                    }
                    if (Path.GetExtension(file).Equals(".xml", StringComparison.InvariantCultureIgnoreCase))
                    {
                        //Future xml files 
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error when getting orders: {ex.Message}");
            }

            return orders;
        }
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
