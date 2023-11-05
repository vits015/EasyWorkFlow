using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using EasyWorkFlowAPI.Models;
using EasyWorkFlowAPI.DTOs;
using Microsoft.IdentityModel.Tokens;
using NuGet.Configuration;

namespace EasyWorkFlowAPI.Services
{
    public static class TokenService
    {
        public static string GenerateToken(UserDTO user)
        {            
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(Settings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Name.ToString())                    
                }),
                Expires = DateTime.UtcNow.AddHours(3),
                SigningCredentials = 
                    new SigningCredentials (
                        new SymmetricSecurityKey(key),
                        SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
