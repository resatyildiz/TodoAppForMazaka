using System;
using System.Collections.Generic;
using System.Text;

namespace TodoApp.DataAccess.Repositories.Abstract
{
    public interface IUserRepository : IRepository<User>
    {
        IEnumerable<User> GetAllUser();
        User GetByUserName(string username);
    }
}
