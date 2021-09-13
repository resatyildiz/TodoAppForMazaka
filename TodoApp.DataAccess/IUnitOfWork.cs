using System;
using System.Collections.Generic;
using System.Text;
using TodoApp.DataAccess.Repositories.Abstract;

namespace TodoApp.DataAccess
{
    public interface IUnitOfWork : IDisposable
    {
        ITodoRepository TodoRepository { get;}

        IUserRepository UserRepository { get; }

        IMRoleRepository MRoleRepository { get; }

        int Complete();
    }
}
