using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using CentiroHomeAssignment.Models;
using CentiroHomeAssignment.Repositories;
using CentiroHomeAssignment.Services;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;

namespace CentiroHomeAssignment.Controllers
{
    public class OrdersController : Controller
    {
        List<OrderRow> Orders = OrderRepository.GetOrders();
        public IActionResult GetAll()
        {
            try
            {
                var path = "C:\\Users\\Saeid\\Downloads\\CentiroHomeAssignment\\CentiroHomeAssignment\\App_Data\\";
                var csvFiles = FileServices.GetCsvFiles(path);

                foreach (var file in csvFiles)
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
                                Orders.Add(order);
                            }                            
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error when getting orders: {ex.Message}");
            }

            return View(Orders);
        }

        public IActionResult GetByOrderNumber(string id)
        {
            ObjectId orderId = new ObjectId(id);
            var order = new OrderRow();
            try
            {
                order = OrderRepository.GetOrderById(orderId);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error when getting order details: {ex.Message}");
            }

            return View(order);

        }

        public IActionResult GetOrderById(string id)
        {
            ObjectId orderId = new ObjectId(id);
            var order = new OrderRow();
            try
            {
                order = OrderRepository.GetOrderById(orderId);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error when getting order details: {ex.Message}");
            }

            return View(order);

        }



        public IActionResult CreateOrder()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateOrder(OrderRow order)
        {
            OrderRepository.CreateOrder(order);
            Orders.Add(order);

            return RedirectToAction("GetAll");
        }

        public IActionResult DeleteOrder(string id)
        {
            ObjectId orderId = new ObjectId(id);
            var order = OrderRepository.GetOrderById(orderId);

            return View(order);
        }

        [HttpPost, ActionName("DeleteOrder")]
        public IActionResult DeleteOrderConfirmed(string id)
        {
            ObjectId orderId = new ObjectId(id);
            var order = OrderRepository.GetOrderById(orderId);
            OrderRepository.DeleteOrderById(orderId);
            Orders.Remove(order);

            return RedirectToAction("GetAll");
        }

        public IActionResult EditOrder(string id)
        {
            ObjectId orderId = new ObjectId(id);
            var order = OrderRepository.GetOrderById(orderId);

            return View(order);
        }

        [HttpPost, ActionName("EditOrder")]
        public IActionResult EditOrder(OrderRow order, string id)
        {
            ObjectId orderId = new ObjectId(id);
            order.Id = orderId;
            OrderRepository.EditOrder(order);

            return RedirectToAction("GetAll");
        }
    }
}
