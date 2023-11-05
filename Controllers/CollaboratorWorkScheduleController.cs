using EasyWorkFlowAPI.Context;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using EasyWorkFlowAPI.Models;
using EasyWorkFlowAPI.DTOs;
using Microsoft.AspNetCore.SignalR;

namespace EasyWorkFlowAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CollaboratorWorkScheduleController : ControllerBase
    {
        
        private readonly WorkFlowDBContext _context;
        public CollaboratorWorkScheduleController(WorkFlowDBContext context)
        {
            _context = context;
        }
        [HttpGet]
        [Authorize]
        public ActionResult<dynamic> GetCollaboratorWorkSchedule() => _context.CollaboratorWorkSchedules;

        [HttpPost]
        [Authorize]
        public ActionResult<dynamic> AddCollaboratorWorkSchedule(CollaboratorWorkScheduleDTO collaboratorWorkScheduleDTO)
        {
            User user = _context.Users.Single(u => u.Id == collaboratorWorkScheduleDTO.UserID);
            WorkScheduleInterface workScheduleInterface = 
                _context.WorkScheduleInterfaces.SingleOrDefault(wsi => wsi.Id == collaboratorWorkScheduleDTO.WorkScheduleInterfaceID)!;

            if (workScheduleInterface == null || user == null) 
            {
                return NotFound(new {message = "Usuário ou Escala informada inválidos"});
            }
            CollaboratorWorkSchedule workSchedule = new CollaboratorWorkSchedule()
            {
                Date = collaboratorWorkScheduleDTO.Date,
                User = user,
                WorkScheduleInterface = workScheduleInterface,
                JobRoleId = user.JobRole.Id.ToString(),
                JobRoleName = user.JobRole.Name,
                JobWeekHours = user.JobRole.WeekTime,
                FirstShiftTimeIn = workScheduleInterface.FirstShiftTimeIn,
                FirstShiftTimeOut = workScheduleInterface.FirstShiftTimeOut,
                SecondShiftTimeIn = workScheduleInterface.SecondShiftTimeIn,
                SecondShiftTimeOut = workScheduleInterface.SecondShiftTimeOut
            };

            _context.CollaboratorWorkSchedules.Add(workSchedule);
            _context.SaveChanges();
            return Ok(new 
                { message = $"Escala de trabalho criada com sucesso.", 
                  description = $"No dia {workSchedule.Date.ToOADate} {workSchedule.Date.DayOfWeek}," +
                    $"{workSchedule.User.Name} vai trabalhar das {workSchedule.FirstShiftTimeIn} às {workSchedule.SecondShiftTimeOut}" });

        }
    }
}
