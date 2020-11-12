namespace Shop.Models
{
    public class CreateUserRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string RolePassword { get; set; }
    }
}