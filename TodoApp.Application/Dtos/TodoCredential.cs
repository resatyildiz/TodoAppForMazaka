using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TodoApp.Application.Dtos
{
    public class TodoCredential
    {
        public int id { get; set; }
        public string title { get; set; }
        public string content { get; set; }
        public string createdFrom { get; set; }
        public string createdAt { get; set; }
        public string TodoFrom { get; set; }
        public string TodoTo { get; set; }
        public string updatedFrom { get; set; }
        public string updatedAt { get; set; }
        public string isStatus { get; set; }
    }
}
