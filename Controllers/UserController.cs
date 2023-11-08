using EasyWorkFlowAPI.Context;
using Microsoft.AspNetCore.Mvc;
using EasyWorkFlowAPI.Models;
using EasyWorkFlowAPI.DTOs;
using EasyWorkFlowAPI.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace EasyWorkFlowAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly WorkFlowDBContext _context;
        public UserController(WorkFlowDBContext context)
        {
            _context = context;
        }

        [HttpGet]

        public ActionResult<dynamic> GetUsers()
        {
            var data = _context.Users.Include(x => x.JobRole);
            List<User> userList = new List<User>(data);
            foreach (User user in userList)
            {
                user.PasswordHash = string.Empty;
            }
            return Ok(userList);
        }

        [HttpGet("GetUserByName")]

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
        [HttpPut("AddJobRuleUser")]

        public ActionResult<dynamic>AddJobRuleUser(int userId, int jobRoleId)
        {
            User user = _context.Users.SingleOrDefault(u => u.Id == userId);
            JobRole jobRole = _context.JobRoles.SingleOrDefault(jr => jr.Id == jobRoleId);

            if (user == null || jobRole == null)
                return NotFound(new { message = "Usuário ou cargo não encontrado" });            

            user.JobRole = jobRole;
            _context.Update(user);
            _context.SaveChanges();

            return Ok(new { message = $"Cargo {jobRole.Name} foi adicionado para {user.Name}", user });
        }
        
    }
}
