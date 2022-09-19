using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using TestJwt.Models; // класс Person
using TestJwt.Services;

namespace TestJwt.Controllers
{
    [ApiController]
    [Route("oauth")]
    public class AccountController : Controller
    {
        // тестовые данные вместо использования базы данных
        /*private List<Person> people = new List<Person>
        {
            new Person {Login="admin@gmail.com", Password="12345", Role = "admin" },
            new Person { Login="qwerty@gmail.com", Password="55555", Role = "user" }
        };*/

        [HttpPost]
        [Route("token")]
        public IActionResult Token(string username, string password)
        {
            var identity = GetIdentity(username, password);
            if (identity == null)
            {
                return BadRequest(new { errorText = "Invalid username or password." });
            }

            var jwt = TokenService.getJwtToken(identity);
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            var refresh_token = TokenService.CreateAndWriteRefreshToken(username, password);

            var response = new
            {
                access_token = encodedJwt,
                username = identity.Name,
                refresh_token = refresh_token
            };

            return Json(response);
        }

        private ClaimsIdentity GetIdentity(string username, string password)
        {
            User? user = UserService.getUserForAuth(username, password);
            if (user != null)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimsIdentity.DefaultNameClaimType, user.Login),
                    /*new Claim(ClaimsIdentity.DefaultRoleClaimType, user.roles)*/
                };
                foreach(var m in user.roles)
                {
                    claims.Add(new Claim(ClaimsIdentity.DefaultRoleClaimType, m.Name));
                }
                ClaimsIdentity claimsIdentity =
                new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType,
                    ClaimsIdentity.DefaultRoleClaimType);
                return claimsIdentity;
            }

            // если пользователя не найдено
            return null;
        }

        [HttpPost]
        [Route("refreshToken")]
        public IActionResult refreshToken()
        {

        }
    }
}