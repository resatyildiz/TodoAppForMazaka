using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TodoApp.Entities;

namespace TodoApp.DataAccess.Repositories.Abstract
{
    public interface ITodoRepository : IRepository<Todo>
    {
        public IEnumerable<Todo> GetTodoListWithUsers();
        public IEnumerable<Todo> GetTopTodoList(int count);
    }
}
