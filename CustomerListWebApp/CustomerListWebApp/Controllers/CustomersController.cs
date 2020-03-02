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
        // GET: Customers
        public ActionResult Index()
        {
            List<Customer> customers = CustomerRepository.GetCustomers();
            return View(customers);
        }

        // GET: Customers/Details/5
        public ActionResult Details(string id)
        {
            ObjectId customerId = new ObjectId(id);
            Customer customer = CustomerRepository.GetCustomerById(customerId);
            return View(customer);
        }

        // GET: Customers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Customers/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(string name, string telNumber, string notes)
        {
            try
            {
                Customer customer = new Customer()
                {
                    Name = name,
                    TelNumber = telNumber,
                    Notes = notes
                };

                CustomerRepository.CreateCustomer(customer);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Customers/Edit/5
        public ActionResult Edit(string id)
        {
            ObjectId customerId = new ObjectId(id);
            Customer customer = CustomerRepository.GetCustomerById(customerId);
            return View(customer);
        }

        // POST: Customers/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(string id, Customer customer)
        {
            try
            {
                ObjectId customerId = new ObjectId(id);
                customer.Id = customerId;
                CustomerRepository.UpdateCustomer(customer);

                return RedirectToAction("Details", new { id=id});
            }
            catch
            {
                return View();
            }
        }

        // GET: Customers/Delete/5
        public ActionResult Delete(string id)
        {
            ObjectId customerId = new ObjectId(id);
            Customer customer = CustomerRepository.GetCustomerById(customerId);
            return View(customer);
        }

        // POST: Customers/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Delete")]
        public ActionResult DeleteConfirmed(string id)
        {
            try
            {
                ObjectId customerId = new ObjectId(id);
                CustomerRepository.DeleteCustomerById(customerId);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}