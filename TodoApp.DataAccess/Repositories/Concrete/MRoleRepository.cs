using System;
using System.Collections.Generic;
using System.Text;
using TodoApp.DataAccess.Repositories.Abstract;
using TodoApp.Entities;

namespace TodoApp.DataAccess.Repositories.Concrete
{
    public class MRoleRepository : Repository<mRole>, IMRoleRepository
    {
        public MRoleRepository(AppContext context) : base(context)
        {
        }

    }
}
