using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoList.Models;

namespace TodoList
{
    public class Helper
    {
        internal List<Todo> GetSortedTodos(string priority=null)
        {
            Database db = new Database();
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
            Database db = new Database();
            return db.FilterTodos(priority);
        }
    }
}
