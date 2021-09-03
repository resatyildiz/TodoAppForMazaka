using Microsoft.AspNet.Identity.EntityFramework;
using System;

namespace TodoApp
{
    public class User : IdentityUser
    {
        public string FullName { get; set; }
    }
}
