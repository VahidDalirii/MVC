using CentiroHomeAssignment.Models;
using CentiroHomeAssignment.Repositories;
using CentiroHomeAssignment.Services;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using System;
using System.Collections.Generic;

namespace CentiroHomeAssignment.Controllers
{
    public class OrdersController : Controller
    {
        public IOrderRepository OrderRepository { get; set; } = new OrderRepository();

        public IActionResult GetAll()
        {
            List<OrderRow> orders = OrderRepository.GetOrders();

            var path = "App_Data\\";
            orders = OrderRepository.AddOrdersFromFiles(orders, path);

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
            order = OrderRepository.GetOrderById(orderId);
          
            return View(order);

        }



        public IActionResult CreateOrder()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateOrder(OrderRow order)
        {
            if (OrderRepository.OrderIsAlreadyRegistered(order))
            {
                ModelState.AddModelError("Order", "This order was already registered. Can't register same order twice.");
                return View(order);
            }
            OrderRepository.CreateOrder(order);

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
            order.Description = string.IsNullOrEmpty(order.Description) ? "" : order.Description;
            
            if (OrderRepository.OrderIsAlreadyRegistered(order))
            {
                ModelState.AddModelError("Order", "This order was already registered. Can't register same order twice.");
                return View(order);
            }
            OrderRepository.EditOrder(order);

            return RedirectToAction("GetAll");
        }
    }
}
