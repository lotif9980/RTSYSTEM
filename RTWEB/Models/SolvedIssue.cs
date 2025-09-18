namespace RTWEB.Models
{
    public class SolvedIssue
    {
        public int Id { get; set; }
        public int? CustomerId {  get; set; }
        public int? SolvedBy {  get; set; }
        public DateTime? SolvedDate { get; set; }
        public int? Status {  get; set; }
    }
}
