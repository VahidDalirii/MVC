﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BankingSystemApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;

namespace BankingSystemApp.Controllers
{
    public class CustomersController : Controller
    {
        Database db = new Database();

        // GET: Customers
        public ActionResult Index(string ssn, string password)
        {
            //Customer customer = db.GetCustomerBySsn(ssn);
            //if (customer.Password == password/* && Request.Form["customer"].ToString() == "Admin"*/)
            //{
            //    List<Customer> customers = db.GetCustomers();
            //    return View(customers);
            //}

            //return Redirect("/Home/Index");

            List<Customer> customers = db.GetCustomers();
            return View(customers);
        }

        // GET: Customers/Details/5
        public ActionResult Details(string id)
        {
            ObjectId customerId = new ObjectId(id);
            Customer customer = db.GetCustomewrById(customerId);
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
        public ActionResult Create(Customer customer)
        {
            try
            {
                db.SaveCustomer(customer);
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
            Customer customer = db.GetCustomewrById(customerId);
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
                db.EditCustomer(customerId, customer);
                return RedirectToAction("Index");
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
            Customer customer = db.GetCustomewrById(customerId);
            return View(customer);
        }

        // POST: Customers/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(string id, Customer customer)
        {
            try
            {
                ObjectId customerId = new ObjectId(id);
                db.DeleteCustomerById(customerId);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}