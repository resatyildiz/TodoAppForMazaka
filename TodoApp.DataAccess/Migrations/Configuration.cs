namespace TodoApp.DataAccess.Migrations
{
    using Microsoft.AspNet.Identity;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using TodoApp.Entities;

    internal sealed class Configuration : DbMigrationsConfiguration<TodoApp.DataAccess.AppContext>
    {

        private static mRole[] roles; // Roles defination in construct method

        private static User[] users; // Users defination in construct method

        private static string[][] UserRoleRelation; // User Roles defination in construct method

        private static TodoApp.DataAccess.AppContext appContext;

        public Configuration()
        {
            AutomaticMigrationsEnabled = true;

            /* Definite Roles */
            roles = new mRole[]
            {
                new mRole()
                {
                    Name = "Admin",
                    Description = "Todo ekleyebilir ve eklediğiniz todoların durumunu görüntüleyebilirsiniz."
                },
                new mRole()
                {
                    Name = "User",
                    Description = "Adınıza eklenen todoları görüntüleyebilir, tamamlanları bildirebilirsiniz."
                }
            };

            /* Definite Users */
            users = new User[]
            {
               new User()
               {
                   UserName = "mazakaadmin",
                   Email = "admin@mazaka.com",
                   FullName = "Mazaka Admin"
               },
               new User()
               {
                   UserName = "mazakauser",
                   Email = "user@mazaka.com",
                   FullName = "Mazaka User"
               }
            };

            /* Definete Users Role */
            UserRoleRelation = new string[][]
            {
                new string[] { "mazakaadmin" , "Admin" },
                new string[] { "mazakauser" , "User" },
            };


        }

        protected override void Seed(TodoApp.DataAccess.AppContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.

            foreach (mRole role in roles)
            {
                context.Roles.Add(role);
            }

            foreach (User user in users)
            {
                context.Users.Add(user);
            }

        }
        
        /* Çalışmıyor, sebebi RoleManager ve UserManager kullanımını kavrayamadım. Sonuç : kullanıcı ve rol verileri eklendikten sonra manuel olarak db üzerinden ilişkilendirme işlemini yaptım 
         
         */
        public static void SeedRoles()
        {
            foreach (mRole _role in Configuration.roles)
            {
                if (appContext.RoleManager.RoleExistsAsync(_role.Name).Result) 
                { 
                    continue;
                }

                IdentityResult roleResult = appContext.RoleManager.
                CreateAsync(_role).Result;
            }
        }

        public static void SeedUsers()
        {

            foreach (User _user in users)
            {
                if (appContext.UserManager.FindByNameAsync(_user.UserName).Result != null)
                {
                    continue;
                }

                IdentityResult result = appContext.UserManager.CreateAsync
                (_user, "123456").Result;

                if (result.Succeeded)
                {
                    foreach (string[] relation in UserRoleRelation)
                    {
                        if (_user.UserName == relation[0])
                        {
                            appContext.UserManager.AddToRoleAsync(_user.Id, relation[1]);
                        }
                    }
                }
            }
        }
    }
}
