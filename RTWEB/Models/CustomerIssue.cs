using RTWEB.Enum;
using System.ComponentModel.DataAnnotations.Schema;
namespace RTWEB.Models
{
    public class CustomerIssue
    {
        public int Id { get; set; }
        public int? DomainId { get; set; }
        public int ? CustomerId { get; set; }
        public string? Problem { get; set; }
        //public int? Status { get; set; }
        public CustomerIssueStatus Status {  get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime CreateDate {  get; set; }
    }
}
