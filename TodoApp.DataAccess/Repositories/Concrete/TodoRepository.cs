using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using TodoApp.DataAccess.Repositories.Abstract;
using TodoApp.Entities;

namespace TodoApp.DataAccess.Repositories.Concrete
{
    public class TodoRepository : Repository<TodoRepository>, ITodoRepository
    {
        public TodoRepository(AppContext context):base(context)
        {

        }

        // I could casting to AppContext because I was 'protected' define this object.
        public AppContext AppContext { get { return _context as AppContext; } }

        public void Add(Todo entity)
        {
            throw new NotImplementedException();
        }

        public void AddRange(IEnumerable<Todo> entities)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Todo> GetTodoListWithUsers()
        {
            return AppContext.Todos.Take(5);
        }

        public IEnumerable<Todo> GetTopTodoList(int count)
        {
            return AppContext.Todos.Take(count);
        }

        public void RemoveRange(IEnumerable<Todo> entities)
        {
            throw new NotImplementedException();
        }

        IEnumerable<Todo> IRepository<Todo>.GetAll()
        {
            throw new NotImplementedException();
        }

        Todo IRepository<Todo>.GetById(string id)
        {
            throw new NotImplementedException();
        }
    }
}
