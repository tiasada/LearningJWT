using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Shop;
using Shop.Models;
using Shop.Repositories;
using Shop.Services;

namespace WebAPI.Controllers.Users
{
    [ApiController]
    [Route("[controller]")]
    public class AccountController : ControllerBase
    {
        [HttpPost]
        [Route("login")]
        public async Task<ActionResult<dynamic>> awaitAuthenticate([FromBody]User model)
        {
            // Recupera o usuário
            var userAndToken = UserRepository.Get(model.Username, model.Password);

            // Verifica se o usuário existe
            if (userAndToken.user.Username == "")
            {
                return NotFound(new { message = "Usuário ou senha inválidos" });
            }  
            if (userAndToken.user.Password == "")
            {
                return NotFound(new { message = "Usuário ou senha inválidos" });
            }
            if (userAndToken.user.Role == "")
            {
                return NotFound(new { message = "Usuário ou senha inválidos" });
            }
            if (userAndToken.token == "")
            {
                return NotFound(new { message = "Usuário ou senha inválidos" });
            }         
            // Retorna os dados
            return new
            {
                user = userAndToken.user.Username,
                token = userAndToken.token
            };
        }
        [HttpPost]
        [Route("singin")]
        public IActionResult Create(CreateUserRequest request)
        {
            var userService = new UserService();
            var user = new User();
            if (request.RolePassword == Settings.EmployeePass)
            {
                user = userService.Create(request.Username, request.Password,"employee");    
            }
            else if (request.RolePassword == Settings.ManagerPass)
            {
                user = userService.Create(request.Username, request.Password,"manager");    
            }
            else
            {
                return NotFound(new { message = "Usuário ou senha inválidos" });
            }
            var userAndToken = UserRepository.GetUserByName(request.Username);
            // Retorna os dados
            return Ok("User Created!");
        }
        [HttpGet]
        [Route("anonymous")]
        [AllowAnonymous]
        public string Anonymous() => "Anônimo";

        [HttpGet]
        [Route("authenticated")]
        [Authorize]
        public string Authenticated() => String.Format("Autenticado - {0}", User.Identity.Name);

        [HttpGet]
        [Route("employee")]
        [Authorize(Roles = "employee,manager")]
        public string Employee() => "Funcionário";

        [HttpGet]
        [Route("manager")]
        [Authorize(Roles = "manager")]
        public string Manager() => "Gerente";
    }
}