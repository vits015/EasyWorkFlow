using EasyWorkFlowAPI.Context;
using EasyWorkFlowAPI.Models;
using EasyWorkFlowAPI.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EasyWorkFlowAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class JobRoleController : ControllerBase
    {
        private readonly WorkFlowDBContext _context;
        public JobRoleController(WorkFlowDBContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Authorize]
        public ActionResult<dynamic> GetAllJobRoles() => _context.JobRoles;

        [HttpPost]
        [Authorize]
        public ActionResult<dynamic>AddJobRole(JobRoleDTO jobRoleDTO)
        {
            JobRole jobRole = new JobRole()
            {
                Name = jobRoleDTO.Name,
                WeekTime = jobRoleDTO.WeekTime
            };
            _context.JobRoles.Add(jobRole);
            _context.SaveChanges();
            return Ok(new {message = "Cargo adicionado com sucesso!", jobRole });
        }
    }
}
