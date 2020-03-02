using MongoDB.Bson;
using Repository.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository
{
    public class OrderRepository
    {
        /// <summary>
        /// Gets all orders and return a list of all orders
        /// </summary>
        /// <returns>a list of all orders</returns>
        public static List<Order> GetOrders()
        {
            Database db = new Database();
            return db.GetOrders();
        }

        /// <summary>
        /// Gets an order by order id and returns it
        /// </summary>
        /// <param name="id"></param>
        /// <returns>An order as an object</returns>
        public static Order GetOrderById(ObjectId id)
        {
            Database db = new Database();
            return db.GetOrderById(id);
        }

        /// <summary>
        /// Gets an order by orders customer id and returns it
        /// </summary>
        /// <param name="id"></param>
        /// <returns>an order as an object</returns>
        public static List<Order> GetOrdersByCustomerId(ObjectId id)
        {
            Database db = new Database();
            return db.GetOrdersByCustomerId(id);
        }

        /// <summary>
        /// Gets an order and sends it to db to save
        /// </summary>
        /// <param name="order"></param>
        public static void CreateOrder(Order order)
        {
            Database db = new Database();
            db.CreateOrder(order);
        }

        /// <summary>
        /// Gets an updated order and send to db to update
        /// </summary>
        /// <param name="order"></param>
        public static void UpdateOrder(Order order)
        {
            Database db = new Database();
            db.UpdateOrder(order);
        }

        /// <summary>
        /// Gets an Id as a parameter and sends id to db to delete the order by this id
        /// </summary>
        /// <param name="id"></param>
        public static void DeleteOrderById(ObjectId id)
        {
            Database db = new Database();
            db.DeleteOrderById(id);
        }

        /// <summary>
        /// Gets an customer id and send to db to delete all orders ordered by this customer
        /// </summary>
        /// <param name="id"></param>
        public static void DeleteOrdersByCustomerId(ObjectId id)
        {
            Database db = new Database();
            db.DeleteOrdersByCustomerId(id);
        }

        /// <summary>
        /// Checks if entered cost is an integer
        /// </summary>
        /// <param name="cost"></param>
        /// <returns>If is integer returns true and returns false if not</returns>
        public static bool IsCostOnlyDigits(string cost)
        {

            foreach (char c in cost)
            {
                if (c < '0' || c > '9')
                    return false;
            }

            return true;
        }

        /// <summary>
        /// Checks if entered order is unique in database
        /// </summary>
        /// <param name="newOrder"></param>
        /// <returns>If is unique returns true and returns false if not</returns>
        public static bool IsOrderUnique(Order newOrder)
        {
            Database db = new Database();
            List<Order> orders = db.GetOrders();

            foreach (var order in orders)
            {
                if (order.Title.ToLower() == newOrder.Title.ToLower() && order.Cost == newOrder.Cost)
                {
                    return false;
                }
            }

            return true;
        }
    }
}
