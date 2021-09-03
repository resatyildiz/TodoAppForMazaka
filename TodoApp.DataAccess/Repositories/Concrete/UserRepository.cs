using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoApp.DataAccess.Repositories.Abstract;
using TodoApp.Entities;

namespace TodoApp.DataAccess.Repositories.Concrete
{
    public class UserRepository : Repository<User>, IUserRepository
    {

        public UserRepository(AppContext context) : base(context)
        {

        }

        public AppContext AppContext { get { return _context as AppContext; } }

        public void Add(Todo entity)
        {
            throw new NotImplementedException();
        }

        public void AddRange(IEnumerable<Todo> entities)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<User> GetAllUser()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Todo> GetTodoListWithUsers()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Todo> GetTopTodoList(int count)
        {
            throw new NotImplementedException();
        }

        public void RemoveRange(IEnumerable<Todo> entities)
        {
            throw new NotImplementedException();
        }

        public User GetByUserName(string username) 
        {
            /**/
            return (User) AppContext.Users.FirstOrDefault(u => u.UserName == username);
        }

    }
}
