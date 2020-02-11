using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TodoList.Models;

namespace TodoList.Controllers
{
    public class TodoController : Controller
    {
        public IActionResult Index()
        {
            Database db = new Database();
            List<Todo> todos = db.GetTodos();
            return View(todos);
        }

        public IActionResult Create()
        {
            return View();
        }
    }
}