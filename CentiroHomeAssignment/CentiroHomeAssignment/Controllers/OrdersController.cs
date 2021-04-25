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

        //Get all orders
        public IActionResult GetAll()
        {
            List<OrderRow> orders = OrderRepository.GetOrders();

            var path = "App_Data\\";
            try
            {
                orders = OrderRepository.AddOrdersFromFiles(orders, path);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Orders", $"{ex.Message}");
                return View(orders);
            }
            

            return View(orders);
        }

        
        public IActionResult GetByOrderNumber()
        {
            return View();
        }

        //Get all orders match given order number
        [HttpPost]
        public IActionResult ShowOrdersByOrderNumber(string orderNumber)
        {
            var orders = OrderRepository.GetOrdersByOrderNumber(orderNumber.Trim());
            return View(orders);
        }

        //Get order by id and show the details
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

        //Create a new order and store in database if there is no other similar order registered in db
        [HttpPost]
        public IActionResult CreateOrder(OrderRow order)
        {
            var id = ObjectId.GenerateNewId();
            order.Id = id;
            order.Description = string.IsNullOrEmpty(order.Description) ? "" : order.Description;
            if (OrderRepository.OrderIsAlreadyRegistered(order))
            {
                ModelState.AddModelError("Order", "This order was already registered. Can't register same order twice.");
                return View(order);
            }
            OrderRepository.CreateOrder(order);

            return RedirectToAction("GetAll");
        }

        //Get order and show details before delete
        public IActionResult DeleteOrder(string id)
        {
            ObjectId orderId = new ObjectId(id);
            var order = OrderRepository.GetOrderById(orderId);

            return View(order);
        }

        //Delete order from DB after confirmation
        [HttpPost, ActionName("DeleteOrder")]
        public IActionResult DeleteOrderConfirmed(string id)
        {
            ObjectId orderId = new ObjectId(id);
            var order = OrderRepository.GetOrderById(orderId);
            OrderRepository.DeleteOrderById(orderId);

            return RedirectToAction("GetAll");
        }

        //Show order details to edit
        public IActionResult EditOrder(string id)
        {
            ObjectId orderId = new ObjectId(id);
            var order = OrderRepository.GetOrderById(orderId);

            return View(order);
        }

        //Edit order in DB if there is no other similar order registered in db
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
