using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
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
            try
            {
                // Your code...
                // Could also be before try if you know the exception occurs in SaveChanges

                return _AppContext.SaveChanges();
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
                return 0;
            }
        }

        public void Dispose()
        {
            _AppContext.Dispose();
        }
    }
}
