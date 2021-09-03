using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;


namespace TodoApp.Entities
{
    public class Todo
    {

        [Key]
        public int Id { get; set; }
        [MaxLength(100)]
        public string Title { get; set; }
        [MaxLength(250)]
        public string Content { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
        public bool IsStatus { get; set; }
        public bool IsActive { get; set; }

        /* User */
        public User CreatedFrom { get; set; }
        public User UpdatedTo { get; set; }
    }
}
