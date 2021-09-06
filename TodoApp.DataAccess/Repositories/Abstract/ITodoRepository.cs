using System;
using System.Collections.Generic;
using TodoApp.Entities;

namespace TodoApp.DataAccess.Repositories.Abstract
{
    public interface ITodoRepository : IRepository<Todo>
    {
        public IEnumerable<Todo> GetTodoListWithUsers();
        public IEnumerable<Todo> GetTopTodoList(int count);
        public Todo UpdateTodo(Todo todo);

        public Todo GetByIdInt(int id);

    }
}
