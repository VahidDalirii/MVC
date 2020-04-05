using DataLibrary.BusinessLogic;
using MVCApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCApp.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult SignUp()
        {
            ViewBag.Message = "Employee Sign Up";

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SignUp(EmployeeModel model)
        {
            if (ModelState.IsValid)
            {
                EmployeeProcessor.CreateEmployee(model.EmployeeId, model.FirstName, model.LastName, model.EmailAddress);
                return RedirectToAction("ViewEmployees");
            }

            return View();
        }

        public ActionResult ViewEmployees(  )
        {
            ViewBag.Message = "Employees List";

            var data = EmployeeProcessor.LoadEmployees();

            return View(data);
        }
    }
}