using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TodoApp.DataAccess;

namespace TodoApp.Application.AuthProcessing.Concrete
{
    public class JWTAuthtenticateManager : IJWTAuthtenticationManager
    {
        private readonly string seckey;

        public JWTAuthtenticateManager(string _seckey)
        {
            seckey = _seckey;
        }
        public string Authenticate(string username, string password)
        {
            UnitOfWork uow = new UnitOfWork(new DataAccess.AppContext());
            User user = uow.UserRepository.GetByUserName(username);

            if (user.Id == null)
            {
                return null;
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.ASCII.GetBytes(seckey);
            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(new Claim[] {

                    new Claim(ClaimTypes.Name, username)

                }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
