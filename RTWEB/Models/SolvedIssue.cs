using RTWEB.Enum;

namespace RTWEB.Models
{
    public class SolvedIssue
    {
        public int Id { get; set; }
        public int? CustomerId {  get; set; }
        public int? SolvedBy {  get; set; }
        public DateTime? SolvedDate { get; set; }
        //public int? Status {  get; set; }
        public CustomerSolvedIssueStatus Status { get; set; }= CustomerSolvedIssueStatus.Solved;
        public int? DomainId { get; set; }
        public List<SolvedDetail> SolvedDetails { get; set; } = new();
    }
}
