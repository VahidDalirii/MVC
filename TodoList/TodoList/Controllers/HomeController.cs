using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MongoDB.Bson;
using TodoList.Models;

namespace TodoList.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Gets all todos from db and sorts them after high priority 
        /// </summary>
        /// <returns>A sorted list of todos</returns>
        public IActionResult Index()
        {
            Helper helper = new Helper();
            var todos = helper.GetSortedTodos();

            return View(todos);
        }


        /// <summary>
        /// Lets user to sort or filter todos after priority
        /// </summary>
        /// <param name="submit"></param>
        /// <param name="priority"></param>
        /// <returns>A list of sorted or filtered todos</returns>
        [HttpPost]
        public IActionResult Index(string submit, string priority, DateTime date)
        {
            Helper helper = new Helper();

            if (submit.Equals("Sort"))
            {
                return View(helper.GetSortedTodos(priority));
            }
            else if (submit.Equals("Filter") && !string.IsNullOrEmpty(priority))
            {
                return View(helper.GetFilteredTodos(priority));
            }
            else if (submit.Equals("Show"))
            {
                return View(helper.GetTodosWithDate(date));
            }
            return Redirect("/Home");
        }

        /// <summary>
        /// Gets a todo by todo's id from db
        /// </summary>
        /// <param name="id"></param>
        /// <returns>returns an object as todo</returns>
        //public IActionResult Show(string id)
        //{
        //    Helper helper = new Helper();
        //    return View(helper.GetTodoById(id));
        //}

        public IActionResult Create()
        {
            return View();
        }


        /// <summary>
        /// Lets user to create a todo
        /// </summary>
        /// <param name="title"></param>
        /// <param name="description"></param>
        /// <param name="priority"></param>
        /// <returns>Redirects to todos list</returns>
        [HttpPost]
        public IActionResult Create(Todo todo)
        {
            Helper helper = new Helper();
            if(todo.Date.Date < DateTime.Today)
            {
                TempData["textmsg"] = "<script>alert('You can not choose a day befor today');</script>";
                return View();
            }
            helper.SaveTodo(todo);
            
            return Redirect("/Home");
        }

        /// <summary>
        /// Gets a todo by todo's id from db
        /// </summary>
        /// <param name="id"></param>
        /// <returns>returns an object as todo</returns>
        public IActionResult Edit(string id)
        {
            Helper helper = new Helper();
            return View(helper.GetTodoById(id));
        }

        /// <summary>
        /// Lets user edits a todo with new values and saves the new values in db
        /// </summary>
        /// <param name="id"></param>
        /// <param name="title"></param>
        /// <param name="description"></param>
        /// <param name="priority"></param>
        /// <returns>Returns the edited todo with new values</returns>
        [HttpPost]
        public IActionResult Edit(string id, string title, string description, string date, string priority)
        {
            Helper helper = new Helper();
            if (DateTime.Parse(date).Date < DateTime.Today)
            {
                TempData["textmsg"] = "<script>alert('You can not choose a day befor today');</script>";
                return View(helper.GetTodoById(id));
            }

            helper.EditTodo(id, title, description, date, priority);

            return Redirect($"/Home");
        }

        public IActionResult Delete(string id)
        {
            Helper helper = new Helper();

            return View(helper.GetTodoById(id));
        }

        /// <summary>
        /// Finds todo by id in db and deletes it
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Redirects to list of todos</returns>
        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(string id)
        {
            Helper helper = new Helper();
            helper.DeleteTodoById(id);

            return Redirect("/Home");
        }

        public IActionResult About()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
