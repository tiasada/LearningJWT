using Shop.Models;
using Shop.Repositories;

namespace Shop.Services
{
    public class UserService
    {
        public User Create(string name, string password, string role)
        {
            var user = new User(name, password, role);
            UserRepository.Add(user);
            return user;
        }
    }
}