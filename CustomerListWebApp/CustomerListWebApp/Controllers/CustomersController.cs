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
    public class CustomersController : Controller
    {
        /// <summary>
        /// Gets a list of customers and returns to index view
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            List<Customer> customers = CustomerRepository.GetCustomers();
            return View(customers);
        }

        /// <summary>
        /// Gets an customer by id and returns to details view
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Details(string id)
        {
            ObjectId customerId = new ObjectId(id);
            Customer customer = CustomerRepository.GetCustomerById(customerId);
            return View(customer);
        }

        /// <summary>
        /// Shows the create view
        /// </summary>
        /// <returns></returns>
        public ActionResult Create()
        {
            return View();
        }

        /// <summary>
        /// Gets all customers values from user and send to save in db(checks to have name and telnumber and not already exists in db)
        /// </summary>
        /// <param name="customer"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Customer customer)
        {
            try
            {
                if (customer.Name!=null)
                {
                    if (customer.TelNumber!=null)
                    {
                        if (CustomerRepository.IsCustomerUnique(customer))
                        {
                            CustomerRepository.CreateCustomer(customer);

                            return RedirectToAction("Index");
                        }
                        else
                        {
                            TempData["textmsg"] = "<script>alert('A customer with same Name and Telnumber already exists');</script>";
                            return View();
                        }
                    }
                    else
                    {
                        TempData["textmsg"] = "<script>alert('You have to enter a Telnumber. Please try again');</script>";
                        return View();
                    }
                }
                else
                {
                    TempData["textmsg"] = "<script>alert('You have to enter a Name. Please try again');</script>";
                    return View();
                }
                
            }
            catch
            {
                return View();
            }
        }

        /// <summary>
        /// Gets a customer by id and returns to edit view
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Edit(string id)
        {
            ObjectId customerId = new ObjectId(id);
            Customer customer = CustomerRepository.GetCustomerById(customerId);
            return View(customer);
        }

        /// <summary>
        /// Gets an updated customer and sends to db to update 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="customer"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(string id, Customer customer)
        {
            try
            {
                ObjectId customerId = new ObjectId(id);
                customer.Id = customerId;
                CustomerRepository.UpdateCustomer(customer);

                return RedirectToAction("Details", new { id = id });
            }
            catch
            {
                return View();
            }
        }

        /// <summary>
        /// Gets a customer by id and send to view to confirmation
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Delete(string id)
        {
            ObjectId customerId = new ObjectId(id);
            Customer customer = CustomerRepository.GetCustomerById(customerId);
            return View(customer);
        }

        /// <summary>
        /// Gets a confirmed delete customer and send to db to delete
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
                ObjectId customerId = new ObjectId(id);
                CustomerRepository.DeleteCustomerById(customerId);

                OrderRepository.DeleteOrdersByCustomerId(customerId);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}