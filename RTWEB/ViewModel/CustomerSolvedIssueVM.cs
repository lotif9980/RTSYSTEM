
using RTWEB.Enum;
using RTWEB.Models;

namespace RTWEB.ViewModel
{
    public class CustomerSolvedIssueVM
    {
        public int Id { get; set; }
        public string? CustomerName { get; set; }
        public string? SolvedBy { get; set; }
        public DateTime? SolvedDate { get; set; }
        //public int? Status { get; set; }
        public CustomerSolvedIssueStatus Status { get; set; }
        public string? DomainName { get;set; }
        public int ? solvedEmployeeId { get; set; }
        public List<SolveDetails> SolveDetails { get; set; } = new List<SolveDetails>();
    }


    public class CSPViewModel
    {
        public SolvedIssue SolvedIssue { get; set; }
        public IEnumerable<Domain> Domains { get; set; }
        public IEnumerable<OurCustomerVM> OurCustomers { get; set; }
        public IEnumerable<Team> Teams { get; set; }
        public IEnumerable<CustomerIssueVM> CustomerIssue { get; set; }

        public List<SolveDetails> SolveDetails { get; set; } = new List<SolveDetails>();
    }

    public class SolveDetails
    {
        public int Id { get; set; }
        public int? SolvedIssueId { get; set; }
        public string? IssueName { get; set; }
        public int? IssueId { get; set; }
        public string? SolutionDetails {  get; set; }
    }
}
