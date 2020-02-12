using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
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
            //IEnumerable<Todo> sortedTodos = todos.OrderByDescending(x =>Regex.Match(x.Priority, @"\W+").Value);
            List<Todo> sortedTodos = todos.OrderByDescending(td => td.Priority == "High").ToList();
            return View(sortedTodos);
        }

        [HttpPost]
        public IActionResult Index(string submit,  string priority)
        {
            Database db = new Database();
            List<Todo> sortedOrFilteredTodos = new List<Todo>();
            if (submit.Equals("Sort"))
            {
                List<Todo> allTodos = db.GetTodos();
                sortedOrFilteredTodos = allTodos.OrderByDescending(td => td.Priority == priority).ToList();

                
            }
            else
            {
                sortedOrFilteredTodos = db.FilterTodos(priority);
            }

            return View(sortedOrFilteredTodos);

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
            return Redirect("/Todo");
        }

        
        public IActionResult Edit(string id)
        {
            ObjectId todoId = new ObjectId(id);
            Database db = new Database();
            Todo todo = db.GetTodoById(todoId);

            return View(todo);
        }

        [HttpPost]
        public IActionResult Edit(string id,string name, string description, string priority)
        {
            ObjectId todoId = new ObjectId(id);
            Database db = new Database();
            db.EditTodo(todoId, name, description, priority);

            return Redirect($"/Todo/Show/{id}");
        }

        public IActionResult Delete(string id)
        {
            ObjectId todoId = new ObjectId(id);
            Database db = new Database();
            db.DeleteTodo(todoId);

            return Redirect("/Todo");
        }
        
    }
}