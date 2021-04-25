using CentiroHomeAssignment.Models;
using CentiroHomeAssignment.Repositories;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace CentiroHomeAssignment.Services
{
    public class Utils: IUtils
    {
        public List<OrderRow> AddOrdersFromFiles(List<OrderRow> orders, string path)
        {
            try
            {
                if (!Directory.Exists(path))
                    throw new Exception("Path does not exists");

                var files = FileServices.GetFiles(path);

                foreach (var file in files)
                {
                    if (Path.GetExtension(file).Equals(".txt", StringComparison.InvariantCultureIgnoreCase))
                    {
                        var fileName = Path.GetFileName(file);
                        if (FileIsAlreadyRegistered(fileName))
                            continue;
                        var parser = new FileParser<OrderRow>('|', true, false, "txt");
                        var rows = parser.GetRows(file, Encoding.UTF8);
                        foreach (var row in rows)
                        {
                            var splitedRow = parser.GetSplitedRow(row);                            
                            var order = new OrderRow();
                            try
                            {
                                order = CsvOrderMapper.MapRow(splitedRow);
                            }
                            catch (NullReferenceException)
                            {
                                continue;
                            }
                            
                            if (!OrderIsAlreadyRegistered(order))
                            {
                                AddOrderToDatabase(order);
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
                Console.WriteLine($"{ex.Message}");
            }

            return orders;
        }

        private bool FileIsAlreadyRegistered(string fileName)
        {
            var file = new FileModel
            {
                Id = ObjectId.GenerateNewId(),
                FileName = fileName
            };

            var fileRepository = new FileRepository();
            var files = fileRepository.GetFiles();
            if (files.Cast<FileModel>().Any(f => f.FileName == fileName))
                return true;

            fileRepository.AddFileToDb(file);
            return false;
        }

        public void AddOrderToDatabase(OrderRow order)
        {
            var orderRepository = new OrderRepository();
            orderRepository.CreateOrder(order);
        }

        public bool OrderIsAlreadyRegistered(OrderRow order)
        {
            var orderRepository = new OrderRepository();
            var orders = orderRepository.GetOrders();

            foreach (OrderRow or in orders)
            {
                if (order.Id == or.Id)
                    continue;

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
