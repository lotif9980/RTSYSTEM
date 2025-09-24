using RTWEB.Models;

namespace RTWEB.ViewModel
{
    public class OurCustomerVM
    {
        public int Id { get; set; }
        public string? Code { get; set; }
        public string? CustomerName { get; set; }
        public string? Address { get; set; }
        public string? ContactNo { get; set; }
        public DateTime CreateDate { get; set; }
        public string? DomainName { get; set; }
        public int? Status { get; set; }
        public int DoaminId { get; set; }
    }
}
