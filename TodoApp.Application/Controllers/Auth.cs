using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading.Tasks;
using TodoApp.Application.AuthProcessing;
using TodoApp.Application.Dtos;
using TodoApp.DataAccess;
using Microsoft.AspNet.Identity.EntityFramework;
using TodoApp.Entities;

namespace TodoApp.Application.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class Auth : ControllerBase
    {
        private IJWTAuthtenticationManager _jWTAuthtenticationManager;
        private UnitOfWork uow;
        public Auth(IJWTAuthtenticationManager jWTAuthtenticationManager)
        {
            _jWTAuthtenticationManager = jWTAuthtenticationManager;
            uow = new UnitOfWork(new DataAccess.AppContext());
        }

        [HttpGet("user")] // Annotation
        public IActionResult authUser()
        {
            try
            {
                var jwt = Request.Cookies["jwt"];
                var token = _jWTAuthtenticationManager.Verify(jwt);
                string userId = token.Payload.First().Value.ToString();

                User user = uow.UserRepository.GetById(userId);


                return Ok(user);
            }
            catch(Exception e)
            {
                return Unauthorized(new { message = e.Message.ToString()});
            }
        }



        [AllowAnonymous]
        [HttpPost("login")]
        public IActionResult login([FromBody] UserCredential user)
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
