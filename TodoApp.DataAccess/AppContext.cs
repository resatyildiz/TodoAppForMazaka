using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
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

        public AppContext() : base("Server=DESKTOP-Q70D9TV;Database=TodoAppDatabase;Trusted_Connection=True;") /* THE VARIABLE OF THIS PROB */
        {

        }

        public DbSet<Todo> Todos { get; set; }
        public DbSet<User> Users { get; set; }

        public DbSet<mRole> Roles { get; set; }


        public UserManager<User> UserManager;
        public RoleManager<IdentityRole> RoleManager;

    }
}
