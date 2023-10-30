using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EasyWorkFlowAPI.Context;
using EasyWorkFlowAPI.Models;
using EasyWorkFlowAPI.Utils;
using Microsoft.SqlServer.Server;
using EasyWorkFlowAPI.Utils;
using Microsoft.DiaSymReader;

namespace EasyWorkFlowAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly WorkFlowDBContext _context;

        
        public AuthController(WorkFlowDBContext context)
        {
            _context = context;
        }

        [HttpPost("Login")]
        public IActionResult Login(string username, string password)
        {
            //retorna um usuário caso encontre no banco com os parametros informados
            var user = _context.Users.Single(u => u.Name.Equals(username)); //busca Usuario pelo nome
                                                                            //
            if (user != null)
            {
                if (SecretHasher.Verify(password, user.PasswordHash))
                {
                    return Ok("Login efetuado com Sucesso!"); //Verifica se a senha está correta
                }
                return BadRequest("Usuário ou Senha inválidos");
            }
            else
            {
                return BadRequest("Usuário ou Senha inválidos");
            }
        }


    }
}