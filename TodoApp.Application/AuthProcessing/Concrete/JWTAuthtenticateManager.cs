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
        public string Generate(string Id, string Role = "User")
        {

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.ASCII.GetBytes(seckey);
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

        public JwtSecurityToken Verify(string jwt) 
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(seckey);

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
