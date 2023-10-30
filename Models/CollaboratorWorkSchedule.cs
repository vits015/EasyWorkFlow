namespace EasyWorkFlowAPI.Models
{
    public class CollaboratorWorkSchedule : BaseEntity
    {
        public int Id { get; set; }
        public User User { get; set; }
        public WorkScheduleInterface WorkScheduleInterface { get; set; }
        public string JobRoleId { get; set; }
        public string JobRoleName { get; set;}
        public string JobWeekHours { get; set; }
        public DateTime Date { get; set; }
        public string FirstShiftTimeIn { get; set; }
        public string FirstShiftTimeOut { get; set; }
        public string SecondShiftTimeIn { get; set; }
        public string SecondShiftTimeOut { get; set; }

    }
}
