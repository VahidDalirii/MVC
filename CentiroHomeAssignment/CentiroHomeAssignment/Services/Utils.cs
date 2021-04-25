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
        //Get files from App_Data folder and check for new files, to add new orders to DB 
        public List<OrderRow> AddOrdersFromFiles(List<OrderRow> orders, string path)
        {
            try
            {
                if (!Directory.Exists(path))
                    throw new Exception("Path does not exists");

                var files = FileService.GetFiles(path);

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
                                if (AddedOrderSuccessfullyToDatabase(order))
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
                throw new Exception($" Erro: {ex.Message}");
            }

            return orders;
        }

        //Check file is already registered
        public bool FileIsAlreadyRegistered(string fileName)
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

        //Add new order to DB
        public bool AddedOrderSuccessfullyToDatabase(OrderRow order)
        {
            try
            {
                var orderRepository = new OrderRepository();
                orderRepository.CreateOrder(order);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
            
        }

        //Check to not register same order twice in DB
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
        
        //Trim all order values before store in DB
        public OrderRow GetTrimedOrderValues(OrderRow order)
        {
            var trimedOrder = new OrderRow
            {
                OrderNumber = order.OrderNumber.Trim(),
                OrderLineNumber = order.OrderLineNumber.Trim(),
                ProductNumber = order.ProductNumber.Trim(),
                Quantity = order.Quantity.Trim(),
                Name = order.Name.Trim(),
                Description = string.IsNullOrEmpty(order.Description) ? "" : order.Description.Trim(),
                Price = order.Price.Trim(),
                ProductGroup = order.ProductGroup.Trim(),
                OrderDate = order.OrderDate.Trim(),
                CustomerName = order.CustomerName.Trim(),
                CustomerNumber = order.CustomerNumber.Trim(),
            };

            return trimedOrder;
        }
    }
}
