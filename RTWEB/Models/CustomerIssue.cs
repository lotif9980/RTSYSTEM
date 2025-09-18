namespace RTWEB.Models
{
    public class CustomerIssue
    {
        public int Id { get; set; }
        public int? DomainId { get; set; }
        public int ? CustomerId { get; set; }
        public string? Problem { get; set; }
        public int? Status { get; set; }
        public DateTime CreateDate {  get; set; }
    }
}
