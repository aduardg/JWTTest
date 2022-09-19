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
    }
}
