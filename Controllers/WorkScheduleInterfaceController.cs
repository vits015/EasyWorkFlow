using EasyWorkFlowAPI.Context;
using EasyWorkFlowAPI.Models;
using EasyWorkFlowAPI.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EasyWorkFlowAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WorkScheduleInterfaceController : ControllerBase
    {    
        private readonly WorkFlowDBContext _context;
        public WorkScheduleInterfaceController(WorkFlowDBContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Authorize]
        public ActionResult<dynamic> GetWorkSchedules() => Ok(_context.WorkScheduleInterfaces);

        [HttpPost]
        [Authorize]
        public ActionResult<dynamic> PostWorkSchedule(WorkScheduleInterfaceDTO workScheduleDTO)
        {
            if (workScheduleDTO == null)
            {
                return BadRequest(new { message = "Um ou mais parâmetros inválidos" });
            }

            WorkScheduleInterface workSchedule = new WorkScheduleInterface()
            {
                Name = workScheduleDTO.Name,
                Description = workScheduleDTO.Description,
                FirstShiftTimeIn = workScheduleDTO.FirstShiftTimeIn.ToString(),
                FirstShiftTimeOut = workScheduleDTO.FirstShiftTimeOut.ToString(),
                SecondShiftTimeIn = workScheduleDTO.SecondShiftTimeIn.ToString(),
                SecondShiftTimeOut = workScheduleDTO.SecondShiftTimeOut.ToString()
            };

            _context.Add(workSchedule);
            _context.SaveChanges();
            return Ok(new {message = "Escala adicionada com sucesso!",
                           workSchedule});
        }
        [HttpDelete]
        [Authorize]
        public ActionResult<dynamic> DeleteWorkSchedule(int id)
        {
            WorkScheduleInterface workSchedule = _context.WorkScheduleInterfaces.SingleOrDefault(x => x.Id == id)!;
            if (workSchedule == null)
                return NotFound(new { message = "Escala não encontrada" });

            _context.Remove(workSchedule);
            _context.SaveChanges();
            return Ok(new { message = "Escala deletada com sucesso!" });
        }

    
    }
}
