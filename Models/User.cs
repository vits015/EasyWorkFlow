using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace EasyWorkFlowAPI.Models
{
    public class User : BaseEntity
    {
        [Key]
        public int Id { get; set; }
        public bool IsMaster { get; set; } = false;
        public int? JobRoleId { get; set; }
        public JobRole JobRole { get; set; }
        public string Name { get; set; }
        public string Cpf { get; set; }
        public DateTime BirthDate { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
    }
}
