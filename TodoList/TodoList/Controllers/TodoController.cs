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

        /// <summary>
        /// Gets all todos from db and sorts them after high priority 
        /// </summary>
        /// <returns>A sorted list of todos</returns>
        public IActionResult Index()
        {
            Database db = new Database();
            List<Todo> todos = db.GetTodos();

            string[] priorities = { "High", "Medium", "Low" };
            List<Todo> sortedTodos = todos.OrderBy(p => Array.IndexOf(priorities, p.Priority)).ToList();

            return View(sortedTodos);
        }


        /// <summary>
        /// Lets user to sort or filter todos after priority
        /// </summary>
        /// <param name="submit"></param>
        /// <param name="priority"></param>
        /// <returns>A list of sorted or filtered todos</returns>
        [HttpPost]
        public IActionResult Index(string submit,  string priority)
        {
            Database db = new Database();
            List<Todo> sortedOrFilteredTodos = new List<Todo>();

            if (submit.Equals("Sort"))
            {
                List<Todo> allTodos = db.GetTodos();

                string[] priorities = { "High", "Medium", "Low" };

                if (priority=="Low")
                {
                    Array.Reverse(priorities);
                }

                sortedOrFilteredTodos = allTodos.OrderBy(p => Array.IndexOf(priorities, p.Priority)).ToList();
            }

            else
            {
                sortedOrFilteredTodos = db.FilterTodos(priority);
            }

            return View(sortedOrFilteredTodos);

        }

        /// <summary>
        /// Gets a todo by todo's id from db
        /// </summary>
        /// <param name="id"></param>
        /// <returns>returns an object as todo</returns>
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


        /// <summary>
        /// Lets user to create a todo
        /// </summary>
        /// <param name="name"></param>
        /// <param name="description"></param>
        /// <param name="priority"></param>
        /// <returns>Redirects to todos list</returns>
        [HttpPost]
        public IActionResult Create(Todo todo)
        {
            Database db = new Database();
            db.SaveTodo(todo);
            return Redirect("/Todo");
        }

        /// <summary>
        /// Gets a todo by todo's id from db
        /// </summary>
        /// <param name="id"></param>
        /// <returns>returns an object as todo</returns>
        public IActionResult Edit(string id)
        {
            ObjectId todoId = new ObjectId(id);
            Database db = new Database();
            Todo todo = db.GetTodoById(todoId);

            return View(todo);
        }

        /// <summary>
        /// Lets user edits a todo with new values and saves the new values in db
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="description"></param>
        /// <param name="priority"></param>
        /// <returns>Returns the edited todo with new values</returns>
        [HttpPost]
        public IActionResult Edit(string id,string name, string description, string priority)
        {
            ObjectId todoId = new ObjectId(id);
            Database db = new Database();
            db.EditTodo(todoId, name, description, priority);

            return Redirect($"/Todo/Show/{id}");
        }

        /// <summary>
        /// Finds a todo in db and deletes it
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Redirects to list of todos</returns>
        public IActionResult Delete(string id)
        {
            ObjectId todoId = new ObjectId(id);
            Database db = new Database();
            db.DeleteTodo(todoId);

            return Redirect("/Todo");
        }
        
    }
}