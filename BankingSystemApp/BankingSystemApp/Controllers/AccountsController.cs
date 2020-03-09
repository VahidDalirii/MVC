using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BankingSystemApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;

namespace BankingSystemApp.Controllers
{
        public class AccountsController : Controller
    {
        private readonly Database db = new Database();

        // GET: Accounts
        public ActionResult Index(string id)
        {
            ObjectId customerId = new ObjectId(id);
            List<Account> Accounts = db.GetAccountsByCustomerId(customerId);
            return View(Accounts);
        }

        // GET: Accounts/Details/5
        public ActionResult Details(string id)
        {
            ObjectId accountId = new ObjectId(id);
            Account account = db.GetAccountById(accountId);
            return View(account);
        }

        // GET: Accounts/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Accounts/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(string id, Account account)
        {
            try
            {
                ObjectId customerId = new ObjectId(id);
                account.CustomerId = customerId;
                db.SaveAccount(account);

                return Redirect($"/Index/{id}");
            }
            catch
            {
                return View();
            }
        }

        // GET: Accounts/Edit/5
        public ActionResult Edit()
        {
            return View();
        }

        // POST: Accounts/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(string id, Account account)
        {
            try
            {
                ObjectId accountId = new ObjectId(id);
                account.Id = accountId;

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Accounts/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Accounts/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}