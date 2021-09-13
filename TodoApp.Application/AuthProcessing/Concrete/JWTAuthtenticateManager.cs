using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TodoApp.DataAccess;
using TodoApp.Entities;

namespace TodoApp.Application.AuthProcessing.Concrete
{
    public class JWTAuthtenticateManager : IJWTAuthtenticationManager
    {
        private readonly string _seckey;
        private UnitOfWork _uow;

        public JWTAuthtenticateManager(string seckey, string dbStr)
        {
            _seckey = seckey;
            _uow = new UnitOfWork(new DataAccess.AppContext(dbStr));
        }
        public string Generate(string Id, string Role = "User")
        {

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.ASCII.GetBytes(_seckey);
            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(new Claim[] {

                    new Claim(ClaimTypes.UserData, Id),
                    new Claim(ClaimTypes.Role, Role)

                }),
                Expires = DateTime.UtcNow.AddYears(1), // 1 YEAR TIMES ADDED FOR TEST DEVELOPMENT 
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }

        public User getAuthUser(string jwt)
        {
                var token = Verify(jwt);
                string userId = token.Payload.First().Value.ToString();

                User user = _uow.UserRepository.GetById(userId);

                return user;
        }

        public mRole getAuthUserRole(string jwt)
        {
            var token = Verify(jwt);
            string userId = token.Payload.First().Value.ToString();

            User user = _uow.UserRepository.GetById(userId);

            return _uow.MRoleRepository.GetById(user.Roles.First().RoleId.ToString());
        }

        public JwtSecurityToken Verify(string jwt) 
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_seckey);

            tokenHandler.ValidateToken(jwt, new TokenValidationParameters
            {
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuerSigningKey = true,
                ValidateIssuer = false,
                ValidateAudience = false
            }, out SecurityToken validatedToken);

            return (JwtSecurityToken)validatedToken;
        }    

    }
}
