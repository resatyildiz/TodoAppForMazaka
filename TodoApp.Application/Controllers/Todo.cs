using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoApp.DataAccess;
using TodoApp.Entities;

namespace TodoApp.Application.Controllers
{
    
    [ApiController]
    [Route("[controller]")]
    public class Todo : ControllerBase
    {

        private readonly ILogger<Todo> _logger;

        public Todo(ILogger<Todo> logger)
        {
            _logger = logger;
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IEnumerable<Entities.Todo> Get()
        {
            UnitOfWork uow = new UnitOfWork(new DataAccess.AppContext());
            return uow.TodoRepository.GetTopTodoList(5);
        }

        [HttpPost]
        public Todo AddTodo([FromBody] Todo todo)
        {

            UnitOfWork uow = new UnitOfWork(new DataAccess.AppContext());
            uow.TodoRepository.Add(new Entities.Todo { }) ;

            return todo;
        }
    }
}
