using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
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

        public IActionResult Show(string id)
        {
            ObjectId todoId = new ObjectId(id);
            Database db = new Database();
            Todo todo = db.GetTodoById(todoId);

            return View(todo);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(string name, string description, string priority)
        {
            Database db = new Database();
            db.SaveTodo(new Todo()
            {
                Name = name,
                Description = description,
                Priority = priority
            });
            return Redirect("/Todos");
        }

        
    }
}