using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Security.Principal;
using TestJwt.Database;
using TestJwt.Models;

namespace TestJwt.Services
{
    public class TokenService
    {
        public static JwtSecurityToken getJwtToken(ClaimsIdentity claims)
        {
            var now = DateTime.Now;
            return new JwtSecurityToken(
                issuer: AuthOptions.ISSUER,
                audience: AuthOptions.AUDIENCE,
                notBefore: now,
                claims: claims.Claims,
                expires: now.Add(TimeSpan.FromSeconds(AuthOptions.LIFETIME)),
                signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256)
                );
        }

        public static string CreateRefreshToken()
        {
            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }

        /// <summary>
        /// Method <c>CreateAndWriteRefreshToken</c> return token and change time of life
        /// </summary>
        public static string CreateAndWriteRefreshToken(string Login, string Password)
        {
            string refreshToken = CreateRefreshToken();
            using (DbEF db = new DbEF())
            {
                User? u = db.Users.FirstOrDefault(u => (u.Login == Login && u.Password == Password));
                u.Refresh_Token = refreshToken;
                u.Created_RefreshToken = DateTime.Now.AddDays(3);
                db.Users.Update(u);
                db.SaveChanges();
            }
            return refreshToken;
        } 

        public static string? ChangeRefreshToken(string refreshToken)
        {
            using (DbEF db = new DbEF())
            {
                User? u = db.Users.FirstOrDefault(u => u.Refresh_Token == refreshToken);
                if (u is null)
                    return null;
                if (u.Created_RefreshToken < DateTime.Now)
                    return null;
                u.Refresh_Token = CreateRefreshToken();
                db.Update(u);
                db.SaveChanges();
                return u.Refresh_Token;
            }            
        }
    }
}
