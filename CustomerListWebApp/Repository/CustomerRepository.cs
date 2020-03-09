using MongoDB.Bson;
using Repository.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository
{
    public class CustomerRepository
    {
        /// <summary>
        /// Gets all customers from Database class and returns a list of customers
        /// </summary>
        /// <returns>a list of customers</returns>
        public static List<Customer> GetCustomers()
        {
            Database db = new Database();
            return db.GetCustomers();
        }

        /// <summary>
        /// Gets a customers from Database class and returns it 
        /// </summary>
        /// <param name="id"></param>
        /// <returns>A customer</returns>
        public static Customer GetCustomerById(ObjectId id)
        {
            Database db = new Database();
            return db.GetCustomerById(id);
        }

        /// <summary>
        /// Gets all values of a customer object and sends to database class to save in db
        /// </summary>
        /// <param name="customer"></param>
        public static void CreateCustomer(Customer customer)
        {
            Database db = new Database();
            db.CreateCustomer(customer);
        }

        /// <summary>
        /// Gets all updated values of a customer object and sends to database class to save in db
        /// </summary>
        /// <param name="customer"></param>
        public static void UpdateCustomer(Customer customer)
        {
            Database db = new Database();
            db.UpdateCustomer(customer);
        }

        /// <summary>
        /// Gets a customer id and sends to database class to delete from db
        /// </summary>
        /// <param name="id"></param>
        public static void DeleteCustomerById(ObjectId id)
        {
            Database db = new Database();
            db.DeleteCustomerById(id);
        }

        /// <summary>
        /// Gets a new customer and checks if a customer with same values already exists in db
        /// </summary>
        /// <param name="newCustomer"></param>
        /// <returns>If is unique customer returns true anf false if is not unique</returns>
        public static bool IsCustomerUnique(Customer newCustomer)
        {
            Database db = new Database();
            List<Customer> customers = db.GetCustomers();

            foreach (var customer in customers)
            {
                if (customer.Name.ToLower()==newCustomer.Name.ToLower() && customer.TelNumber==newCustomer.TelNumber)
                {
                    return false;
                }
            }
            return true;
        }
    }
}
