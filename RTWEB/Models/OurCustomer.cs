

using RTWEB.Enum;
using System.ComponentModel.DataAnnotations.Schema;

namespace RTWEB.Models
{
    public class OurCustomer
    {
        public int Id {  get; set; }
        public string? Code { get; set; }
        public string? CustomerName { get; set; }
        public string? Address { get; set; }
        public string? ContactNo { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime CreateDate {  get; set; }
        public int DomainId { get; set; }
        //public int? Status { get; set; } = 1;
        public OurCustomerStatus Status{ get; set; }= OurCustomerStatus.Active;
    }
}
