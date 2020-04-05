using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using Repository;
using Repository.Models;

namespace CustomerListWebApp.Controllers
{
    public class OrdersController : Controller
    {
        /// <summary>
        /// Gets all orders and sends the list to index view
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            List<Order> orders = OrderRepository.GetOrders();

            return View(orders);
        }

        /// <summary>
        /// Gets all orders by a customers and send the list to view
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult CustomerOrders(string id)
        {
            ObjectId customerId = new ObjectId(id);
            List<Order> customerOrders = OrderRepository.GetOrdersByCustomerId(customerId);

            return View(customerOrders);
        }

        /// <summary>
        /// Gets an order by order id and sends it to details view
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Details(string id)
        {
            ObjectId orderId = new ObjectId(id);
            Order order = OrderRepository.GetOrderById(orderId);
            Customer customer = CustomerRepository.GetCustomerById(order.CustomerId);
            return View(customer);
        }

        /// <summary>
        /// Gets all customers as a list and sends it to CreateOrder view
        /// </summary>
        /// <returns></returns>
        public ActionResult CreateOrder()
        {
            List<Customer> customers = CustomerRepository.GetCustomers();
            return View(customers);
        }
        
        /// <summary>
        /// This create metod uses if you already chose an customer 
        /// Checks if new order is unique in db, cost is not null, cost is an integer and title is not null and sends the new order to save in db
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="order"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateOrder(string customerId, Order order)
        {
            try
            {
                List<Customer> customers = CustomerRepository.GetCustomers();

                if (order.Title != null)
                {
                    if (order.Cost != null)
                    {
                        if (OrderRepository.IsCostOnlyDigits(order.Cost))
                        {
                            if (OrderRepository.IsOrderUnique(order))
                            {
                                ObjectId id = new ObjectId(customerId);
                                order.CustomerId = id;
                                OrderRepository.CreateOrder(order);

                                return RedirectToAction("CustomerOrders", new { id = customerId });
                            }
                            else
                            {
                                TempData["textmsg"] = "<script>alert('An order with the Same Title and the Same Cost already exists. Please try another order');</script>";
                                return View(customers);
                            }
                        }
                        else
                        {
                            TempData["textmsg"] = "<script>alert('The Cost you entered is not in right format. Please try again');</script>";
                            return View(customers);
                        }  
                    }
                    else
                    {
                        TempData["textmsg"] = "<script>alert('You have to enter an order cost. Please try again');</script>";
                        return View(customers);
                    }
                }
                else
                {
                    TempData["textmsg"] = "<script>alert('You have to enter an order Title. Please try again');</script>";
                    return View(customers);
                }
            }
            catch
            {

                return View();
            }

        }

        /// <summary>
        /// Shows create view
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Create(string id)
        {
            return View();
        }

        /// <summary>
        /// This create metod uses if you don't already chose an customer and shows you a list of customers to choose 
        /// Checks if new order is unique in db, cost is not null, cost is an integer and title is not null and sends the new order to save in db
        /// </summary>
        /// <param name="id"></param>
        /// <param name="order"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(string id, Order order)
        {
            try
            {
                if (order.Title != null) 
                {
                    if (order.Cost != null)
                    {
                        if (OrderRepository.IsCostOnlyDigits(order.Cost))
                        {
                            if (OrderRepository.IsOrderUnique(order))
                            {
                                ObjectId CustomerId = new ObjectId(id);
                                order.CustomerId = CustomerId;
                                OrderRepository.CreateOrder(order);

                                return RedirectToAction("Index");
                            }
                            else
                            {
                                TempData["textmsg"] = "<script>alert('An order with the Same Title and the Same Cost already exists. Please try another order');</script>";
                                return View();
                            }
                        }
                        else
                        {
                            TempData["textmsg"] = "<script>alert('The Cost you entered is not in right format. Please try again');</script>";
                            return View();
                        }  
                    }
                    else
                    {
                        TempData["textmsg"] = "<script>alert('You have to enter an order Cost. Please try again');</script>";
                        return View();
                    }
                }
                else
                {
                    TempData["textmsg"] = "<script>alert('You have to enter an order Title. Please try again');</script>";
                    return View();
                }

            }
            catch
            {
                return View();
            }
        }

        /// <summary>
        /// Shows the order that user wants to edit
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Edit(string id)
        {
            ObjectId orderId = new ObjectId(id);
            Order order = OrderRepository.GetOrderById(orderId);

            return View(order);
        }

        /// <summary>
        /// Sends the updated order to update
        /// </summary>
        /// <param name="id"></param>
        /// <param name="order"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(string id, Order order)
        {
            try
            {
                ObjectId orderId = new ObjectId(id);
                order.Id = orderId;
                OrderRepository.UpdateOrder(order);

                return RedirectToAction("Details", new { id = id });
            }
            catch
            {
                return View();
            }
        }

        /// <summary>
        /// Shows the order that user wants to delete
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Delete(string id)
        {
            ObjectId orderId = new ObjectId(id);
            Order order = OrderRepository.GetOrderById(orderId);

            return View(order);
        }

        /// <summary>
        /// Sends the confirmed delete order to delete from db
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Delete")]
        public ActionResult DeleteConfirmed(string id)
        {
            try
            {
                ObjectId orderId = new ObjectId(id);
                OrderRepository.DeleteOrderById(orderId);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}