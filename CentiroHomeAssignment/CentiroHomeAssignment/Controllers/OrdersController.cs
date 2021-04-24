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
        public IOrderRepository OrderRepository { get; set; } = new OrderRepository();
        
        public IActionResult GetAll()
        {
            List<OrderRow> orders = OrderRepository.GetOrders();

            var path = @"D:\Repos\MVC\CentiroHomeAssignment\CentiroHomeAssignment\App_Data\";
            orders = Utils.AddOrdersFromFiles(orders, path);

            return View(orders);
        }

        public IActionResult GetByOrderNumber()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ShowOrdersByOrderNumber(string orderNumber)
        {
            var orders = OrderRepository.GetOrdersByOrderNumber(orderNumber);
            return View(orders);
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
            var orderRepository = new OrderRepository();
            orderRepository.CreateOrder(order);
            //Orders.Add(order);

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
            //Orders.Remove(order);

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
