using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoList.Models;

namespace TodoList
{
    public class Helper
    {
        private readonly Database db = new Database();
        internal List<Todo> GetSortedTodos(string priority=null)
        {
            List<Todo> todos = db.GetTodos();

            string[] priorities = { "High", "Medium", "Low" };
            if (priority == "Low")
            {
                Array.Reverse(priorities);
            }
            return todos.OrderBy(p => Array.IndexOf(priorities, p.Priority)).ToList();
        }

        internal List<Todo> GetFilteredTodos(string priority)
        {
            return db.FilterTodos(priority);
        }

        internal Todo GetTodoById(string id)
        {
            ObjectId todoId = new ObjectId(id);
            return db.GetTodoById(todoId);
        }

        internal void SaveTodo(Todo todo)
        {
            db.SaveTodo(todo);
        }

        internal void EditTodo(string id, string title, string description, string priority)
        {
            ObjectId todoId = new ObjectId(id);
            db.EditTodo(todoId, title, description, priority);
        }

        internal void DeleteTodoById(string id)
        {
            ObjectId todoId = new ObjectId(id);
            db.DeleteTodo(todoId);
        }
    }
}
