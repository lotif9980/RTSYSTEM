using RTWEB.Enum;

namespace RTWEB.Models
{
    public class Issue
    {
        public int Id { get; set; } 
        public string Title { get; set; }
        public string? Description { get; set; }
        //public int AssignedTo { get; set; }
        public int ProjectId {  get; set; }
        //public int Status { get; set; } = 1;
        public IssueStatus Status { get; set; } 

    }
}
