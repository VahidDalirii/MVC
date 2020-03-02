using MongoDB.Bson;
using Repository.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository
{
    public class CustomerRepository
    {
        public static List<Customer> GetCustomers()
        {
            Database db = new Database();
            List<Customer> customers= db.GetCustomers();
            return customers;
        }

        public static Customer GetCustomerById(ObjectId id)
        {
            Database db = new Database();
            Customer customer = db.GetCustomerById(id);
            return customer;
        }

        public static void CreateCustomer(Customer customer)
        {
            Database db = new Database();
            db.CreateCustomer(customer);
        }

        public static void UpdateCustomer(Customer customer)
        {
            Database db = new Database();
            db.UpdateCustomer(customer);
        }

        public static void DeleteCustomerById(ObjectId id)
        {
            Database db = new Database();
            db.DeleteCustomerById(id);
        }
    }
}
