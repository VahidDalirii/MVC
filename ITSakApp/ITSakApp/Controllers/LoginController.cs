using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repository;
using Repository.Models;

namespace ITSakApp.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return RedirectToAction(nameof(Create));
        }

        // GET: Login/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Login/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Login/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(string username, string password)
        {
            try
            {
                List<User> users = UserRepository.GetUsersWithSameUsername(username);
                bool userExists = false;
                foreach (var user in users)
                {
                    string[] splitedPass = user.Password.Split(":");
                    string salt = splitedPass[1];
                    string hashedPass = UserRepository.CreateMD5(password + salt) + ":" + salt;

                    if (hashedPass == user.Password)
                    {
                        userExists = true;
                        break;
                    }
                }

                if (userExists)
                {
                    TempData["textmsg"] = "<script>alert('Username & password is CORRECT.');</script>";
                    return View();
                }
                else
                {
                    TempData["textmsg"] = "<script>alert('Username & password is INCORRECT.');</script>";
                    return View();
                }
            }
            catch
            {
                return View();
            }
        }

        // GET: Login/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Login/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Login/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Login/Delete/5
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