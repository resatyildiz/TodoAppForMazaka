using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;


namespace TodoApp.Entities
{
    [Table("dbo.Todoes")]
    public class Todo
    {

        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(100)]
        public string Title { get; set; }
        [Required]
        [MaxLength(250)]
        public string Content { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
        public bool IsStatus { get; set; }
        public bool IsActive { get; set; }

        /* Job User */
        [ForeignKey("TodoFrom")]
        public string TodoFromId { get; set; }
        [ForeignKey("TodoTo")]
        public string TodoToId { get; set; }
        [ForeignKey("CreatedFrom")]
        public string CreatedFromId { get; set; }
        [ForeignKey("UpdatedFrom")]
        public string? UpdatedFromId { get; set; }

        public User TodoFrom { get; set; }
        public User TodoTo { get; set; }

        /* Proces User */
        public User CreatedFrom { get; set; }

        public User UpdatedFrom { get; set; }
    }
}
