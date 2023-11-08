using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EasyWorkFlowAPI.Models
{
    public class JobRole : BaseEntity
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string WeekTime { get; set; }
    }
}
