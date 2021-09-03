using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading.Tasks;
using TodoApp.Application.AuthProcessing;

namespace TodoApp.Application.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class Login : ControllerBase
    {
        private IJWTAuthtenticationManager _jWTAuthtenticationManager;
        public Login(IJWTAuthtenticationManager jWTAuthtenticationManager)
        {
            _jWTAuthtenticationManager = jWTAuthtenticationManager;
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody] UserCredendial user)
        {
            var token = _jWTAuthtenticationManager.Authenticate(user.Username, user.Password);
            if (token == null)
            {
                return Unauthorized();
            }

            return Ok(token);
        }
    }

    public class UserCredendial{
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
