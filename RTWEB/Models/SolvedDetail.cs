namespace RTWEB.Models
{
    public class SolvedDetail
    {
        public int Id {  get; set; }
        public int? SolvedIssueId {  get; set; }
        public int? IssueId { get; set; }
        public string ? SolutionDetails {  get; set; }
    }
}
