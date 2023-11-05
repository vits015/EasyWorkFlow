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
using Microsoft.AspNetCore.Authorization;
using EasyWorkFlowAPI.DTOs;
using EasyWorkFlowAPI.Services;

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
        [AllowAnonymous]
        public ActionResult<dynamic> Login(UserDTO userDTO)
        {
            //retorna um usuário caso encontre no banco com os parametros informados
            var password = SecretHasher.Hash(userDTO.Password);            
            var user = _context.Users.Single(u => u.Name.Equals(userDTO.Name)); //busca Usuario pelo nome             
            if (user != null)
            {
                if (!SecretHasher.Verify(userDTO.Password, user.PasswordHash))
                    return NotFound(new { message = "Usuário ou senha inválidos" });
            }
            userDTO.Password = string.Empty;
            var token = TokenService.GenerateToken(userDTO);            
            return new
            {
                user = user!.Name,
                token = token
            };

        }


    }
}