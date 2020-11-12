using System;

namespace Shop.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public User(string name, string password, string role)
        {
            Id = new Random().Next(10);
            Username = name;
            Password = password;
            Role = role;
        }
        public User()
        {
            
        }
    }
}