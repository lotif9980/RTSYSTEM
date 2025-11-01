using System.ComponentModel.DataAnnotations.Schema;

namespace RTWEB.Models
{
    public class User
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? PhoneNo { get; set; }
        public string? Email { get; set; }

        public int? RoleId {  get; set; }
        public string? UserName { get; set; }
        public string? Password { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime CreateDate {  get; set; }
        public bool Status { get; set; }
        public int EmployeeId { get; set; }
    }
}
