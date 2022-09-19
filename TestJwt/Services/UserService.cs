using Microsoft.EntityFrameworkCore;
using TestJwt.Database;
using TestJwt.Models;

namespace TestJwt.Services
{
    public class UserService
    {
        public static User? getUserForAuth(string Login, string Password)
        {
            using (DbEF db = new DbEF())
            {
                var userDB = db.Users.Include(u =>
                /*(u.Login == Login) && (u.Password ==Password)*/
                u.roles
                ).FirstOrDefault(u => (u.Login == Login && u.Password == Password));

                return userDB;
            }
        }

        public static User? getUserForRefreshToken(string refresh_token)
        {
            using (DbEF db = new DbEF())
            {
                var userDB = db.Users.Include(r =>
                /*(u.Login == Login) && (u.Password ==Password)*/
                r.roles
                ).FirstOrDefault(u => u.Refresh_Token == refresh_token);

                return userDB;
            }
        }
    }
}
