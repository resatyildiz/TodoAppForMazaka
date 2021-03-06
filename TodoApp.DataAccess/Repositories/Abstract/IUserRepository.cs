using System;
using System.Collections.Generic;
using System.Text;

namespace TodoApp.DataAccess.Repositories.Abstract
{
    public interface IUserRepository : IRepository<User>
    {
        User GetByUserName(string username);
    }
}
