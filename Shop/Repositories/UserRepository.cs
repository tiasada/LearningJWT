using System.Collections.Generic;
using System.Linq;
using Shop.Models;
using Shop.Services;

namespace Shop.Repositories
{
    public static class UserRepository
    {
        public static List<(User user, string token)> users { get; set; } = new List<(User user, string token)>();
        public static IReadOnlyCollection<(User user, string token)> Users => users;
        public static (User user, string token) Get(string username, string password)
        {
            var findedUser = users.Where(x => x.user.Username.ToLower() == username.ToLower() && x.user.Password == x.user.Password).FirstOrDefault();
            if (findedUser.user.Password == password)
            {
                return findedUser;
            }
            else
            {
                return (new User(), "");
            }
        }
        public static void Add(User _user)
        {
            var userAndToken = (_user , TokenService.GenerateToken(_user));
            users.Add(userAndToken);
        }
        public static (User user, string token) GetUserByName(string name)
        {
            return Users.FirstOrDefault(x => x.user.Username == name);
        }
    }
}