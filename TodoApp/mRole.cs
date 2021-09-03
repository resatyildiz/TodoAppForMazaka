using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TodoApp.Entities
{
    public class mRole : IdentityRole
    {
        [MaxLength(250)]
        public string Description { get; set; } // Role description
    }
}
