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
        public int Status { get; set; }

    }

    public class IssueSaveVm
    {
        public Issue Issue { get; set; }
        public IEnumerable<Project> Projects { get; set; }
      
    }
}
