using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;

namespace TodoApp.Application.AuthProcessing
{
    public interface IJWTAuthtenticationManager
    {
        string Generate(string Id,string Role = "User");
        public JwtSecurityToken Verify(string jwt);
    }

}
