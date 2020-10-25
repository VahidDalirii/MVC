using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoWebApp.Models;

namespace ToDoWebApp
{
    public class Helper
    {
        private readonly Database db = new Database();
        internal List<Todo> GetTodosByPriority(string userName, string priority = null)
        {
            List<Todo> todos = db.GetTodosForThisUser(userName);

            string[] priorities = { "High", "Medium", "Low" };
            if (priority == "Low")
            {
                Array.Reverse(priorities);
            }
            return todos.OrderBy(p => Array.IndexOf(priorities, p.Priority)).ToList();
        }

        internal List<Todo> GetFilteredTodos(string userName, string priority)
        {
            return db.FilterTodos(userName,priority);
        }

        internal Todo GetTodoById(string id)
        {
            ObjectId todoId = new ObjectId(id);
            return db.GetTodoById(todoId);
        }

        internal List<Todo> GetTodosForThisUser(string name)
        {
            return db.GetTodosForThisUser(name);
        }

        internal void SaveTodo(Todo todo)
        {
            db.SaveTodo(todo);
        }

        internal void EditTodo(string id, string title, string description, string date, string priority)
        {
            ObjectId todoId = new ObjectId(id);
            db.EditTodo(todoId, title, description, date, priority);
        }

        internal void DeleteTodoById(string id)
        {
            ObjectId todoId = new ObjectId(id);
            db.DeleteTodo(todoId);
        }

        internal List<Todo> GetTodosWithDate(string userName, DateTime date)
        {
            return db.GetTodosWithDate(userName , date);
        }
    }
}
