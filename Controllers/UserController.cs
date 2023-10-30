using EasyWorkFlowAPI.Context;
using Microsoft.AspNetCore.Mvc;
using EasyWorkFlowAPI.Models;
using EasyWorkFlowAPI.DTOs;
using EasyWorkFlowAPI.Utils;

namespace EasyWorkFlowAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly WorkFlowDBContext _context;
        public UserController(WorkFlowDBContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetUsers() => Ok(_context.Users);

        [HttpPost]
        public IActionResult CreateUser(UserDTO formData)
        {
            string passwordHash = SecretHasher.Hash(formData.Password);

            User user = new User() 
            { 
                Name = formData.Name,                 
                Cpf = formData.Cpf, 
                BirthDate = formData.BirthDate,
                Email = formData.Email,
                PasswordHash = passwordHash
            };

            _context.Add(user);
            _context.SaveChanges();
            return Ok("Usuario criado com sucesso!");
        }
        
    }
}
