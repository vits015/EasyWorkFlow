namespace EasyWorkFlowAPI.DTOs
{
    public class WorkScheduleInterfaceDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string FirstShiftTimeIn { get; set; }
        public string FirstShiftTimeOut { get; set; }
        public string SecondShiftTimeIn { get; set; }
        public string SecondShiftTimeOut { get; set; }

    }
}
