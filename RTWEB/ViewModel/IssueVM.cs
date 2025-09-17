using RTWEB.Enum;
using RTWEB.Models;

namespace RTWEB.ViewModel
{
    public class IssueVM
    {
        public int Id {  get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public string AssignPersion { get; set; }
        public string ProjectName { get; set; }
        public IssueStatus Status { get; set; }

    }

    public class IssueSaveVm
    {
        //public Issue Issue { get; set; }
        public List<Issue> Issues { get; set; } = new List<Issue>();
        public IEnumerable<Project> Projects { get; set; }
        public int ProjectId { get; set; }

    }
}
