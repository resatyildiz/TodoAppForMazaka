using System;
using System.Collections.Generic;
using System.Text;
using TodoApp.DataAccess.Repositories.Abstract;
using TodoApp.DataAccess.Repositories.Concrete;

namespace TodoApp.DataAccess
{
    public class UnitOfWork : IUnitOfWork
    {
        private AppContext _AppContext;
        public UnitOfWork(AppContext context)
        {
            _AppContext = context;
            TodoRepository = new TodoRepository(_AppContext);
            UserRepository = new UserRepository(_AppContext);
            MRoleRepository = new MRoleRepository(_AppContext);
        }

        public ITodoRepository TodoRepository { get; private set; }

        public IUserRepository UserRepository { get; private set; }

        public IMRoleRepository MRoleRepository { get; private set; }

        public int Complete()
        {
            return _AppContext.SaveChanges();
        }

        public void Dispose()
        {
            _AppContext.Dispose();
        }
    }
}
