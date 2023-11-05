using EasyWorkFlowAPI.Context;
using Microsoft.AspNetCore.Mvc;
using EasyWorkFlowAPI.Models;
using EasyWorkFlowAPI.DTOs;
using EasyWorkFlowAPI.Utils;
using Microsoft.AspNetCore.Authorization;

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
        [Authorize]
        public ActionResult<dynamic> GetUsers()
        {
            List<User> userList = new List<User>(_context.Users);
            foreach (User user in userList)
            {
                user.PasswordHash = string.Empty;
            }
            return Ok(userList);
        }

        [HttpGet("GetUserByName")]
        [Authorize]
        public ActionResult<dynamic> GetUserByName(string name)
        {
            var user = _context.Users.SingleOrDefault(x => x.Name.Equals(name));
                
                           
            if (user == null)
            {
                return NotFound(new { message = "Usuário não encontrado." });
            }
            return Ok(user);
        }

        [HttpPut("UpdateUser")]
        [Authorize]
        public ActionResult<dynamic> UpdateUser(int id, UserDTO userDTO)
        {
            User user = _context.Users.Single(x => x.Id == id);

            if (user == null)
            {
                return NotFound("Não existe usuário com esse ID");
            }
            user.Name = userDTO.Name;
            user.Cpf = userDTO.Cpf;
            user.BirthDate = userDTO.BirthDate;
            user.Email = userDTO.Email;

            _context.Update(user);
            _context.SaveChanges();
            return Ok(new
            {
                message = "Usuário atualizado com sucesso!",
                to = userDTO
            }
            );
        }
        [HttpDelete]
        [Authorize]
        public ActionResult<dynamic> DeleteUser(int id)
        {
            User user = _context.Users.SingleOrDefault(u => u.Id == id);
            if(user == null)
            {
                return NotFound(new { message = "Usuário não encontrado" });
            }
            _context.Remove(user);
            _context.SaveChanges();
            return Ok(new { message = $"O usuário {user.Name} foi deletado com sucesso!" });
        }


        [HttpPost]
        [Authorize]
        public ActionResult<dynamic> CreateUser(UserDTO formData)
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
