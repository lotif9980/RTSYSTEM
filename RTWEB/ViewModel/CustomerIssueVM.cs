using RTWEB.Enum;

namespace RTWEB.ViewModel
{
    public class CustomerIssueVM
    {
        public int Id { get; set; }
        public string? Domainname { get; set; }
        public string? CustomerName { get; set; }
        public int ? CustomerId { get; set; }
        public string? Problem { get; set; }
        //public int? Status { get; set; }
        public CustomerIssueStatus Status { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
    }
}
