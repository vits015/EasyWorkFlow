namespace EasyWorkFlowAPI.Models
{
    public class JobRole : BaseEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string WeekTime { get; set; }
    }
}
