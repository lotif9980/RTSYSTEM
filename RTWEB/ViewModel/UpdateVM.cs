using RTWEB.Models;

namespace RTWEB.ViewModel
{
    public class UpdateVM
    {
      
        public int Id {  get; set; }
        public string? DomainName { get; set; }
        public DateTime UpdateDate { get; set; }
        public string? BranchName { get; set; }
        public string? DeveloperName { get; set; }
        public string? TesterName { get; set; }
        public int? Status { get; set; }
        public List<UpdateDetailsVM> UpdateDetails { get; set; } = new List<UpdateDetailsVM>();
    }

    public class UpdateSaveVM
    {
        public Update Update { get; set; }
        public IEnumerable<Team> Developer {  get; set; }
        public IEnumerable<Team> Tester { get; set; }
        public IEnumerable<Domain> Domain { get; set; }
        public List<UpdateDetailsVM> UpdateDetails { get; set; } = new List<UpdateDetailsVM>();
    }

    public class UpdateDetailsVM
    {
        public int Id { get; set; }
        public int UpdateId { get; set; }
        public int IssueId { get; set; }
        public string IssueName { get; set; }
    }

}
