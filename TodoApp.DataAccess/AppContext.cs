using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Extensions.Configuration;
using System;
using System.Data.Entity;
using TodoApp.Entities;

namespace TodoApp.DataAccess
{
    public class AppContext : IdentityDbContext
    {
        /* 
         * PROB
         * I WILLL READ 'CONNECTIONSTRING' TEXT FROM APPSETTINGS.JSON. 
         * I COULDN'T GET TO THE TEXT FROM THE FILE. 
         */
        public AppContext(String dbString) : base(dbString) /* THE VARIABLE OF THIS PROB */
        {
        }

        public DbSet<Todo> Todos { get; set; }
        public DbSet<User> _Users { get; set; }
        public DbSet<mRole> _Roles { get; set; }


        public UserManager<User> UserManager;
        public RoleManager<mRole> RoleManager;

    }
}
