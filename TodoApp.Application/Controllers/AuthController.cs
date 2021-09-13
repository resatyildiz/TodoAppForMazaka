using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using TodoApp.Application.AuthProcessing;
using TodoApp.Application.Dtos;
using TodoApp.DataAccess;
using TodoApp.Entities;

namespace TodoApp.Application.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private IJWTAuthtenticationManager _jWTAuthtenticationManager;
        private UnitOfWork uow;
        public AuthController(IJWTAuthtenticationManager jWTAuthtenticationManager)
        {
            _jWTAuthtenticationManager = jWTAuthtenticationManager;
            uow = new UnitOfWork(new DataAccess.AppContext());
        }

        [HttpGet("user")] // Annotation
        public IActionResult AuthUser()
        {
            var jwt = Request.Cookies["jwt"];
            User user = _jWTAuthtenticationManager.getAuthUser(jwt);
            if (user != null) return Ok(user);
            else return Unauthorized();
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public IActionResult Login([FromBody] UserCredential user)
        {

            User _user = uow.UserRepository.GetByUserName(user.Username);

            if (_user == null) return BadRequest(new { message = "Invalid Credentials" });

            if (!BCrypt.Net.BCrypt.Verify(user.Password, _user.PasswordHash))
            {
                return BadRequest(new { message = "Invalid Credentials" });
            }

            var roleId = _user.Roles.First().RoleId.ToString();
            mRole mRole = uow.MRoleRepository.GetById(roleId);


            var jwt = _jWTAuthtenticationManager.Generate(_user.Id,mRole.Name);


            Response
                .Cookies
                .Append("jwt", jwt, new CookieOptions
                {
                    HttpOnly = true
                });


            return Ok(new { jwt = jwt });
        }


    }

}
