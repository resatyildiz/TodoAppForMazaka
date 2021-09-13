using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using TodoApp.Application.AuthProcessing;
using TodoApp.Application.Dtos;
using TodoApp.DataAccess;
using TodoApp.Entities;

namespace TodoApp.Application.Controllers
{

    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class TodoController : ControllerBase
    {
        UnitOfWork uow;
        private IJWTAuthtenticationManager _jWTAuthtenticationManager;
        private readonly ILogger<TodoController> _logger;

        public TodoController(ILogger<TodoController> logger, IJWTAuthtenticationManager jWTAuthtenticationManager)
        {
            _logger = logger;
            uow = new UnitOfWork(new DataAccess.AppContext());
            _jWTAuthtenticationManager = jWTAuthtenticationManager;
        }

        [Authorize(Roles = "Admin,User")]
        [HttpGet]
        public IEnumerable<Entities.Todo> Get()
        {
            var jwt = Request.Cookies["jwt"];
            mRole userRole = _jWTAuthtenticationManager.getAuthUserRole(jwt);
            User user = _jWTAuthtenticationManager.getAuthUser(jwt);
            if (userRole != null && userRole.Name == "User")
                return uow.TodoRepository.GetByUserId(user.Id.ToString());
            else
                return uow.TodoRepository.GetAll();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public Todo AddTodo([FromBody] TodoCredential todo)
        {
            Todo _todo = new Todo();
            _todo.Title = todo.title;
            _todo.Content = todo.content;
            _todo.CreatedAt = DateTime.UtcNow;
            _todo.CreatedFromId = todo.createdFrom;
            _todo.TodoFromId = todo.TodoFrom;
            _todo.TodoToId = todo.TodoTo;
            _todo.IsActive = true; // Veri aktif olarak oluşturuluyor.
            _todo.IsStatus = false;

            uow.TodoRepository.Add(_todo);

            uow.Complete();
            return _todo;
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("updatecontent")]
        public IActionResult UpdateTodoContent([FromBody] TodoCredential todo)
        {
            Todo _todo = uow.TodoRepository.GetByIdInt(todo.id);

            if (todo == null) return NotFound(); 

            _todo.Title = todo.title;
            _todo.Content = todo.content;
            _todo.UpdatedAt = DateTime.UtcNow;
            _todo.UpdatedFromId = todo.updatedFrom;
            _todo.TodoFromId = todo.TodoFrom;
            _todo.TodoToId = todo.TodoTo;
            _todo.IsStatus = todo.isStatus;

            uow.Complete();
            return Ok(_todo);
        }

        [Authorize(Roles = "Admin,User")]
        [HttpPut("updatestatus")]
        public IActionResult UpdateTodoStatus([FromBody] TodoCredential todo)
        {
            Todo _todo = uow.TodoRepository.GetByIdInt(todo.id);

            if (todo == null) return NotFound();

            _todo.UpdatedAt = DateTime.UtcNow;
            _todo.UpdatedFromId = todo.updatedFrom;
            _todo.IsStatus = todo.isStatus;

            uow.Complete();
            return Ok(_todo);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete]
        public IActionResult DeleteTodo([FromBody] TodoCredential todo)
        {

            Todo _todo = uow.TodoRepository.GetByIdInt(todo.id);

            if (todo == null) return NotFound();


            _todo.DeletedAt = DateTime.UtcNow;
            _todo.IsActive = false;

            uow.Complete();
            return Ok(_todo);

        }
    }
}
